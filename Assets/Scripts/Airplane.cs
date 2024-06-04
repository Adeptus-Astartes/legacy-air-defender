using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class WeaponAbillity //Weapon Characteristics
{
	public int damage;
	public float damageRadius;
	public float shotAccuracy;
	public int camZoom;
}

public class Airplane : MonoBehaviour 
{
    public GunCaliber currentCaliber; //Selected gun

	public WaypointPath myPath; //Path
	public float speed;
	public float rotationSpeed;

	private int currentWayPoint; //current Waypoint
	private List<Vector3> myWaypoints;
	//Gun links
    public Gun smallGun; 
    public Gun middleGun;
    public Gun hugeGun;

	//Current Upgrades
	[Header("Upgrades")]
	public int smallGunLevelUpgrade = 1;
	public int middleGunLevelUpgrade = 1;
	public int hugeGunLevelUpgrade = 1;

    [Header("Weapons Settings")]
	//Abilities List for guns
	public List<WeaponAbillity> smallGunAbillities;
	public List<WeaponAbillity> middleGunAbillities;
	public List<WeaponAbillity> hugeGunAbillities;

    public Camera cam; //Main Camera

    void Start()
	{
		if(myPath!=null)
			myWaypoints = myPath.waypoints;

        SetSetting();
	}

    void SetSetting()//Apply characteristics
    {
		WeaponAbillity smallGunAbi = smallGunAbillities[smallGunLevelUpgrade];
		smallGun.gunDamage = smallGunAbi.damage;
		smallGun.gunDamRadius = smallGunAbi.damageRadius;
		smallGun.gunAccuracy = smallGunAbi.shotAccuracy;

		WeaponAbillity middleGunAbi = middleGunAbillities[middleGunLevelUpgrade];
        middleGun.gunDamage = middleGunAbi.damage;
        middleGun.gunDamRadius = middleGunAbi.damageRadius;
        middleGun.gunAccuracy = middleGunAbi.shotAccuracy;

		WeaponAbillity hugeGunAbi = hugeGunAbillities[hugeGunLevelUpgrade];
        hugeGun.gunDamage = hugeGunAbi.damage;
        hugeGun.gunDamRadius = hugeGunAbi.damageRadius;
        hugeGun.gunAccuracy = hugeGunAbi.shotAccuracy;
    }

    public void SetCaliber(string value) //Calls from GameManager.cs
    {
        if (value == "Small")
        {
            currentCaliber = GunCaliber.Small;
            cam.fieldOfView = smallGunAbillities[smallGunLevelUpgrade].camZoom;
        }
          
        if (value == "Middle")
        {
            currentCaliber = GunCaliber.Middle;
			cam.fieldOfView = middleGunAbillities[middleGunLevelUpgrade].camZoom;
        }
           
        if (value == "Huge")
        {
            currentCaliber = GunCaliber.Huge;
			cam.fieldOfView = middleGunAbillities[middleGunLevelUpgrade].camZoom;
        }      
    }

	void Update()//Movement
	{
		Debug.DrawLine(transform.position,myWaypoints[currentWayPoint]);
		if(Vector3.Distance(transform.position,myWaypoints[currentWayPoint])< 5f)
		{
			if(currentWayPoint + 1 < myWaypoints.Count)
		    	currentWayPoint ++;
			else
				currentWayPoint = 0;
		}


		Move();
		Rotation();
		GunLookAt();
	}

	void Move()
	{
		transform.Translate(Vector3.forward * speed * Time.deltaTime);
	}

	void Rotation()
	{
		Quaternion rotation = Quaternion.LookRotation(myWaypoints[currentWayPoint] - transform.position);
		transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * rotationSpeed);
	}

	void GunLookAt()
	{
		RaycastHit hit;
		Ray ray = new Ray();
		ray.origin = Camera.main.transform.position;
		ray.direction = Camera.main.transform.forward;

		if (Physics.Raycast(ray, out hit))
		{
			smallGun.transform.LookAt(hit.point);
			middleGun.transform.LookAt(hit.point);
			hugeGun.transform.LookAt(hit.point);
			
		}
	}

    public void Shoot(bool value) //Calls when player press "Fire" button
    {
        if (currentCaliber == GunCaliber.Small)
        {
            smallGun.SendMessage("Shoot", value);

        }
         
        if (currentCaliber == GunCaliber.Middle)
        {
            middleGun.SendMessage("Shoot", value);

        }
           
        if (currentCaliber == GunCaliber.Huge)
        {
            hugeGun.SendMessage("Shoot", value);

        }
            
    }
}
