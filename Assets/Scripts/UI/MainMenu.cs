using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class MainMenu : MonoBehaviour
{
	public Transform MainMenuBlock;
	public Transform TopScreen;
	public Transform OptionsBlock;
	public Transform LevelsBlock;

	[Header("Windows")]

	public Transform ShopBlock;
	public Transform ShopConfirmationWindow;
	public Transform EnoughtMoneyWindow;
	public Transform ExitDiagWindow;
	public Transform CleatDataDiagWindow;

	[Header("UI")]
	public Text t_money;

	[HideInInspector]
	public Item ConversationWith;

	public List<GameObject> levels;
	public UILevel selectedLevel;

	[Header("Options UI")]
	public Toggle nightVisionColor;
	public Toggle invertX;
	public Toggle invertY;

	[HideInInspector]
	bool OptionsWindowOpened = false;
	[HideInInspector]
	bool ShopWindowOpened = false;


	public List<Item> items;

	[Header("Audio")]
	public AudioClip clickClip;

	void Start()
	{
		ReloadUI();
	}

	private void ReloadUI()
	{
		if(PlayerPrefs.GetString("IsFirstLaunch") == "False")
		{
			t_money.text = PlayerPrefs.GetInt("Money").ToString(); 
			
			//Set-Up SavedOptions
			nightVisionColor.isOn = (PlayerPrefs.GetString("NightVisionColor") == "True");
			invertX.isOn = (PlayerPrefs.GetString("InvertX") == "True");
			invertY.isOn = (PlayerPrefs.GetString("InvertY") == "True");
		}
		else
		{
			PlayerPrefs.SetInt("Money",3000);//Add Money
			PlayerPrefs.SetString("Minigun","True");//Add FirstWeapon
			
			t_money.text = PlayerPrefs.GetInt("Money").ToString(); 
			PlayerPrefs.SetString("IsFirstLaunch","False");
		}

		t_money.text = PlayerPrefs.GetInt("Money").ToString(); 
		foreach(Item _item in items)
		{
			_item.SendMessage("LoadData");
		}
	}

	public void CallFunctionFromUI(string value) // UI callbacks, calls when player click to any button
	{
		if(value == "OpenLevelBlock")
		{
			MainMenuBlock.SendMessage("TweenPlay",false);
			TopScreen.SendMessage("TweenPlay",true);

			if(OptionsWindowOpened)
			{
			    OptionsBlock.SendMessage("TweenPlay",false);
			}

			LevelsBlock.gameObject.SetActive(true);
		}
		if(value == "OpenMenuBlock")
		{
			MainMenuBlock.SendMessage("TweenPlay",true);
			TopScreen.SendMessage("TweenPlay",false);
			LevelsBlock.gameObject.SetActive(false);
			if(OptionsWindowOpened)
			{
				OptionsWindowOpened = false;
			}

			if(ShopWindowOpened)
			{
				ShopBlock.SendMessage("TweenPlay",false);
				ShopWindowOpened = false;
			}
		}
		if(value == "OpenOptions")
		{
			if(!OptionsWindowOpened)
			{
		    	OptionsBlock.SendMessage("TweenPlay",true);

				OptionsWindowOpened = true;
			}
			else
			{
				OptionsBlock.SendMessage("TweenPlay",false);
				OptionsWindowOpened = false;
			}
		}
		if(value == "CloseOptions")
		{
			OptionsBlock.SendMessage("TweenPlay",false);
		}
		if(value == "OpenShopBlock")
		{
			if(!ShopWindowOpened)
			{
			    ShopBlock.SendMessage("TweenPlay",true);
				ShopWindowOpened = true;
			}
			else
			{
				ShopBlock.SendMessage("TweenPlay",false);
				ShopWindowOpened = false;
			}
		}

		if(value == "CloseShopBlock")
		{
			ShopBlock.SendMessage("TweenPlay",false);
			ShopWindowOpened = false;
		}


		if(value == "OpenClearUserDataDiagWindow")
		{
			CleatDataDiagWindow.gameObject.SetActive(true);
		}

		if(value == "CleanUserData")
		{
			this.GetComponent<PlayerPrefsCleaner>().enabled = true;
			ReloadUI();
			CleatDataDiagWindow.gameObject.SetActive(false);
		}
		if(value == "Exit")
		{
			Application.Quit();
			print("Quit");
		}

	}
	//Options
	public void ChangeNightVisionColor()//Change camera color
	{
		PlayerPrefs.SetString("NightVisionColor",nightVisionColor.isOn.ToString());//True = green | False = Dark
		print(PlayerPrefs.GetString("NightVisionColor"));
	}

	public void ChangeInvertX()
	{
		PlayerPrefs.SetString("InvertX",invertX.isOn.ToString());
		print(PlayerPrefs.GetString("InvertX"));
	}

	public void ChangeInvertY()
	{
		PlayerPrefs.SetString("InvertY",invertY.isOn.ToString());
		print(PlayerPrefs.GetString("InvertY"));
	}


	public void ShowLevel(string value)//Calls when player click on yellow button
	{
		foreach(GameObject level in levels)
		{
			if(level.name == value)
			{
				level.SetActive(true);
				selectedLevel = level.GetComponent<UILevel>();
			}
			else
			{
				level.SetActive(false);
			}
		}
	}

	public void PlayAudio(string value)
	{
		if(value == "Click")
		{
			audio.PlayOneShot(clickClip);
		}
	}


	public void Play()//Calls when player click on play button,
	{
		if(selectedLevel!= null)
		{
			PlayerPrefs.SetString("LoadScene",selectedLevel.levelName);
			Application.LoadLevelAsync("Loading");
		}
	}

	public void GetRequest(Item m_item,bool canPurchase)//Calls from Item.cs when player want upgrade/buy button
	{
		ConversationWith = m_item;

		if(canPurchase)
		   ShopConfirmationWindow.gameObject.SetActive(true);
		else
			EnoughtMoneyWindow.gameObject.SetActive(true);
	}
	//Dialog window
	public void ShopDialogWindowCallback(string value)
	{
		if(value == "Confirm")
		{
			ConversationWith.PositiveResponse();
			ShopConfirmationWindow.gameObject.SetActive(false);
		}

		if(value == "NotConfirm")
		{
			ConversationWith = null;
			ShopConfirmationWindow.gameObject.SetActive(false);
		}
	}
}
