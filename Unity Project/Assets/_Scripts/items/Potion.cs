using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Potion : Pickup {

	public DisplayPotion potionitem;

	public MeshRenderer[] balls;

	public Rate[] rate;

	private bool setUp;

	void Start(){

		if (setUp)
			return;

		var posspot = new LinkedList<DisplayPotion.PotionName>();
		if (AchievementManager.IsAchievementDone (AchievementManager.AchievementName.DieOnce)) {
			for(int i = 0; i < rate.Single (r => r.name == DisplayPotion.PotionName.Health).rate; i++){
				posspot.AddLast (DisplayPotion.PotionName.Health);
			}
		}
		if (AchievementManager.IsAchievementDone (AchievementManager.AchievementName.CompleteLevel)) {
			for (int i = 0; i < rate.Single (r => r.name == DisplayPotion.PotionName.Mana).rate; i++) {
				posspot.AddLast (DisplayPotion.PotionName.Mana);
			}
		}
		if (AchievementManager.IsAchievementDone (AchievementManager.AchievementName.Kill250Enemies)) {
			for (int i = 0; i < rate.Single (r => r.name == DisplayPotion.PotionName.Invinc).rate; i++) {
				posspot.AddLast (DisplayPotion.PotionName.Invinc);
			}
		}
		if (AchievementManager.IsAchievementDone (AchievementManager.AchievementName.Kill100Enemies)) {
			for (int i = 0; i < rate.Single (r => r.name == DisplayPotion.PotionName.Regen).rate; i++) {
				posspot.AddLast (DisplayPotion.PotionName.Regen);
			}
		}
		if (AchievementManager.IsAchievementDone (AchievementManager.AchievementName.LooseAHeart)) {
			for (int i = 0; i < rate.Single (r => r.name == DisplayPotion.PotionName.Strength).rate; i++) {
				posspot.AddLast (DisplayPotion.PotionName.Strength);
			}
		}
		if (AchievementManager.IsAchievementDone (AchievementManager.AchievementName.FinishIn3)) {
			for (int i = 0; i < rate.Single (r => r.name == DisplayPotion.PotionName.Speed).rate; i++) {
				posspot.AddLast (DisplayPotion.PotionName.Speed);
			}
		}

		if (posspot.Count == 0) {
			base.consumed = true;
			DestroyImmediate (this.gameObject);
			return;
		}

		int rand = Random.Range (0, posspot.Count);

		var randItem = posspot.First;

		for(int i = 0; i < rand; i++){
			randItem = randItem.Next;
		}

		SetToName (randItem.Value);

	}

	public void SetToName(DisplayPotion.PotionName name){
		setUp = true;
		potionitem = Instantiate (potionitem);
		potionitem.gameObject.SetActive (false);
		potionitem.Setup (name);

		foreach (MeshRenderer r in balls) {
			r.materials [0].color = potionitem.colorPart.color;
		}
	}

	protected override void Handle (PlayerModelController pmc){
		pmc.GetComponentInParent<ItemManager> ().AddItem(potionitem);
		base.consumed = true;
		Destroy (gameObject);

	}

	public override string GetName (){
		return potionitem.GetName ();
	}

	[System.Serializable]
	public class Rate{
		public DisplayPotion.PotionName name;
		public int rate;

	}
}
