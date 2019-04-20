using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Screenkey : ScreenItem {

	public override bool ActOnPlayer(PlayerModelController pmc){
		pmc.ToShop ();
		return false;
	}

	public override string GetName (){
		return "Shop Key";
	}
}
