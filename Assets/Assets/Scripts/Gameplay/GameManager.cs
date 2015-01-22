using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	public static bool bPaused;

	void Start()
	{
		bPaused = true;
	}

	public void TogglePause()
	{
		bPaused = !bPaused;
	}
}
