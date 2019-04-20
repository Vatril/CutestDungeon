using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeIn : MonoBehaviour {

	public RawImage blocker;
	public float waitTime = 1.75f;
	public float fadeTime = 3f;
	// Use this for initialization
	void Start () {
		blocker.color = Color.black;
		StartCoroutine (FadeAfterXSeconds());

	}

	private IEnumerator FadeAfterXSeconds(){
		yield return new WaitForSeconds (waitTime);
		blocker.CrossFadeAlpha (0f, fadeTime, false);
		yield return new WaitForSeconds (fadeTime - 0.5f);
		GameController.maincontroller.canPause = true;
	}

}
