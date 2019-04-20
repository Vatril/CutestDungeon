using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericEnemy : EnemyController {

	protected override void Start(){
		base.Start ();
		this.health = 30;
	}

	public override Vector2 GetDamageRange(){
		return new Vector2 (2,4);
	}
}
