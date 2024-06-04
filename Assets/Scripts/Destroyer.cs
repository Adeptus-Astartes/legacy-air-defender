using UnityEngine;
using System.Collections;

public class Destroyer : MonoBehaviour 
{
	//Simple timer, when time < 0 destroy game object
	public float time;

	void Update () 
	{
		time -= Time.deltaTime;
		if(time<0)
		{
			Destroy(gameObject);
		}
	}
}
