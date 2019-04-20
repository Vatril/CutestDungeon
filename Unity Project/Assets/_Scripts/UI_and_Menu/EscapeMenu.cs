using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EscapeMenu : MonoBehaviour {

	public Text text;
	public Text dead;
	public Button quit;
	public Button deadquit;
	public Button nope;
	public GameObject volumePanel;

	// Use this for initialization
	void Start () {
		text.gameObject.SetActive (false);
		nope.gameObject.SetActive (false);
		quit.gameObject.SetActive (false);
		dead.gameObject.SetActive (false);
		deadquit.gameObject.SetActive (false);
		volumePanel.gameObject.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
		if (InputManager.GetMenu() && GameController.maincontroller.canPause) {
			Time.timeScale = 0;
			text.gameObject.SetActive (true);
			nope.gameObject.SetActive (true);
			quit.gameObject.SetActive (true);
			volumePanel.gameObject.SetActive (true);
		}
	}

	public void Quit(){
		Time.timeScale = 1;
		SceneManager.LoadScene ("StartScene");

		Destroy (GameObject.FindObjectOfType<UIController> ().gameObject);
		Destroy (GameObject.FindObjectOfType<MainUIHolder> ().gameObject);
		Destroy (GameObject.FindObjectOfType<AchievementTracker> ().gameObject);
		Destroy (GameController.maincontroller.gameObject);
		Destroy (this.gameObject);
	}

	public void Cancel(){
		Time.timeScale = 1;
		text.gameObject.SetActive (false);
		nope.gameObject.SetActive (false);
		quit.gameObject.SetActive (false);
		volumePanel.gameObject.SetActive (false);
	}

	public void Dead(){
		GameController.maincontroller.canPause = false;
		dead.gameObject.SetActive (true);
		deadquit.gameObject.SetActive (true);
	}
}
