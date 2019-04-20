using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayPortal : ScreenItem {



	public override bool ActOnPlayer(PlayerModelController pmc){
		pmc.ToNextLevel ();
		return false;
	}

	public override string GetName (){
		return "Portal";
	}
}
