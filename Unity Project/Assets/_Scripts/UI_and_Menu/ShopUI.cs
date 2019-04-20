using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopUI : MonoBehaviour {

	public Camera cam;
	public PlayerModelController pmc;
	public Prices prices;
	public SpeechBubble display;

	// Update is called once per frame
	void Update () {
		RaycastHit hit;
		Pickup obj;
		if (Physics.Raycast (cam.ScreenPointToRay (Input.mousePosition), out hit)) {
			if ((obj = hit.collider.gameObject.GetComponent<Pickup> ()) != null) {
				var price =  GetPrice(obj.GetName());
				display.ShowText (string.Format("X {0}", price), obj.GetName());
				if (Input.GetMouseButtonDown (0)) {
					if (pmc.CanAfford (price)) {
						Debug.LogError (pmc.gameObject.GetComponentInParent<ItemManager>());
						var newitem = Instantiate (obj, obj.transform.parent);
						newitem.transform.localPosition = obj.transform.localPosition;
						Potion pot;
						if ((pot = newitem.GetComponent<Potion>()) != null) {
							pot.SetToName (obj.GetComponent<Potion>().potionitem.name);
						}
						obj.Consume (pmc);
						pmc.ModMoney (-price);
					}
				}
			} else {
				display.Hide ();
			}
		}

	}
	private int GetPrice(string name){
		switch (name) {
		case "Heart":
			return prices.heartPrice;
		case "Health Potion":
			return prices.healthPrice;
		case "Mana Potion":
			return prices.manaPrice;
		case "Regen Potion":
			return prices.regenPrice;
		case "Invincibility Potion":
			return prices.invincPrice;
		case "Strength Potion":
			return prices.strengthPrice;
		case "Speed Potion":
			return prices.speedPrice;
		}
		return 0;
	}

	[System.Serializable]
	public class Prices{
		public int healthPrice;
		public int regenPrice;
		public int speedPrice;
		public int invincPrice;
		public int strengthPrice;
		public int manaPrice;
		public int heartPrice;
	}
}
