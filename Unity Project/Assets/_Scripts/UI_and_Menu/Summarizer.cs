using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Summarizer : MonoBehaviour {

	public Text name;
	public Text health;
	public Text mana;
	public Text speed;
	public Text damage;

	public void SetDisplay(string character){
		switch (character) {
		case "Vatril":
			name.text = "Vatril";
			health.text = "OO";
			mana.text = "OO";
			speed.text = "OO";
			damage.text = "OO";
			break;
		case "Regin":
			name.text = "Regin";
			health.text = "OO";
			mana.text = "OO";
			speed.text = "OOO";
			damage.text = "O";
			break;
		case "Rain":
			name.text = "Rain";
			health.text = "OO";
			mana.text = "OOO";
			speed.text = "OO";
			damage.text = "O";
			break;
		case "Nomsi":
			name.text = "Nomsi";
			health.text = "OO";
			mana.text = "O";
			speed.text = "OO";
			damage.text = "OOO";
			break;
		case "Nigig":
			name.text = "Nigig";
			health.text = "OOO";
			mana.text = "OO";
			speed.text = "O";
			damage.text = "OO";
			break;
		case null:
			name.text = "Locked";
			health.text = "?";
			mana.text = "?";
			speed.text = "?";
			damage.text = "?";
			break;
		}
	}
}
