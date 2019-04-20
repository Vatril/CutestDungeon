using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomParticleColor : MonoBehaviour {

	private ParticleSystem sys;
	public Color[] colors;


	// Use this for initialization
	void Start () {
		sys = GetComponent<ParticleSystem> ();
		var m = sys.main;
		m.startColor = colors[Random.Range(0, colors.Length)];
	}
}
