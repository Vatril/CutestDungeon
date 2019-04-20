using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dropper : MonoBehaviour {

	public Key key;

	public Drop[] drops;

	public void DropItem(){

		if (!GameController.maincontroller.keyHasDropped) {
			if (Random.value < .05) {
				Instantiate (key, transform.position, Quaternion.identity);
				GameController.maincontroller.keyHasDropped = true;
				return;
			}
		} else if (Random.value < .005) {
			Instantiate (key, transform.position, Quaternion.identity);
			return;
		}

		if (Random.value > .8) {
			int dropsmaxrange = 0;
			List<Drop> temp = new List<Drop>();
			foreach(Drop d in drops){
				for (int i = 0; i < d.rate; i++) {
					temp.Add (d);
				}
			}
			Instantiate (temp[Random.Range(0,temp.Count)].drop, transform.position, Quaternion.identity);
		}
	}

	[System.Serializable]
	public class Drop{
		public Pickup drop;
		public int rate;
	}
}
