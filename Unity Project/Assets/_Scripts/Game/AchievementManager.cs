using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class AchievementManager {

	private static bool[] achievements;
	private const string saveName = "Save.json";

	public static readonly Achievement[] achievementList = new Achievement[]{
		new Achievement(AchievementName.DieOnce,"Die your first time", "Health Potion"),
		new Achievement(AchievementName.CompleteLevel,"Complete a Level", "Mana Potion"),
		new Achievement(AchievementName.KillABoss,"Kill a Boss", "Regin"),
		new Achievement(AchievementName.Get12Hearts,"Get 12 Heart Containers", "Nigig"),
		new Achievement(AchievementName.KillAllBosses,"Defeat all Bosses in one Run", "Rain"),
		new Achievement(AchievementName.Kill100Enemies,"Kill 100 Enemies in one Run", "Regeneration Potion"),
		new Achievement(AchievementName.Kill250Enemies,"Kill 250 Enemies in one Run", "Invincibility Potion"),
		new Achievement(AchievementName.Kill500Enemies,"Kill 500 Enemies in one Run", "Nomsi"),
		new Achievement(AchievementName.LooseAHeart,"Loose 1 full heart in one attack", "Strength Potion"),
		new Achievement(AchievementName.FinishIn3,"Finish a Dungeon in under 3 minutes", "Speed Potion"),
		new Achievement(AchievementName.FinishAll,"Survive the last stand", "An actual sense of pride and accomplishment")
	};

	private static void InitIfNotInit(){
		if (achievements != null)
			return;
		string filePath = Path.Combine(Application.persistentDataPath, saveName);
		if (File.Exists (filePath)) {
			achievements = JsonUtility.FromJson<SaveContainer>(File.ReadAllText (filePath)).achievements;
			if (achievements.Length > 0) {
				return;
			}
		}
		achievements = new bool[Enum.GetNames(typeof(AchievementName)).Length];

	}

	private static void Save(){
		File.WriteAllText (Path.Combine(Application.persistentDataPath, saveName), JsonUtility.ToJson(new SaveContainer(achievements)));
	}

	public static bool IsAchievementDone(AchievementName name){
		InitIfNotInit ();
		return achievements [(int)name];
	}

	public static bool CompleteAchievement(AchievementName name){
		InitIfNotInit ();
		var rval = achievements [(int)name];
		achievements [(int)name] = true;
		Save ();
		return !rval;
	}

	public enum AchievementName{
		DieOnce = 0,
		KillAllBosses = 1,
		KillABoss = 2,
		Get12Hearts = 3,
		Kill100Enemies = 4,
		Kill250Enemies = 5,
		Kill500Enemies = 6,
		LooseAHeart = 7,
		FinishIn3 = 8,
		CompleteLevel = 9,
		FinishAll = 10
	}

	public class Achievement{
		public AchievementName logicalName;
		public string name;
		public string completeText;

		public Achievement(AchievementName lo, string na, string te){
			logicalName = lo;
			name = na;
			completeText = te;
		}
	}

	[System.Serializable]
	private class SaveContainer{
		public bool[] achievements;

		public SaveContainer(bool[] ach){
			achievements = ach;
		}
	}

	public static void Reset(){
		achievements = new bool[Enum.GetNames(typeof(AchievementName)).Length];
		Save ();
	}
			
}
