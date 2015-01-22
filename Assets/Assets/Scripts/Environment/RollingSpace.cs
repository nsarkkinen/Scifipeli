using UnityEngine;
using System.Collections;

public class RollingSpace : MonoBehaviour {

	float offset;
	// Use this for initialization
	void Start ()
	{
		offset = 0.0f;
	}
	
	// Update is called once per frame
	void Update ()
	{
		// Pause check
		if (GameManager.bPaused)
		{
			return;
		}

		offset -= 0.2f*Time.deltaTime;

		renderer.material.SetTextureOffset("_MainTex", new Vector2(0, offset));

		if (offset <= -100.0f)
		{
			offset = 0.0f;
		}
	}
}
