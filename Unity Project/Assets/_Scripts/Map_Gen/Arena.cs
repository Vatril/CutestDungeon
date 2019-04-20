using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arena : MonoBehaviour {

	public GameObject[] arenas;

	// Use this for initialization
	void Start () {
		arenas [Random.Range (0, arenas.Length)].gameObject.SetActive(true);
		FindObjectOfType<MapCamController> ().PositionCam ();
	}
}
