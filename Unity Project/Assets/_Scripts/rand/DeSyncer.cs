using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeSyncer : MonoBehaviour {

	private Animator ani;
	private float speed;


	// Use this for initialization
	void Start () {
		ani = GetComponent<Animator> ();
		speed = ani.speed;
		ani.speed = 0;
		StartCoroutine (StartAni());
	}
	
	private IEnumerator StartAni(){
		yield return new WaitForSeconds (Random.value);
		ani.speed = speed;
	}
}
