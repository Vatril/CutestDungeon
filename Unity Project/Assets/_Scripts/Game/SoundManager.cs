using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

	public static SoundManager instance;

	private float musicLoudness;
	private float sfxLoudness;

	public AudioClip mainMenu;
	public AudioClip[] music;

	private State state = State.NONE;

	private AudioSource source;
	private int last;

	void Awake(){
		if (instance == null) {
			DontDestroyOnLoad (this.gameObject);
			instance = this;
		} else if (instance != this) {
			Destroy (this.gameObject);
		}
	}

	// Use this for initialization
	void Start () {
		source = GetComponent<AudioSource> ();
		musicLoudness = PlayerPrefs.GetFloat ("music",.25f);
		sfxLoudness = PlayerPrefs.GetFloat ("sfx",1f);
		source.volume = musicLoudness;
	}
	
	// Update is called once per frame
	void Update () {
		if (!source.isPlaying) {
			HandleDone ();
		}
	}

	public void SwitchTo(State state){
		if (state == this.state)
			return;
		this.state = state;
		StartCoroutine (FadeOut());
	}

	private IEnumerator FadeOut(){
		for (float i = 0; i < 1; i += 0.01f) {
			yield return new WaitForSeconds (0.01f);
			source.volume = Mathf.Lerp (musicLoudness,0,i);
		}
		source.Stop ();
		source.volume = musicLoudness;
	}

	private void HandleDone(){
		if (state == State.MENU) {
			source.clip = mainMenu;
		} else if (state == State.SHUFFLE) {
			int next;
			do {
				next = Random.Range (0, music.Length);
			} while(next == last);
			last = next;
			source.clip = music [next];
		}
		source.Play ();
	}

	[System.Serializable]
	public enum State{
		NONE,SHUFFLE,MENU
	}

	public void SetMusicLoudness(float music){
		musicLoudness = music;
		source.volume = music;
		PlayerPrefs.SetFloat ("music", music);
	}

	public void SetSFXLoudness(float sfx){
		sfxLoudness = sfx;
		foreach (AudioSource ass in FindObjectsOfType<AudioSource>()) {
			if (ass.gameObject.GetHashCode () != this.gameObject.GetHashCode ()) {
				ass.volume = sfx;
			}
		}
		PlayerPrefs.SetFloat ("sfx", sfx);
	}

	public float GetMusicVolume(){
		return musicLoudness;
	}

	public float GetSFXVolume(){
		return sfxLoudness;
	}
}
