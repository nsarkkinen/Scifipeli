using UnityEngine;
using System.Collections;

public class EnemySpawn : MonoBehaviour {

	// Spawn area dimensions
	public float areaWidth;
	public float areaHeight;

	// Time when last enemy spawned
	float lastSpawnTime;

	[HideInInspector]
	public float spawnRate = 1.0f;

	// Array of enemy prefabs
	public GameObject[] enemies;
	
	void OnDrawGizmos()
	{
		Gizmos.color = Color.white;
		Gizmos.DrawWireCube(transform.position, new Vector3(areaWidth, 0, areaHeight));
	}

	void Start()
	{
		lastSpawnTime = 0.0f;
	}

	void Update()
	{
		// Pause check
		if (GameManager.bPaused)
		{
			return;
		}

		Spawn();
	}

	void Spawn()
	{
		// check lastSpawnTime
		if (Time.time > spawnRate + lastSpawnTime)
		{
			// Randoming spawn percentage
			float rnd = Random.Range(0.0f, 100.0f);
			int enemyTier = GameStats.level/3;
			int enemyLevel = 0;

			if (spawnRate > rnd)
			{
				// Randoming spawn location
				float pos = Random.Range(-areaWidth/2, areaWidth/2);

				// Choosing enemy to spawn
				float rndSpawn = Random.Range(0.0f, 1.0f);

				if (rndSpawn<0.1f)
				{
					enemyLevel = enemyTier-3;
				}

				else if (rndSpawn<0.3f)
				{
					enemyLevel = enemyTier-2;
				}
				else if (rndSpawn<0.6f)
				{
					enemyLevel = enemyTier-1;
				}
				else
				{
					enemyLevel = enemyTier;
				}

				if (enemyLevel < 0){enemyLevel = 0;}
				if (enemyLevel >= enemies.Length){enemyLevel = enemies.Length-1;}

				// Instatiating enemy
				GameObject enemy1 = Instantiate(enemies[enemyLevel], new Vector3(pos, 2, transform.position.z), transform.rotation) as GameObject;
				lastSpawnTime = Time.time;
			}
		}
	}
}
