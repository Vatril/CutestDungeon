using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReginController : PlayerModelController {

	// Use this for initialization
	protected override void Start () {
		base.speedMulti = 1.5f;
		base.damageMulti = .7f;
		base.Start();
	}

	protected override void FixedUpdate () {
		base.FixedUpdate ();
	}
}
