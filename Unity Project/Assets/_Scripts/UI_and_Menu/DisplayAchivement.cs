using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayAchivement : MonoBehaviour {

	public Text achiText;
	public Text unlockText;
	public RawImage disp;

	public Texture yes;
	public Texture no;

	public void SetParameters(string atext,string ltext, bool unlocked){
		achiText.text = atext;
		if (unlocked) {
			unlockText.text = string.Format("Unlocked: {0}",ltext);
			disp.texture = yes;
		} else {
			unlockText.text = "Locked";
			disp.texture = no;
		}
	}

}
