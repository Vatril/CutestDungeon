using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeController : EnemyController {


	protected override void Start(){
		base.Start ();
		this.health = 40;
	}

	public override Vector2 GetDamageRange(){
		return new Vector2 (1,4);
	}
}
