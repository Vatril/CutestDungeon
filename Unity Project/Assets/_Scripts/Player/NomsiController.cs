using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NomsiController : PlayerModelController {


	// Use this for initialization
	protected override void Start () {
		base.maxmana = 50;
		base.manaregenrate = 0.5f;
		base.damageMulti = 1.3f;
		base.Start();
	}

	protected override void FixedUpdate () {
		base.FixedUpdate ();
	}
}
