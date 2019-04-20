using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : Pickup {

	public ScreenItem keyitem;

	void Start(){
		this.transform.Rotate (new Vector3(90,90,90));
	}

	protected override void Handle (PlayerModelController pmc){
		pmc.GetComponentInParent<ItemManager> ().AddItem(Instantiate(keyitem));
		base.consumed = true;
		Destroy (gameObject);

	}

	public override string GetName (){
		return "Shop Key";
	}
}
