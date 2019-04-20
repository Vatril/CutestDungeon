using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderController : EnemyController {

	private Animator ani;

	protected override void Start(){
		base.Start ();
		this.health = 20;
		ani = GetComponent<Animator> ();
	}

	protected override void Update () {
		base.Update ();

		if (nma.hasPath) {
			ani.SetBool ("walking", true);
		} else {
			ani.SetBool ("walking", false);
		}
	}


	public override Vector2 GetDamageRange(){
		return new Vector2 (1,2);
	}
}
