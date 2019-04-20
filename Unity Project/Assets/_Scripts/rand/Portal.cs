using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour {

	public Color[] colors;
	private ParticleSystem ps;


	void Start () {
		ps = GetComponent<ParticleSystem> ();
		ps.Stop ();
	}
	

	void Update () {
		if (ps.isPlaying) {
			var main = ps.main;
			main.startColor = colors [Random.Range (0, colors.Length)];
		}
	}

	public void ActivateFor5(){
		StartCoroutine (TimeoutIn5());
	}

	private IEnumerator TimeoutIn5(){
		yield return new WaitForSeconds (0.1f);
		ps.Play ();
		yield return new WaitForSeconds (5);
		ps.Stop ();
	}

	public void Play(){
		if(!ps.isPlaying)
			ps.Play ();
	}

	public void Stop(){
		ps.Stop ();
	}
}
