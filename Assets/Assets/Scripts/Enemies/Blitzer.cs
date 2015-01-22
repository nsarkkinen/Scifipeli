using UnityEngine;
using System.Collections;

public class Blitzer : Enemy
{

	// Use this for initialization
	void Start ()
	{
		base.Start();
	}
	
	// Update is called once per frame
	void Update ()
	{
		// Pause check
		if (GameManager.bPaused)
		{
			return;
		}

		base.Update();

		// Move enemy across the screen
		transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - velocity*Time.deltaTime);
	}
}
