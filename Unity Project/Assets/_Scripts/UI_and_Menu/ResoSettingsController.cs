using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ResoSettingsController : MonoBehaviour {


	public Dropdown resolution;
	public Dropdown quality;

	private bool skip2 = true;
	private bool skip3 = true;

	// Use this for initialization
	void Start () {
		int curres = 0;
		List<string> resos = new List<String> ();
		var res = Screen.resolutions;
		Debug.LogError (Screen.width);
		for (int i = 0; i < res.Length; i++) {
			resos.Add (string.Format("{0}x{1}@{2}Hz", res[i].width, res[i].height,res[i].refreshRate));
			if (res[i].width == Screen.width) {
				Debug.LogError (i);
				curres = i;
			}
		}
		resolution.AddOptions (resos);

		List<string> quals = new List<String> ();
		foreach (string qual in QualitySettings.names) {
			quals.Add (qual);
		}
		quality.AddOptions (quals);
		quality.value = QualitySettings.GetQualityLevel ();
		resolution.value = curres;
	}

	public void ToggleWindowed(){
		Screen.fullScreen = !Screen.fullScreen;
	}

	public void ChangeResolution(){
		if (skip2) {
			skip2 = false;
			return;
		}
		Screen.SetResolution(Screen.resolutions[resolution.value].width,
			Screen.resolutions[resolution.value].height,
			Screen.fullScreen,
			Screen.resolutions[resolution.value].refreshRate);
	}

	public void ChangeQuality(){
		if (skip3) {
			skip3 = false;
			return;
		}
		QualitySettings.SetQualityLevel(quality.value, true);
	}
}
