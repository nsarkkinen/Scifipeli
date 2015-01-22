using UnityEngine;
using System.Collections;

public class ShipController : MonoBehaviour {

	// Script holding the game area dimensions
	public GameArea area;
	public Status status;

	// Weapons
	GameObject rGun;
	GameObject lGun;

	// Standard bullet
	public GameObject standardBullet;

	// Stats
	public int sensitivity;
	public float reloadTime;
	public int health;
	public int shield;
	public int shieldRegen;
	[HideInInspector]
	public int maxHealth;
	[HideInInspector]
	public int maxShield;

	// Tracking temporary variables
	float lastShotTime;
	float tempRegen;

	// Upgraded values
	int actualSensitivity;
	[HideInInspector]
	public int actualMaxShield;
	int actualShieldRegen;
	float actualReload;
	float actualDamageMultiplier;
	float actualVelocityMultiplier;


	void Start()
	{
		lastShotTime = Time.time;
		tempRegen = 0.0f;
		maxHealth = health;
		maxShield = shield;
		UpdateValues();
	}

	void Update ()
	{
		// Pause check
		if (GameManager.bPaused)
		{
			return;
		}

		// Calling ship movement method
		Controls();

		if (Input.GetButton("Fire1"))
		{
			Fire();
		}

		RegenShield();
	}

	void OnTriggerEnter(Collider col)
	{
		if (col.tag == "enemy_bullet" || col.tag == "enemy")
		{
			// Damage if colliding with enemy
			int damage = 50;

			// Damage if colliding with bullet
			if (col.tag == "enemy_bullet")
			{
				damage = col.GetComponent<Bullet>().damage;
			}

			// Destroy enemy or bullet
			Destroy(col.gameObject);

			TakeDamage(damage);
		}
	}

	public void UpdateValues()
	{
		// Shield
		actualMaxShield = (int)(maxShield * (1.0f + ((float)GameStats.shieldLevel - 1.0f)/5.0f));
		actualShieldRegen = (int)(shieldRegen * (1.0f + ((float)GameStats.shieldLevel - 1.0f)/10.0f));

		// Weapons
		actualReload = reloadTime/(1.0f + (GameStats.weaponsLevel-1.0f)/5.0f);
		actualDamageMultiplier = 1.0f + (GameStats.weaponsLevel-1.0f)/5.0f;
		actualVelocityMultiplier = 1.0f + (GameStats.weaponsLevel-1.0f)/10.0f;

		// Engines
		actualSensitivity = (int)(sensitivity * (1.0f + ((float)GameStats.engineLevel - 1.0f)/10.0f));
	}

	// Basic movement controls
	void Controls()
	{
		// Get movement input
		float horizontalValue = Input.GetAxis ("Horizontal") * (float) actualSensitivity/100;
		float verticalValue = Input.GetAxis ("Vertical") * (float) actualSensitivity/100;;

		// Set new coordinates for the ship
		float newHorizontalValue = transform.position.x + horizontalValue;
		float newVerticalValue = transform.position.z + verticalValue;

		// Make sure ship stays inside the playable area
		if (newHorizontalValue <= -area.areaWidth/2)
		{
			newHorizontalValue = -area.areaWidth/2;
		}
		
		if (newHorizontalValue >= area.areaWidth/2)
		{
			newHorizontalValue = area.areaWidth/2;
		}
		
		if (newVerticalValue <= -area.areaHeight/2)
		{
			newVerticalValue = -area.areaHeight/2;
		}

		if (newVerticalValue >= area.areaHeight/2)
		{
			newVerticalValue = area.areaHeight/2;
		}

		// Move the ship
		transform.position = new Vector3(newHorizontalValue, transform.position.y, newVerticalValue);
	}

	// Firing the weapons
	void Fire()
	{
		// Getting weapons
		if (rGun == null) {rGun = gameObject.transform.Find("Wing/Engine_r/gun_r").gameObject;}
		if (lGun == null) {lGun = gameObject.transform.Find("Wing/Engine_l/gun_l").gameObject;}

		if (lGun != null && rGun != null)
		{
			// Weapon reload time checked
			if (Time.time > lastShotTime + actualReload)
			{
				// Instantiating bullets
				GameObject bullet1 = Instantiate(standardBullet, lGun.transform.position, transform.rotation) as GameObject;
				Bullet bulletScript1 = bullet1.GetComponent<Bullet>();
				bulletScript1.damage = (int)(bulletScript1.damage * actualDamageMultiplier);
				bulletScript1.velocity = (int)(bulletScript1.velocity * actualVelocityMultiplier);

				GameObject bullet2 = Instantiate(standardBullet, rGun.transform.position, transform.rotation) as GameObject;
				Bullet bulletScript2 = bullet2.GetComponent<Bullet>();
				bulletScript2.damage = (int)(bulletScript2.damage * actualDamageMultiplier);
				bulletScript2.velocity = (int)(bulletScript2.velocity * actualVelocityMultiplier);

				lastShotTime = Time.time;
			}
		}
	}

	void TakeDamage(int damage)
	{
		// The damage not blocked by shield
		int penetrated = 0;

		// Damaging shield
		if (shield > 0)
		{
			penetrated = damage - shield;
			shield -= damage;

			if (shield <= 0)
			{
				shield = 0;
			}

			if (penetrated <= 0)
			{
				return;
			}
		}

		// Damaging hull
		health -= penetrated;

		// To avoid healthbar problems
		if (health <= 0)
		{
			health = 0;
		}

		status.UpdateHealthNShield();

		// Kill player if health 0
		if (health == 0)
		{
			Debug.Log ("Player dies");
			Destroy(gameObject);
		}
	}

	void RegenShield()
	{
		// Regen only if damaged
		if (shield < actualMaxShield)
		{
			// Regenerating each frame even if the step is smaller than 1 (Shield value is integer)
			tempRegen += actualShieldRegen*Time.deltaTime;

			// Actually regen if the total regen is more than 1
			if (tempRegen >= 1.0f)
			{
				int realRegen = Mathf.FloorToInt(tempRegen);
				shield += realRegen;

				// Reduce regenerated amount from running counter
				tempRegen -= realRegen;
			}
			status.UpdateHealthNShield();
		}
	}
}
