using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoryController : MonoBehaviour {

	public LoadManager loadManager;
	public Text text;
	public AudioClip[] intros;

	private AudioSource source;

	void Start(){

		source = GetComponent<AudioSource> ();
		source.volume = SoundManager.instance.GetMusicVolume () * 4;
		source.clip = intros[GameController.maincontroller.playerSelected];
		source.Play ();

		switch (GameController.maincontroller.playerSelected) {
		case 0:
			text.text = "There is suppose to be a crystal deep in this dungeon that allows a magic user to gaze into other people souls.\nWith its help, I could maybe finally find true love.\n\nI have to retrieve it....";
			break;
		case 1:
			text.text = "Rumours tell of a powerful crystal hidden deep inside this dungeon.\nIts abilities seem to outmatch them of any other.\nThe rumours spread fast, I can't let it fall into the wrong hands.\n\nI have to retrieve it....";
			break;
		case 2:
			text.text = "I have heard that deep in this dungeon there is a crystal with strong healing capabilities.\nMaybe it can cure the terrible disease that is ravaging away in my hometown.\nI have to act fast before the disease kills my fiancé.\n\nI have to retrieve it....";
			break;
		case 3:
			text.text = "The crystal that rests in this dungeon could give me unlimited power.\nWith it, I could gain all the wealth in the world, rule everything.\nIt could give me the power to live forever!\n\nI have to retrieve it....";
			break;
		case 4:
			text.text = "A crystal is suppose to lie inside this dungeon.\nIt has to be worth a lot.\nWith the money I would get from selling it I could finally pay off my villages dept.\n\nI have to retrieve it....";
			break;
		}
	}

	void Update () {
		if (Input.anyKey) {
			loadManager.Load ("Dungeon");
		}
	}
}
