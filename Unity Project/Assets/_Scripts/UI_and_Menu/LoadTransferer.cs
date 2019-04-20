using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadTransferer : MonoBehaviour {

	public string sceneToLoad;

	// Use this for initialization
	void Start () {
		DontDestroyOnLoad (gameObject);
	}

}
