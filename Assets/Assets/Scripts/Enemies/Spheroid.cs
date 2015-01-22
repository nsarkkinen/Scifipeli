using UnityEngine;
using System.Collections;

public class Spheroid : Enemy
{
	// Armor object
	GameObject armor;

	void Start()
	{
		base.Start();

		// Get the armor object
		armor = transform.Find("Armor").gameObject;
	}

	void Update()
	{
		base.Update();

		// Rotate armot object
		armor.transform.Rotate(new Vector3(Time.deltaTime*-500.0f, Time.deltaTime*300.0f, Time.deltaTime*-300.0f));
	}
}
