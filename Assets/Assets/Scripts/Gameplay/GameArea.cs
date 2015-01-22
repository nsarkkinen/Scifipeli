using UnityEngine;
using System.Collections;

public class GameArea : MonoBehaviour {

	public float areaWidth;
	public float areaHeight;

	void OnDrawGizmos()
	{
		Gizmos.color = Color.white;
		Gizmos.DrawWireCube(transform.position, new Vector3(areaWidth, 0, areaHeight));
	}
}
