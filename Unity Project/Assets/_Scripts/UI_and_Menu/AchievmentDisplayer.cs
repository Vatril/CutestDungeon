using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AchievmentDisplayer : MonoBehaviour {

	public DisplayAchivement template;

	private RectTransform self;
	private bool askSure;

	// Use this for initialization
	void Start () {
		self = GetComponent<RectTransform> ();
		SetUp ();
	}
	
	private void SetUp(){
		int off = 0;
		self.sizeDelta = new Vector2 (0, 0);
		foreach(AchievementManager.Achievement ach in AchievementManager.achievementList){
			DisplayAchivement di = Instantiate (template, this.transform) as DisplayAchivement;
			di.transform.localPosition = new Vector3 (di.transform.localPosition.x, -((off++ * 70) + 35), di.transform.localPosition.z);
			di.SetParameters (ach.name, ach.completeText, AchievementManager.IsAchievementDone(ach.logicalName));
			self.sizeDelta = new Vector2 (0, self.rect.height + 70);
		}
	}

	public void Reset(Text t){
		if (!askSure) {
			askSure = true;
			t.text = "Are you sure?";
		} else {
			askSure = false;
			foreach(DisplayAchivement ad in transform.GetComponentsInChildren<DisplayAchivement>()){
				Destroy (ad.gameObject);
			}
			t.text = "Reset";
			AchievementManager.Reset ();
			SetUp ();
		}
	}
}
