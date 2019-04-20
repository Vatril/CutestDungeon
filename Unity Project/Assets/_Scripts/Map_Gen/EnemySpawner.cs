using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[System.Obsolete("EnemySpawner is deprecated, please use EnemySpawner2 instead.")]
public class EnemySpawner : MonoBehaviour {

	public EnemyController[] enemies;



	// Use this for initialization
	void Start () {
		
	}
	
	public List<EnemyController> Spawn(LinkedList<DungeonPart> map){

		List<EnemyController> ecs = new List<EnemyController>();
		var m = map.Where( dp => !((Mathf.Abs(dp.logicalPosition.x) < 2) && Mathf.Abs(dp.logicalPosition.y) < 2)).ToList();
		foreach (DungeonPart dp in m) {
			foreach (SpawnSpot s in dp.gameObject.GetComponentsInChildren<SpawnSpot> ()) {
				EnemyController ec = Instantiate (enemies [0], s.transform.position, s.transform.localRotation) as EnemyController;
				ec.transform.SetParent (s.transform);
				ecs.Add (ec);
				ec.gameObject.SetActive (false);
			}
		}
		return ecs;
	}
}
