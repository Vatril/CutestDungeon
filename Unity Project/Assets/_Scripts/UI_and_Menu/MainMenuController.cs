using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour {

	private GameController gc;
	private bool  isLoading = false;
	public Text loading;

	public LoadManager loadManager;

	public void Exit(){
		Application.Quit();
	}

	public void Settings(){
		SceneManager.LoadScene ("Settings");
	}

	public void ResoSettings(){
		SceneManager.LoadScene ("ResoSettings");
	}

	public void Achievement(){
		SceneManager.LoadScene ("Achievements");
	}

	public void Creds(){
		SceneManager.LoadScene ("Credits");
	}

	// Use this for initialization
	void Start () {
		gc = GameController.maincontroller;
		SoundManager.instance.SwitchTo (SoundManager.State.MENU);
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (isLoading) {
			return;
		}
		if(Input.GetButton("Primary")){
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			RaycastHit hit;
			if (Physics.Raycast (ray, out hit)) {
				Debug.Log (hit.collider.name);
				if(hit.collider.CompareTag("Player")){
					if (hit.collider.name == "Vatril" || hit.collider.gameObject.GetComponent<ModelGrayer> ().isUnlocked) {
						int character = -1;
						switch (hit.collider.name) {
						case "Vatril":
							gc.playerSelected = 0;
							break;
						case "Regin":
							gc.playerSelected = 1;
							break;
						case "Rain":
							gc.playerSelected = 2;
							break;
						case "Nomsi":
							gc.playerSelected = 3;
							break;
						case "Nigig":
							gc.playerSelected = 4;
							break;
						}
						if (gc.playerSelected >= 0) {
							isLoading = true;
							SceneManager.LoadScene ("StoryScene");
						}
					}
				}
			}
		}
	}
}
