using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemySpawner2 : MonoBehaviour
{

	public Enemy[] enemies;
	public int rounds;
	public SuicideAfter effect;


	// Use this for initialization
	void Start ()
	{

	}

	public void Spawn (LinkedList<DungeonPart2> map, int lrounds = -1, Vector2[] excluded = null, bool showParticles = false)
	{
		if (lrounds < 0)
			lrounds = this.rounds;
		lrounds = (int) ((lrounds/2) + (lrounds * GameController.maincontroller.difficutlyScale));
		if (excluded == null)
			excluded = new Vector2[] {
				new Vector2 (0, 0), new Vector2 (1, 0), new Vector2 (0, 1), new Vector2 (-1, -1), new Vector2 (1, 1), new Vector2 (1, -1), new Vector2 (-1, 1), new Vector2 (-1, 0), new Vector2 (0, -1)
			};
		for (int i = 0; i < lrounds; i++) {
			foreach (DungeonPart2 part in map) {
				foreach (Vector2 v2 in excluded) {
					if (part.logicalPosition.x == v2.x && part.logicalPosition.y == v2.y) {
						continue;
					}
				}
				NavMeshHit hit;
				if (NavMesh.SamplePosition ((Random.insideUnitSphere * 20) + part.gameObject.transform.position, out hit, 1f, NavMesh.AllAreas)) {
					if (Random.value > .3) {
						int enerange = 0;
						List<Enemy> temp = new List<Enemy> ();
						foreach (Enemy d in enemies) {
							for (int j = 0; j < d.rate; j++) {
								temp.Add (d);
							}
						}

						var ene = Instantiate (temp[Random.Range (0, temp.Count)].enemy, hit.position, Quaternion.identity);
						if (showParticles) {
							Instantiate (effect, ene.gameObject.transform);
						}
						ene.gameObject.SetActive (false);
						ene.transform.parent = this.transform;
						Debug.DrawLine (new Vector3 (0, 30, 0), hit.position, Color.green, duration: 20);
						StartCoroutine (ActivateAfterTime (ene.gameObject));

					}
				}
			}
		}
	}

	IEnumerator ActivateAfterTime (GameObject o)
	{
		yield return new WaitForSeconds (0);
		o.SetActive (true);
	}

	[System.Serializable]
	public class Enemy
	{
		public EnemyController enemy;
		public int rate;
	}
}
