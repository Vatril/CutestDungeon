using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartScreenController : MonoBehaviour {

	public Text gametitle;
	public Text by;
	public Text pressToStart;


	private bool done = false;

	// Use this for initialization
	void Start () {
		SoundManager.instance.SwitchTo (SoundManager.State.MENU);
		if (PlayerPrefs.GetInt ("HasInit", 0) == 0) {
			InputSettingsController.Init ();
			PlayerPrefs.SetInt ("HasInit", 1);
		}
		gametitle.CrossFadeAlpha(0f, 0f, false);
		by.CrossFadeAlpha(0f, 0f, false);
		pressToStart.CrossFadeAlpha(0f, 0f, false);


		gametitle.CrossFadeAlpha(1f, 3f, false);
		StartCoroutine (CrossfadeAfterDelay());
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.anyKey && done) {
			gametitle.CrossFadeAlpha(0f, 0.2f, false);
			by.CrossFadeAlpha(0f, 0.2f, false);
			pressToStart.CrossFadeAlpha(0f, 0.2f, false);
			SceneManager.LoadScene ("Main Menu");

		}
	}

	IEnumerator CrossfadeAfterDelay(){
		yield return new WaitForSeconds (1f);
		by.CrossFadeAlpha(1f, 3f, false);
		yield return new WaitForSeconds (2f);
		pressToStart.CrossFadeAlpha(1f, 3f, false);
		yield return new WaitForSeconds (1.2f);
		done = true;
	}


}
