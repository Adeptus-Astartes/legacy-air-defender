using UnityEngine;
using System.Collections;
//This script attached to hit effect, after spawn he set radius for SphereCollider. If enemy or human enter to this sphere then he get damage
public class HitDetector : MonoBehaviour
{
    int damage;

    void SetRadius(float radius)
    {
        this.GetComponent<SphereCollider>().radius = radius;
    }

    void SetDamage(int m_dam)
    {
        damage = m_dam;
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.collider.tag == "Humanoid")
        {
            other.SendMessage("Damage", damage);
        }

    }

}
