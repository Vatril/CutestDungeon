using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossFightController : MonoBehaviour
{

	public MattyController matty;
	public HugoTilesController hugotiles;
	public FolfController folf;
	public FolfController neo;
	public BannonJeffController bannonjeff;

	public Arena arena;

	public Portal portal;

	public EnemyController[] spawnables;

	public Text bossText;

	private int bossfight;
	private bool started = false;
	private BossController[] bosses = new BossController[5];
	private Slider life;
	private int maxlife;
	private bool spawnwaves = false;
	public SuicideAfter particles;
	public Heart heart;
	public Star star;
	public Coin coin;
	public Potion potion;


	// Use this for initialization
	void Start ()
	{
		life = GetComponentInChildren<Slider> ();
		Instantiate (portal, this.transform).ActivateFor5 ();
		var p2 = Instantiate (portal, this.transform);
		p2.ActivateFor5 ();
		p2.transform.position = new Vector3 (0, 0, -15);

		StartCoroutine (SpawnBossAfterTime ());
		StartCoroutine (SetUpChar ());
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (started) {
			life.value = bosses [0].GetLife ()
			+ (bosses [1] != null ? bosses [1].GetLife () : 0)
			+ (bosses [2] != null ? bosses [2].GetLife () : 0)
			+ (bosses [3] != null ? bosses [3].GetLife () : 0)
			+ (bosses [4] != null ? bosses [4].GetLife () : 0);
			if (!spawnwaves && (maxlife / 1.25f > life.value)) {
				spawnwaves = true;
				StartCoroutine (Spawnwaves ());
			}
			if (bosses [0] == null && bosses [1] == null && bosses [2] == null && bosses [3] == null && bosses [4] == null) {
				if (GameController.maincontroller.finalRound) {
					GameController.maincontroller.finalRoundEnded = true;
					FindObjectOfType<PlayerModelController> ().AddProgress (500);
					FindObjectOfType<AchievementTracker> ().BossKilled ();
					FindObjectOfType<AchievementTracker> ().LastStandEnd ();
				} else {
					GameController.maincontroller.bosskilled [bossfight] = true;
					Vector3 pos = FindObjectOfType<PlayerModelController> ().transform.position;
					SpawnAroundBoss (pos, heart.gameObject);
					for (int i = 0; i < Random.Range (5, 20); i++) {
						SpawnAroundBoss (pos, coin.gameObject);
					}
					for (int i = 0; i < Random.Range (1, 10); i++) {
						SpawnAroundBoss (pos, potion.gameObject);
					}
					for (int i = 0; i < Random.Range (0, 5); i++) {
						SpawnAroundBoss (pos, star.gameObject);
					}
					FindObjectOfType<PlayerModelController> ().AddProgress (500);
					FindObjectOfType<AchievementTracker> ().BossKilled ();
				}
				started = false;
				GameController.maincontroller.difficutlyScale += .25f;
			}
		}

	}

	private void SpawnAroundBoss(Vector3 projpoint, GameObject what){
		RaycastHit info;
		bool spawned = false;
			projpoint.y = 20;
		do {
			if(!(spawned = Physics.Raycast (projpoint, new Vector3 ((Random.value - 0.5f) * 10, -10, (Random.value - 0.5f) * 10), out info, Mathf.Infinity, (1 << 13)))){
				continue;
			}

			Instantiate (what, info.point, Quaternion.identity).gameObject.transform.SetParent (this.transform);
			Instantiate (particles, info.point, Quaternion.identity).gameObject.transform.SetParent (this.transform);

		}while(!spawned);

	}

	private IEnumerator SetUpChar ()
	{
		yield return new WaitUntil (() => {
			return FindObjectOfType<PlayerController> () != null;
		});
		FindObjectOfType<PlayerController> ().SetCharacter (GameController.maincontroller.playerSelected);
		FindObjectOfType<PlayerController> ().GetPlayer ().transform.position = new Vector3 (0, 0, -15);
	}

	private IEnumerator SpawnBossAfterTime ()
	{
		
		if (!(GameController.maincontroller.bosskilled [GameController.BANNONJEFF] &&
		    GameController.maincontroller.bosskilled [GameController.HUGOTILES] &&
		    GameController.maincontroller.bosskilled [GameController.FOLFS] &&
		    GameController.maincontroller.bosskilled [GameController.MATTY])) {
			do {
				bossfight = Random.Range (0, 4);
			} while(GameController.maincontroller.bosskilled [bossfight]);
			yield return new WaitForSeconds (5);
			switch (bossfight) {
			case GameController.FOLFS:
				bosses [0] = Instantiate (folf, this.transform);
				bosses [1] = Instantiate (neo, this.transform);
				break;
			case GameController.MATTY:
				bosses [0] = Instantiate (matty, this.transform);
				break;
			case GameController.BANNONJEFF:
				bosses [0] = Instantiate (bannonjeff, this.transform);
				break;
			case GameController.HUGOTILES:
				bosses [0] = Instantiate (hugotiles, this.transform);
				break;
			}
			bossText.text = bosses [0].GetName ();
		} else {
			bossfight = -1;
			Debug.LogError (bossfight);
			GameController.maincontroller.finalRound = true;
			yield return new WaitForSeconds (5);
			bosses [0] = Instantiate (folf, this.transform);
			bosses [1] = Instantiate (neo, this.transform);
			bosses [2] = Instantiate (matty, this.transform);
			bosses [3] = Instantiate (bannonjeff, this.transform);
			bosses [4] = Instantiate (hugotiles, this.transform);
			bossText.text = "Last Stand";
		}

		yield return new WaitUntil (() => {

			return ((bosses [0].GetLife ()
			+ (bosses [1] != null ? bosses [1].GetLife () : 0)
			+ (bosses [2] != null ? bosses [2].GetLife () : 0)
			+ (bosses [3] != null ? bosses [3].GetLife () : 0)
			+ (bosses [4] != null ? bosses [4].GetLife () : 0))) != 0;
		});
		if (bossfight == -1) {
			foreach (BossController bc in bosses) {
				bc.ReduceLife ();
				Debug.LogError (bc + " " + bc.GetLife ());
			} 
		}

		maxlife = (bosses [0].GetLife ()
		+ (bosses [1] != null ? bosses [1].GetLife () : 0)
		+ (bosses [2] != null ? bosses [2].GetLife () : 0)
		+ (bosses [3] != null ? bosses [3].GetLife () : 0)
		+ (bosses [4] != null ? bosses [4].GetLife () : 0));

		life.maxValue = maxlife;
		started = true;

	}

	private IEnumerator Spawnwaves ()
	{
		while (life.value > 5) {
			Debug.Log ("spawn");
			foreach (BossController bc in bosses) {
				if (bc == null)
					continue;
				
				for (int i = 0; i < 3; i++) {
					if (Random.value > .4f) {
						SpawnAroundBoss (bc.transform.position, spawnables [Random.Range (0, spawnables.Length)].gameObject);
					}
				}
			}
			yield return new WaitForSeconds (Random.Range (0.45f, (life.normalizedValue * 7) + 5f));
		}
	}
}
