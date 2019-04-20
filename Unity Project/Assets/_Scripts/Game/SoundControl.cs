using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundControl : MonoBehaviour {

	public Slider music;
	public Slider sfx;

	private AudioSource pas;

	// Use this for initialization
	void Start () {
		music.value = SoundManager.instance.GetMusicVolume ();
		sfx.value = SoundManager.instance.GetSFXVolume ();
		pas = FindObjectOfType<PlayerModelController> ().GetComponent<AudioSource> ();
	}

	public void SetSFX(){
		SoundManager.instance.SetSFXLoudness (sfx.value);
		if (pas == null) {
			pas = FindObjectOfType<PlayerModelController> ().GetComponent<AudioSource> ();
		}
		pas.volume = sfx.value;
	}

	public void SetMusic(){
		SoundManager.instance.SetMusicLoudness (music.value);
	}
}
