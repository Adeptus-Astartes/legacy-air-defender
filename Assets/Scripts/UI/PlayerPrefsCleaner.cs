using UnityEngine;
using System.Collections;
//Clean all player data or some key
public class PlayerPrefsCleaner : MonoBehaviour 
{
	public string key;

	void Start()
	{
		this.enabled = false;
	}

	void OnEnable()
	{
		if(key != "")
		{
			PlayerPrefs.DeleteKey(key);
		}
		else
		{
	    	PlayerPrefs.DeleteAll();
		}
		print("PlayerPrefs.cs : Cleaned!");
	}
}
