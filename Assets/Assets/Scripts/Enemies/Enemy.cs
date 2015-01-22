using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

	// Stats
	public int expGain;
	public float velocity;
	public int health;
	public float reloadTime;
	
	public GameObject bullet;




	protected GameArea area;
	Status status;

	float lastShotTime;


	protected void Start ()
	{
		area = GameObject.Find("GameArea").GetComponent<GameArea>();
		status = GameObject.Find("Status").GetComponent<Status>();
		lastShotTime = 0.0f;
	}

	protected void Update ()
	{
		// Pause check
		if (GameManager.bPaused)
		{
			return;
		}

		// Move enemy down the screen
		transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - velocity*Time.deltaTime);

		if (transform.position.z > area.areaHeight*2.0f || transform.position.z < -area.areaHeight*2.0f)
		{
			Destroy(gameObject);
		}

		// Fire
		Fire();
	}

	void OnTriggerEnter(Collider col)
	{
		if (col.tag == "player_bullet")
		{
			// Reduce health
			int damage = col.GetComponent<Bullet>().damage;
			health -= damage;

			// Destroy bullet
			Destroy(col.gameObject);

			// Destroy enemy if health 0
			if (health <= 0)
			{
				GameStats.experience += expGain;
				status.UpdateExp();

				Destroy(gameObject);
			}
		}
	}

	void Fire()
	{
		if (Time.time > lastShotTime + reloadTime)
		{
			// Instantiating bullets
			GameObject bullet1 = Instantiate(bullet, transform.position, transform.rotation) as GameObject;
			
			lastShotTime = Time.time;
		}
	}
}
