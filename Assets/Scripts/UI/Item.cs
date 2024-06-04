//Its item
//He can be upgradable
//He have can have unlimited upgrades

using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

[System.Serializable]
public class ItemUpgrade
{
	public int upgradeLevel;
	public string description;//Upgrade description
	public int cost;
}

public class Item : MonoBehaviour 
{
	[HideInInspector]
	private MainMenu m_mainMenu;//Auto fill

	public string itemName;
	public bool purchased = false;
	public int purchaseCost;

	public List<ItemUpgrade> upgrades;

	public int nextUprgade = 0;

	[Header("UISet-Up")]

	public Text buyButtonText;//Need for change text from buy to upgrade

	public Text t_description;
	public Text t_cost;
	public GameObject g_price;

	public Slider s_upgradeLevel;

	void Awake () 
	{
		m_mainMenu = (MainMenu)GameObject.FindObjectOfType(typeof (MainMenu));//Find main menu

		LoadData();
	}

	private void LoadData()
	{
		print(PlayerPrefs.GetString(itemName));
		purchased = (PlayerPrefs.GetString(itemName) == "True");
		nextUprgade = PlayerPrefs.GetInt(itemName + "Level");


		ReloadUI();
	}

	private void ReloadUI()
	{
		m_mainMenu.t_money.text = PlayerPrefs.GetInt("Money").ToString(); 

		if(!purchased)
		{
			buyButtonText.text = "BUY";
			t_cost.text = purchaseCost.ToString();
			s_upgradeLevel.gameObject.SetActive(false);
		}
		else
		{
			s_upgradeLevel.gameObject.SetActive(true);

			if(nextUprgade < upgrades.Count)
			{
				buyButtonText.transform.parent.gameObject.SetActive(true);
				g_price.SetActive(true);

				t_cost.text = upgrades[nextUprgade].cost.ToString();
				buyButtonText.text = "UPGRADE";
				t_description.text = upgrades[nextUprgade].description;

				s_upgradeLevel.maxValue = upgrades.Last().upgradeLevel;
				if(nextUprgade - 1 >= 0)
				{
			    	s_upgradeLevel.value = upgrades[nextUprgade - 1].upgradeLevel;
				}
				else
				{
					s_upgradeLevel.value = 0;
				}

			}
			else
			{
				t_description.text = "All Improvements Completed";
				buyButtonText.transform.parent.gameObject.SetActive(false);
				g_price.SetActive(false);

				s_upgradeLevel.maxValue = 1;
				s_upgradeLevel.value = 1;
			}

		}




	}

	public void SubmitRequest()//Calls when player click on buy/upgrade button
	{
		int _money = PlayerPrefs.GetInt("Money");

		if(!purchased)
		{
		    if((_money - purchaseCost) >= 0)
			{
				m_mainMenu.GetRequest(this,true);
			}
			else
			{
				m_mainMenu.GetRequest(this,false);
			}
		}
		else
		{
			if((_money - upgrades[nextUprgade].cost) >= 0)
			{
				m_mainMenu.GetRequest(this,true);
			}
			else
			{
				m_mainMenu.GetRequest(this,false);
			}
		}

	}

	public void PositiveResponse() // If player confirm 
	{
		if(!purchased)
		{
			PlayerPrefs.SetString(itemName,"True");

			int _money = PlayerPrefs.GetInt("Money");
			_money -= purchaseCost;
			PlayerPrefs.SetInt("Money",_money);
		}
		else
		{
			PlayerPrefs.SetInt(itemName + "Level",nextUprgade + 1);
			
			int _money = PlayerPrefs.GetInt("Money");
			_money -= upgrades[nextUprgade].cost;
			PlayerPrefs.SetInt("Money",_money);
		}

		m_mainMenu.SendMessage("ReloadUI");


	}

}
