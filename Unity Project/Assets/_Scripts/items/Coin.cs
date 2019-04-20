using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : Pickup {

	protected override void Handle (PlayerModelController pmc){
		base.consumed = true;
		Destroy (gameObject);
		pmc.ModMoney (1);
	}

	public override string GetName (){
		return "Coin";
	}
}
