using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : Pickup {

	protected override void Handle (PlayerModelController pmc){
		pmc.ModMaxHealth (1);
		pmc.Heal (4);
		base.consumed = true;
		Destroy (gameObject);

	}

	public override string GetName (){
		return "Heart";
	}
}
