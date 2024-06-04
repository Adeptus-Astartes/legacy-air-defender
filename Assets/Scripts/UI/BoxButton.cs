using UnityEngine;
using System.Collections;
//This script send message when player click down on object with this script
public class BoxButton : MonoBehaviour 
{
	public GameObject target;
	public string funcName;
	public string value;

	void OnMouseDown()
	{
		target.SendMessage(funcName,value);
	}
}
