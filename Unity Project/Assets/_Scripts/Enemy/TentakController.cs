using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TentakController : EnemyController {

	private Animator ani;

	protected override void Start(){
		base.Start ();
		this.health = 60;
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
		return new Vector2 (3,6);
	}
}
