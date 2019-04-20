using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyableProp : MonoBehaviour, IDamagable {

	public int health;

	private float lastHit;
	private bool isBroken = false;

	public void Damage(int damage){
		if (lastHit + .1 < Time.time) {
			lastHit = Time.time;
			health -= damage;
			if (health <= 0) {
				Break ();
			}
		}
	}

	private void Break(){
		if (isBroken) {
			return;
		}
		isBroken = true;
		GetComponent<Dropper> ().DropItem ();
		Destroy (gameObject);
	}
}
