using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelGrayer : MonoBehaviour {

	public AchievementManager.AchievementName unlockname;


	public bool isUnlocked = false;

	// Use this for initialization
	void Start () {
		if (!AchievementManager.IsAchievementDone (unlockname)) {
			foreach (MeshRenderer rend in GetComponentsInChildren<MeshRenderer>()) {
				foreach (Material mat in rend.materials) {
					var avg = (mat.color.r + mat.color.g + mat.color.b) / 3;
					mat.color = new Color (avg,avg,avg);
				}
			}
		} else {
			isUnlocked = true;
		}
	}
}
