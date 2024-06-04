//Load Scene

using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Loading : MonoBehaviour {
	public string SceneNameToLoad; 
	
	AsyncOperation menuSync;
	public Slider progressBar;
	public List<string> tips;
	public Text tipLabel;
	private int tipId;

	void Awake()
	{
		tipId = Random.Range(0,tips.Count);
		tipLabel.text = tips[tipId];
		SceneNameToLoad = PlayerPrefs.GetString ("LoadScene");
	}
	
	IEnumerator Start () 
	{
		menuSync = Application.LoadLevelAsync(SceneNameToLoad);
		yield return menuSync;

	}
	
	
	void Update()
	{
		if (!menuSync.isDone && progressBar != null)
		{
			progressBar.value = menuSync.progress;
		}
		
	}
	
	void Load()
	{
		var async = Application.LoadLevelAsync(SceneNameToLoad);
		while (!async.isDone) 
		{
			Debug.Log("%: " + async.progress);
		}
		return;
	}
	
	
	IEnumerator LoadLevelWithProgress (string levelToLoad) 
	{
		var async = Application.LoadLevelAsync(levelToLoad);
		Debug.Log("%: " + async.progress);
		while (!async.isDone) 
		{
			Debug.Log("%: " + async.progress);
			yield return async;
		}
	}
	
	
}
