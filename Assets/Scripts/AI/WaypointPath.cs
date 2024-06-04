using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WaypointPath : MonoBehaviour 
{
	public bool autoFill = false;
	public List<Vector3> waypoints;

	void Start()
	{
		if(autoFill)
		{
			foreach(Transform child in transform)
			{
				waypoints.Add(child.position);
			}
		}
	}
}
