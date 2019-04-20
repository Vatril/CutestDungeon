using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainController : PlayerModelController {


	// Use this for initialization
	protected override void Start () {
		base.maxmana = 200;
		base.manaregenrate = 2;
		base.damageMulti = .8f;
		base.Start();
	}

	protected override void FixedUpdate () {
		base.FixedUpdate ();
	}
}
