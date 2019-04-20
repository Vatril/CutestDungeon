using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Pickup : MonoBehaviour{

	protected bool consumed = false;

	public void Consume(PlayerModelController pmc){
		if (!consumed) {
			Handle (pmc);
		}
	}
	protected abstract void Handle (PlayerModelController pmc);

	public abstract string GetName ();
}
