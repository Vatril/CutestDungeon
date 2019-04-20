using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsController : MonoBehaviour {

	public void OpenLink(string url){
		Application.OpenURL(url);
	}
}
