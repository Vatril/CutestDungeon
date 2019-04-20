using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlinkImage : MonoBehaviour {

	public Color blinkColor;

	private Image image;
	private Color origColor;
	private bool blinking;


	void Start () {
		image = GetComponent<Image> ();
		origColor = image.color;
	}
	
	public void Blick(){
		if (blinking)
			return;
		blinking = true;
		StartCoroutine (BlickMe());
	}

	private IEnumerator BlickMe(){
		for (int i = 0; i < 10; i++) {
			image.color = blinkColor;
			yield return new WaitForSeconds (.1f);
			image.color = origColor;
			yield return new WaitForSeconds (.1f);
		}
		blinking = false;
	}
}
