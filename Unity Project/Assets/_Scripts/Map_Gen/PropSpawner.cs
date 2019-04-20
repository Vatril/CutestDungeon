using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropSpawner : MonoBehaviour {

	public GameObject[] layouts;

	// Use this for initialization
	void Start () {
		if (GetComponentInParent<DungeonPart2> ().logicalPosition == Vector2.zero)
			return;
		if (Random.value > .3f) {
			layouts [Random.Range (0, layouts.Length)].SetActive (true);

		}
	}

}
