using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyController : EnemyController {

	private Animator ani;

	// Use this for initialization
	protected override void Start () {
		base.Start ();
		this.health = 10;
		ani = GetComponent<Animator> ();
	}
	
	protected override void Update () {
		base.Update ();

		if (nma.hasPath) {
			ani.SetBool ("flying", true);
		} else {
			ani.SetBool ("flying", false);
		}
	}

	public override Vector2 GetDamageRange(){
		return new Vector2 (2,3);
	}
}
