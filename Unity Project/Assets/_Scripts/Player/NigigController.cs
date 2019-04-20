using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NigigController : PlayerModelController {



	// Use this for initialization
	protected override void Start () {
		base.maxhealth = 24;
		base.speedMulti = .7f;
		base.Start();
	}

	protected override void FixedUpdate () {
		base.FixedUpdate ();
	}
}
