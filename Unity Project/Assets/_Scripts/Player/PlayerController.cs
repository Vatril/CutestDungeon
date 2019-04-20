using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{

	private CharacterController cc;
	private PlayerModelController current;
	private GameController gc;
	private UIController uic;
	public float speed;
	public PlayerModelController[] playerModels;
	public Portal portal;

	public static PlayerController instance;

	private MainUIHolder holder;


	void Awake(){
		if (instance == null) {
			DontDestroyOnLoad (this.gameObject);
			instance = this;
		} else if (instance != this) {
			Destroy (this.gameObject);
		}

	}

	void Start ()
	{
		holder = FindObjectOfType<MainUIHolder> ();
		uic = GetComponent<UIController> ();
		gc = GameObject.FindObjectOfType<GameController> ();
		SoundManager.instance.SwitchTo (SoundManager.State.SHUFFLE);

	}
		

	public void SetCharacter (int character)
	{
		
		if (holder == null) {
			holder = FindObjectOfType<MainUIHolder> ();
		}
		if (current == null) {
			current = Instantiate (playerModels [character], this.transform) as PlayerModelController;
		}
		var p = Instantiate (portal, this.transform);
		p.ActivateFor5 ();
		current.gameObject.SetActive (false);
		StartCoroutine (SetActiveAfter4Seconds ());
		holder.mana.fillRect.GetComponent<Image> ().color = current.color;
		cc = current.GetComponent<CharacterController> ();

		PlayerFollower pf = Camera.main.gameObject.GetComponent<PlayerFollower> ();
		if (pf != null) {
			pf.toFollow = current.transform;
		}
		uic.DisplayHearts (current.GetHealth (), current.GetMaxHealth ());
		current.transform.position = Vector3.zero;
	}

	void FixedUpdate ()
	{
		if (current != null) {
			HandleMove ();
		}
	}

	void Update(){
		if (holder == null || holder.mana == null) {
			holder = FindObjectOfType<MainUIHolder> ();
		}
	}

	public PlayerModelController GetPlayer ()
	{
		return current;
	}

	private void HandleMove ()
	{
		if (current.gameObject.activeInHierarchy && current.alive) {
			cc.Move (new Vector3 (InputManager.GetHorizontal (), -10f, InputManager.GetVertical ()) * Time.deltaTime * speed * GetPlayer ().speedMulti);
			current.isMoving = InputManager.GetHorizontal () != 0 || InputManager.GetVertical () != 0;
		}
	}

	void LateUpdate ()
	{
		if (current == null)
			return;
		if (holder == null)
			return;
		holder.mana.value = current.GetManaPercent ();
		if (current.alive && Time.timeScale > 0) {
			current.transform.rotation = Quaternion.FromToRotation (Vector3.forward, 
				new Vector3 (Input.mousePosition.x - Camera.main.WorldToScreenPoint (current.transform.position).x,
					0, Input.mousePosition.y - Camera.main.WorldToScreenPoint (current.transform.position).y));
		}
	}

	IEnumerator SetActiveAfter4Seconds ()
	{
		yield return new WaitForSeconds (4);
		current.gameObject.SetActive (true);
		FindObjectOfType<AchievementTracker> ().SetLevelStartTime ();
	}

	public class ScreenItemHolder
	{
		public ScreenItem item;
		public int amount;

		public ScreenItemHolder (ScreenItem i, int a)
		{
			item = i;
			amount = a;
		}
	}
}
