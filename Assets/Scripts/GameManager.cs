using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public enum GunCaliber
{
    Small,
    Middle,
    Huge            
}

public class GameManager : MonoBehaviour
{
    public Airplane airplane; //Airplane Link

    [Header("GameScores")]
    public int totalKill;
    public int humanKill;
    public int maxHumanKill;
	public int humanSaved;



	[Header("UI Setup")] // Links for UI Elemnets
    public Text t_totalKill;
    public Text t_humanKill;
    public Text t_moneyEarned;
	public Text t_humanSaved;
	public Image i_deadScreen;
    [Space(10)]
    public Button smallCallibreButton;
	public Button middleCallibreButton;
	public Button hugeCallibreButton;
	[Space(10)]
	public GameObject menuPause;
	public List<GameObject> inGameUIElemnts;

	private bool defeat = false;

	private int tempMoney = 0; 
	private int tempPlayerEarn = 0;

    void Start()
    {
		tempMoney = PlayerPrefs.GetInt("Money"); 

        t_totalKill.text = "Kills : 0";
        t_humanKill.text = "0/" + maxHumanKill.ToString();
		t_moneyEarned.text = "Coins : " + tempPlayerEarn.ToString();

		ReadPlayerBoughtItems();
    }

	void ReadPlayerBoughtItems() //Load and check player items
	{
		if(PlayerPrefs.GetString("MiddleGun") == "True")
		{
			middleCallibreButton.interactable = true;
			middleCallibreButton.transform.FindChild("Text").gameObject.SetActive(true);
			middleCallibreButton.transform.FindChild("Locked").gameObject.SetActive(false);
		}
		else
		{
			middleCallibreButton.interactable = false;
			middleCallibreButton.transform.FindChild("Text").gameObject.SetActive(false);
			middleCallibreButton.transform.FindChild("Locked").gameObject.SetActive(true);
		}

		if(PlayerPrefs.GetString("HugeGun") == "True")
		{
			hugeCallibreButton.interactable = true;
			hugeCallibreButton.transform.FindChild("Text").gameObject.SetActive(true);
			hugeCallibreButton.transform.FindChild("Locked").gameObject.SetActive(false);
		}
		else
		{
			hugeCallibreButton.interactable = false;
			hugeCallibreButton.transform.FindChild("Text").gameObject.SetActive(false);
			hugeCallibreButton.transform.FindChild("Locked").gameObject.SetActive(true);
		}



		//Upgrades
		airplane.smallGunLevelUpgrade = PlayerPrefs.GetInt("MinigunLevel");
		airplane.middleGunLevelUpgrade = PlayerPrefs.GetInt("MiddleGunLevel");
		airplane.hugeGunLevelUpgrade = PlayerPrefs.GetInt("HugeGunLevel");
	}

    public void HumanKill() //Calls when player kill human
    {
        humanKill++; 
        t_humanKill.text = humanKill.ToString() + "/" + maxHumanKill.ToString();

        if(humanKill > maxHumanKill)
		{
			Defeat();
		}
    }

	public void HumanSaved() //Calls when human enter in bunker
	{
		humanSaved ++;
		t_humanSaved.text = humanSaved.ToString();
	}

    public void ZombieKill(int reward)//Calls when player kill enemy
    {
        totalKill++;
        tempPlayerEarn += reward;

        t_totalKill.text = "Kills : " + totalKill.ToString();
        t_moneyEarned.text = "Coins : " + tempPlayerEarn.ToString();
    }

	public void Defeat()//Calls when player loose
	{
		if(!defeat)
		{
			i_deadScreen.gameObject.SetActive(true);
			i_deadScreen.transform.SendMessage("TweenPlay",false);

			defeat = true;
		}
	}

	void Update()//Calls every frame
	{
		if(defeat)
		{
			if(i_deadScreen.color.a == 1)
	     	{
				PlayerPrefs.SetInt("Money",tempMoney + tempPlayerEarn);

				Time.timeScale = 1;
				PlayerPrefs.SetString("LoadScene","Menu");
				Application.LoadLevelAsync("Loading");
		    }
		}

	}

    public void SelectCalibre(string value)//Calls when player click on caliber buttons
    {
        airplane.SetCaliber(value); //Set caliber for airplane

        if(value == "Small")
        {
            smallCallibreButton.transform.FindChild("Selected").gameObject.SetActive(true);
            middleCallibreButton.transform.FindChild("Selected").gameObject.SetActive(false);
            hugeCallibreButton.transform.FindChild("Selected").gameObject.SetActive(false);
        }

        if(value == "Middle")
        {
            smallCallibreButton.transform.FindChild("Selected").gameObject.SetActive(false);
            middleCallibreButton.transform.FindChild("Selected").gameObject.SetActive(true);
            hugeCallibreButton.transform.FindChild("Selected").gameObject.SetActive(false);

        }

        if(value == "Huge")
        {
            smallCallibreButton.transform.FindChild("Selected").gameObject.SetActive(false);
            middleCallibreButton.transform.FindChild("Selected").gameObject.SetActive(false);
            hugeCallibreButton.transform.FindChild("Selected").gameObject.SetActive(true);

        }

        
    }

    public void UICallback(string value) //UI Callbacks
	{
		if(value == "Pause")
		{
			Time.timeScale = 0;
			foreach(GameObject uiElement in inGameUIElemnts)
			{
				uiElement.SetActive(false);
			}
			menuPause.SetActive(true);

			Camera.main.SendMessage("Blur",true);
		}
		if(value == "Resume")
		{
			Time.timeScale = 1;
			foreach(GameObject uiElement in inGameUIElemnts)
			{
				uiElement.SetActive(true);
			}
			menuPause.SetActive(false);
			Camera.main.SendMessage("Blur",false);
		}
		if(value == "Restart")
		{
			PlayerPrefs.SetInt("Money",tempMoney + tempPlayerEarn);

			Time.timeScale = 1;
			PlayerPrefs.SetString("LoadScene",Application.loadedLevelName);
			Application.LoadLevelAsync("Loading");
		}
		if(value == "Quit")
		{
			PlayerPrefs.SetInt("Money",tempMoney + tempPlayerEarn);

			Time.timeScale = 1;
			PlayerPrefs.SetString("LoadScene","Menu");
			Application.LoadLevelAsync("Loading");
		}
	}

}
