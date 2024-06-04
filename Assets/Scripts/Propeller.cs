using UnityEngine;
using System.Collections;

//Rotate game object

public class Propeller : MonoBehaviour
{
    public float speed;

	void Update ()
    {
        transform.Rotate(Vector3.forward * speed * Time.deltaTime);
	}
}
