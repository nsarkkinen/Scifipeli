using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LevelUpButtons : MonoBehaviour {

	public Text pointsText;
	public Text shieldText;
	public Text engineText;
	public Text weaponText;
	public ShipController ship;
	int pointsToSpend;

	void OnEnable()
	{
		// Retrieving all data when opening the panel first time
		RefreshPoints();
	}

	void RefreshPoints()
	{
		// Getting required stats from GameStats
		pointsToSpend = GameStats.level - GameStats.shieldLevel - GameStats.engineLevel - GameStats.weaponsLevel + 2;
		pointsText.text = "Points: " + pointsToSpend;
		shieldText.text = "Shield: " + GameStats.shieldLevel;
		weaponText.text = "Weapons: " + GameStats.weaponsLevel;
		engineText.text = "Engine: " + GameStats.engineLevel;
	}

	// To be accessed from Unity Editor's GUI-system
	public void LevelUpSelected(string upgType)
	{
		switch (upgType)
		{
		case "shield":
			GameStats.shieldLevel += 1;
			break;
		case "weapons":
			GameStats.weaponsLevel += 1;
			break;
		case "engine":
			GameStats.engineLevel += 1;
			break;
		}

		// Refresh stats after selecting an upgrade
		RefreshPoints();

		// Only close if player has spent all points
		if (pointsToSpend <= 0)
		{
			GameManager.bPaused = false;
			ship.UpdateValues();
			gameObject.SetActive(false);
		}
	}
}
