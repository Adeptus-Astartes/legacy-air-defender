using UnityEngine;
using System.Collections;

//Bullet

public class Projectile : MonoBehaviour
{
    public float speed;
    public int damage;
    public float radius;
    public GameObject deadEffect;

	//Calls immediatly after spawn
    void UpdateDamage(int value)
    {
        damage = value;
    }

    void UpdateRadius(float value)
    {
        radius = value;
    }

    void UpdateAccuracy(float value)
    {
        transform.eulerAngles += new Vector3(Random.Range(-value, value), Random.Range(-value, value), 0);

    }

	//Moving bullet
    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }
	//Collision Detect
    void OnCollisionEnter(Collision other)
    {
        if(other.collider.tag != "StaticStuff")
        {
            print("Hit");

        }
        if (other.collider.tag == "StaticStuff")
        {
            GameObject r_dead = Instantiate(deadEffect, other.contacts[0].point, Quaternion.Euler(0, 0, 0)) as GameObject;
            r_dead.SendMessage("SetDamage", damage);
            r_dead.SendMessage("SetRadius", radius);
            Destroy(gameObject);
        } 
    }


}
