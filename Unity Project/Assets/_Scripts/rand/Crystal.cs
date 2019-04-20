using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crystal : MonoBehaviour {

	public GameObject[] spinners;
	public Color[] colors;

	// Use this for initialization
	void Start () {
		GetComponent<MeshRenderer>().material.color = colors[GameController.maincontroller.playerSelected];
		for (int i = 0, j = 0; i < 5; i++, j++) {
			if (i == GameController.maincontroller.playerSelected)
				i++;
			spinners [j].GetComponent<MeshRenderer>().material.color = colors [i];
			spinners [j + 4].GetComponent<MeshRenderer>().material.color = colors [i];
		}
	}
}
