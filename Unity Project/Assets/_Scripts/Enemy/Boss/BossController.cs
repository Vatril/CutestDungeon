using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BossController : EnemyController {

	// Use this for initialization
	protected override void Start () {
		base.Start ();
	}
	
	// Update is called once per frame
	protected virtual void Update () {
		base.Update ();
	}

	public override Vector2 GetDamageRange(){
		return new Vector2(1,6);
	}

	public int GetLife(){
		return this.health;
	}

	public abstract string GetName ();

	public void ReduceLife(){
		this.health /= 3;
	}
}
