using UnityEngine;
using System.Collections;

public class GameStats : MonoBehaviour {

	public static int experience;
	public static int level;
	public static int shieldLevel;
	public static int weaponsLevel;
	public static int engineLevel;

	static int levelCurve = 50;

	void Start()
	{
		GameStats.experience = 0;
		GameStats.level = 1;
		GameStats.shieldLevel = 1;
		GameStats.weaponsLevel =1;
		GameStats.engineLevel = 1;
	}

	// Checks if player has leveled up and returns true if so
	public static bool CheckLevel()
	{
		// Calculates the level based on current exp
		int checkedLevel = Mathf.FloorToInt((GameStats.levelCurve + Mathf.Sqrt(GameStats.levelCurve*GameStats.levelCurve - 4*GameStats.levelCurve *-GameStats.experience))/(2*GameStats.levelCurve));

		// If level has changed call the method
		if (checkedLevel != GameStats.level)
		{
			GameStats.LevelUp(checkedLevel);
			return true;
		}
		return false;
	}

	public static void LevelUp(int newLevel)
	{
		GameStats.level = newLevel;
	}

	// Function to calculate how much exp is required to reach inquired level
	public static int RequiredExpForLevel(int lvl)
	{
		int req = GameStats.levelCurve*lvl*lvl - (GameStats.levelCurve*lvl);
		return req;
	}
}
