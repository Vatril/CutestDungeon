using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuicideAfter : MonoBehaviour {

	public float time;

	// Use this for initialization
	void Start () {
		StartCoroutine (Suicide());
	}
	
	private IEnumerator Suicide(){
		yield return new WaitForSeconds (time);
		Destroy (this.gameObject);
	}
}
