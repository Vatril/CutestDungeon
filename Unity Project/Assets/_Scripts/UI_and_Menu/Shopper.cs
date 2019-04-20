using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shopper : MonoBehaviour {

	public Potion basepotion;
	public Heart heart;
	public PlayerModelController[] players;

	// Use this for initialization
	void Start () {
		if (AchievementManager.IsAchievementDone (AchievementManager.AchievementName.DieOnce)) {
			var pot = Instantiate (basepotion, this.transform);
			pot.SetToName (DisplayPotion.PotionName.Health);
			pot.transform.localPosition = new Vector3 (-1,2,-6f);
		}
		if (AchievementManager.IsAchievementDone (AchievementManager.AchievementName.CompleteLevel)) {
			var pot = Instantiate (basepotion, this.transform);
			pot.SetToName (DisplayPotion.PotionName.Mana);
			pot.transform.localPosition = new Vector3 (-1,2,-4f);
		}
		if (AchievementManager.IsAchievementDone (AchievementManager.AchievementName.Kill250Enemies)) {
			var pot = Instantiate (basepotion, this.transform);
			pot.SetToName (DisplayPotion.PotionName.Invinc);
			pot.transform.localPosition = new Vector3 (-1,2,-2f);
		}
		if (AchievementManager.IsAchievementDone (AchievementManager.AchievementName.Kill100Enemies)) {
			var pot = Instantiate (basepotion, this.transform);
			pot.SetToName (DisplayPotion.PotionName.Regen);
			pot.transform.localPosition = new Vector3 (-1,2,0f);
		}
		if (AchievementManager.IsAchievementDone (AchievementManager.AchievementName.LooseAHeart)) {
			var pot = Instantiate (basepotion, this.transform);
			pot.SetToName (DisplayPotion.PotionName.Strength);
			pot.transform.localPosition = new Vector3 (-1,2,2f);
		}
		if (AchievementManager.IsAchievementDone (AchievementManager.AchievementName.FinishIn3)) {
			var pot = Instantiate (basepotion, this.transform);
			pot.SetToName (DisplayPotion.PotionName.Speed);
			pot.transform.localPosition = new Vector3 (-1,2,4f);
		}

		var heartItem = Instantiate (heart, this.transform);
		heartItem.transform.localPosition = new Vector3 (-1,2,6f);


		var player = Instantiate (players[GameController.maincontroller.playerSelected]
			, new Vector3(2 + transform.position.x,1+ transform.position.y,+ transform.position.z),
			Quaternion.FromToRotation(Vector3.forward,Vector3.left));
		player.enabled = false;
		foreach (ParticleSystem ps in player.gameObject.GetComponentsInChildren<ParticleSystem>()) {
			Destroy (ps);
		}
		Destroy (player.GetComponentInChildren<Portal>().gameObject);

	}


	public void Back(){
		GameObject.FindObjectOfType<ShopController> ().EndShopping();
	}
}
