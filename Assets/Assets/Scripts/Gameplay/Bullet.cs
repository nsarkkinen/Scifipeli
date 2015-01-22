using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

	// Script holding the game area dimensions
	GameArea area;

	// Bullet variables
	public float velocity;
	public int damage;

	void Start()
	{
		area = GameObject.Find("GameArea").GetComponent<GameArea>();
	}

	// Update is called once per frame
	void Update ()
	{
		// Pause check
		if (GameManager.bPaused)
		{
			return;
		}

		// Moving the bullet each frame;
		transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + velocity*Time.deltaTime);

		if (transform.position.z > area.areaHeight || transform.position.z < -area.areaHeight)
		{
			Destroy(gameObject);
		}
	}
}
