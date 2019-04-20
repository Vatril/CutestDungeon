using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpeechBubble : MonoBehaviour {

	public Text text;
	public Image bubble;
	public Text text2;

	void Start(){
		bubble.gameObject.SetActive (false);
	}

	public void ShowText(string t, string t2){
		text.text = t;
		text2.text = t2;
		bubble.gameObject.SetActive (true);
	}

	public void Hide(){
		bubble.gameObject.SetActive (false);
	}
}
