using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class DisplayPotion :  ScreenItem {

	public RawImage colorPart;
	public Potionkind[] potions;

	public PotionName name { get; set;}

	public override bool ActOnPlayer(PlayerModelController pmc){
		switch (name) {
		case PotionName.Health:
			if (pmc.GetHealth () == pmc.GetMaxHealth ()) {
				return false;
			}
			pmc.Heal (Random.Range (2, 4));
			return true;
		case PotionName.Mana:
			if (pmc.GetManaPercent () > 99f) {
				return false;
			}
			pmc.HealMana (Random.Range (30, 70));
			return true;
		case PotionName.Regen:
			pmc.AddRegenTime (Random.Range(2,10));
			return true;
		case PotionName.Invinc:
			pmc.AddInvincTime (Random.Range(2,10));
			return true;
		case PotionName.Strength:
			pmc.AddStrengthTime (Random.Range(2,10));
			return true;
		case PotionName.Speed:
			pmc.AddSpeedTime (Random.Range(2,10));
			return true;
		}
		Debug.LogError ("Impossible Potion");
		return true;
	}

	public void Setup(PotionName name){
		colorPart.color = potions.Single (p => p.name == name).color;
		this.name = name;
	}

	public override string GetName (){
		switch (name) {
		case PotionName.Health:
			return "Health Potion";
		case PotionName.Mana:
			return "Mana Potion";
		case PotionName.Regen:
			return "Regen Potion";
		case PotionName.Invinc:
			return "Invincibility Potion";
		case PotionName.Strength:
			return "Strength Potion";
		case PotionName.Speed:
			return "Speed Potion";
		}
		Debug.LogError ("Impossible Potion");
		return "Display Potion";
	}

	[System.Serializable]
	public class Potionkind{
		public PotionName name;
		public Color color;

	}

	[System.Serializable]
	public enum PotionName{
		Health, Mana, Regen, Invinc, Speed, Strength
	}
}
