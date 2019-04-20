using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameController : MonoBehaviour {

	public static GameController maincontroller;
	public int playerSelected = -1;
	public bool keyHasDropped = false;
	public bool finalRound;
	public bool finalRoundEnded;
	public bool[] bosskilled;
	public bool canPause;
	public float difficutlyScale = .5f;

	public const int MATTY = 0;
	public const int FOLFS = 1;
	public const int HUGOTILES = 2;
	public const int BANNONJEFF = 3;


	void Awake(){
		if (maincontroller == null) {
			DontDestroyOnLoad (this.gameObject);
			maincontroller = this;
		} else if (maincontroller != this) {
			Destroy (this.gameObject);
		}
		bosskilled = new System.Boolean[]{ false, false, false, false };
	}

	
	// Update is called once per frame
	void Update () {
		
	}

}
