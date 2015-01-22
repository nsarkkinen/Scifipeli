using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Status : MonoBehaviour {

	public ShipController ship;

	// UI elements
	public GameObject healthBar;
	public GameObject healthText;
	public GameObject shieldBar;
	public GameObject shieldText;
	public GameObject expBar;
	public GameObject levelText;
	public GameObject levelUp;
	public GameObject score;

	void Start()
	{
	//	UpdateHealthNShield();
		UpdateExp();
		UpdateLevel();
	}

	public void UpdateHealthNShield()
	{
		healthBar.transform.localScale = new Vector3(1, (float)ship.health/ship.maxHealth, 1);
		shieldBar.transform.localScale = new Vector3(1, (float)ship.shield/ship.actualMaxShield, 1);

		healthText.GetComponent<Text>().text = ship.health.ToString();
		shieldText.GetComponent<Text>().text = ship.shield.ToString();
	}

	public void UpdateExp()
	{
		// Making sure we're on the correct level
		bool bLevelUp = GameStats.CheckLevel();

		// Scaling value for UI element indicating exp towards new level
		float expScale = (float)(GameStats.experience-GameStats.RequiredExpForLevel(GameStats.level))/(GameStats.RequiredExpForLevel(GameStats.level+1)-GameStats.RequiredExpForLevel(GameStats.level));

		// Applying scale
		expBar.transform.localScale = new Vector3(expScale, 1, 1);

		// Updating score
		score.GetComponent<Text>().text = GameStats.experience.ToString();

		// Keep level updated
		UpdateLevel();

		if (bLevelUp)
		{
			LevelUp();
		}
	}

	public void UpdateLevel()
	{
		levelText.GetComponent<Text>().text = "Level " + GameStats.level;
	}

	void LevelUp()
	{
		// Activate button for leveling up
		levelUp.gameObject.SetActive(true);
	}
}
