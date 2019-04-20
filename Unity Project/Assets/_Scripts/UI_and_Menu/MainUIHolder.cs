using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainUIHolder : MonoBehaviour {

	public Slider mana;
	public Slider progress;

	public RawImage container;
	public Text amountText;
	public Text nameText;
	public Text money;
	public Canvas canvas;

	public static MainUIHolder instance;

	void Awake(){
		if (instance == null) {
			DontDestroyOnLoad (this.gameObject);
			instance = this;
		} else if (instance != this) {
			Destroy (this.gameObject);
		}

	}

}
