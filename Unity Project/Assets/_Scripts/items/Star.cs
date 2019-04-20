using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star : Pickup {

	protected override void Handle (PlayerModelController pmc){
		base.consumed = true;
		Destroy (gameObject);
		pmc.ModMoney (10);
	}

	public override string GetName (){
		return "Star";
	}
}
