using UnityEngine;
using System.Collections;

public class Gun : MonoBehaviour 
{
    private bool shoot = false;

	public float attackRate = 0.3f;

	public Rigidbody projectile;
	public Transform shootPoint;

    [Header("Weapon Settings")]
    public int gunDamage = 10;
    public float gunDamRadius = 1;
    public float gunAccuracy;

    private float lastShot = -10.0f;
	
	float timer = 0;
    float shootTime = 0;


	void Shoot(bool value)
	{
        shoot = value;
    }

    void Update()
    {
        if(shoot)
        {
            if (Time.time > lastShot + attackRate + Random.Range(-0.3f, 0.3f))
            {
                GameObject m_proj = Instantiate(projectile.gameObject, shootPoint.position, shootPoint.rotation) as GameObject;
                m_proj.SendMessage("UpdateDamage", gunDamage);
                m_proj.SendMessage("UpdateRadius", gunDamRadius);
                m_proj.SendMessage("UpdateAccuracy", gunAccuracy);
                lastShot = Time.time;
            }
        }

    }

}
