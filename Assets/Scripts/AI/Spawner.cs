using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Wave//Wave settings
{
	public int count;

	public Humanoid prefab;

	public WaypointPath path;

	public bool atSameTime = false;

	public float spawnInterval = 0.5f;

	public float timeToNextWave = 5;
}

public class Spawner : MonoBehaviour 
{
	public bool canSpawn = true;
	public bool infinite = false;
	public List<Wave> waves;//Waves List

	public int currentWave = 0;
	[HideInInspector]
	float tempTime = 0;
	[HideInInspector]
	float tempIntervalTime = 0;
	[HideInInspector]
	int tempSpawnedUnits;

	void Update() //Spawn
	{
		if(canSpawn)
		{
			tempTime += Time.deltaTime;//Add time

			if(currentWave < waves.Count)
			{

				if(tempTime > waves[currentWave].timeToNextWave)
				{
					if(waves[currentWave].atSameTime)
					{
						//Spawn
						for(int i = 0;i<waves[currentWave].count;i++)//Spawn and set path for Humanoid.cs
						{
							GameObject _humanoid = Instantiate(waves[currentWave].prefab.gameObject,transform.position,transform.rotation) as GameObject;
							_humanoid.GetComponent<Humanoid>().myPath = waves[currentWave].path;
						}

						tempTime = 0;
						currentWave ++;
					}
					else
					{
						tempIntervalTime += Time.deltaTime;
						if(tempIntervalTime > waves[currentWave].spawnInterval)
						{
							if(tempSpawnedUnits < waves[currentWave].count)
							{
								GameObject _humanoid = Instantiate(waves[currentWave].prefab.gameObject,transform.position,transform.rotation) as GameObject;
								_humanoid.GetComponent<Humanoid>().myPath = waves[currentWave].path;

								tempSpawnedUnits ++;
								tempIntervalTime = 0;

								if(tempSpawnedUnits == waves[currentWave].count)
								{
									tempTime = 0;
									currentWave ++;
									tempSpawnedUnits = 0;
								}
							}
						}
					}
				}

			}
			else
			{
				if(infinite)
				{
					print("restart");
					currentWave = 0;
				}
				else
				{
					canSpawn = false;
					print("finish");
				}
			}
		}
	}
}
