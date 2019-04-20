using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class AchievementTracker : MonoBehaviour {

	public static AchievementTracker instance;

	public DisplayAchivement display;

	private int eneKilled;

	private float offset;
	private float leveltime;

	void Awake(){
		if (instance == null) {
			DontDestroyOnLoad (this.gameObject);
			instance = this;
		} else if (instance != this) {
			Destroy (this.gameObject);
		}
	}

	// Use this for initialization
	void Start () {
		
	}
	
	public void SetLevelStartTime(){
		leveltime = Time.time;
	}

	public void CheckHearts(int maxHealth){
		if (maxHealth >= 4*12) {
			if (AchievementManager.CompleteAchievement (AchievementManager.AchievementName.Get12Hearts)) {
				ShowCompleted (AchievementManager.AchievementName.Get12Hearts);
			}
		}
	}

	public void EnemyKilled(){
		eneKilled++;
		if (eneKilled >= 100) {
			if (AchievementManager.CompleteAchievement (AchievementManager.AchievementName.Kill100Enemies)) {
				ShowCompleted (AchievementManager.AchievementName.Kill100Enemies);
			}
		}
		if (eneKilled >= 250) {
			if (AchievementManager.CompleteAchievement (AchievementManager.AchievementName.Kill250Enemies)) {
				ShowCompleted (AchievementManager.AchievementName.Kill250Enemies);
			}
		}
		if (eneKilled >= 500) {
			if (AchievementManager.CompleteAchievement (AchievementManager.AchievementName.Kill500Enemies)) {
				ShowCompleted (AchievementManager.AchievementName.Kill500Enemies);
			}
		}
	}

	public void ChangeLevel(){
		if (AchievementManager.CompleteAchievement (AchievementManager.AchievementName.CompleteLevel)) {
			ShowCompleted (AchievementManager.AchievementName.CompleteLevel);
		}

		if (Time.time - leveltime <= (3*60)) {
			if (AchievementManager.CompleteAchievement (AchievementManager.AchievementName.FinishIn3)) {
				ShowCompleted (AchievementManager.AchievementName.FinishIn3);
			}
		}

	}

	public void GetDamage(int d){
		if (d >= 4) {
			if (AchievementManager.CompleteAchievement (AchievementManager.AchievementName.LooseAHeart)) {
				ShowCompleted (AchievementManager.AchievementName.LooseAHeart);
			}
		}
	}

	public void Die(){
		if (AchievementManager.CompleteAchievement (AchievementManager.AchievementName.DieOnce)) {
			ShowCompleted (AchievementManager.AchievementName.DieOnce);
		}
	}

	public void LastStandEnd(){
		if (AchievementManager.CompleteAchievement (AchievementManager.AchievementName.FinishAll)) {
			ShowCompleted (AchievementManager.AchievementName.FinishAll);
		}
	}

	public void BossKilled(){
		if (AchievementManager.CompleteAchievement (AchievementManager.AchievementName.KillABoss)) {
			ShowCompleted (AchievementManager.AchievementName.KillABoss);
		}
		foreach (bool b in GameController.maincontroller.bosskilled) {
			if (!b)
				return;
		}
		if (AchievementManager.CompleteAchievement (AchievementManager.AchievementName.KillAllBosses)) {
			ShowCompleted (AchievementManager.AchievementName.KillAllBosses);
		}
	}

	private void ShowCompleted(AchievementManager.AchievementName name){
		var dis = Instantiate (display, this.transform);
		dis.transform.position = new Vector3 (dis.transform.position.x,dis.transform.position.y + offset ,dis.transform.position.z);
		offset += dis.GetComponent<RectTransform> ().rect.height * 1.5f;
		AchievementManager.Achievement a = AchievementManager.achievementList.Single(ach => ach.logicalName == name);
		dis.SetParameters(a.name, a.completeText, true);
		StartCoroutine (KillIn5(dis));
	}

	private IEnumerator KillIn5(DisplayAchivement display){
		yield return new WaitForSeconds (5);
		offset -= (float) display.GetComponent<RectTransform> ().rect.height * 1.5f;
		Destroy (display.gameObject);
	}
}
