using UnityEngine;
using System.Collections;
using System.Collections.Generic;
//Basic AI scripts
public class Humanoid : MonoBehaviour
{
    public WaypointPath myPath; //Path
    [Header("Settings")]
    public bool itsZombie = true;
	//Settings
    public int hp;
    public float speed;
    public float rotationSpeed;

    public int reward;

    [Header("Behavior")]//Behavior settings

    public bool moveToTarget = true;
    public bool moveToRandomTarget = false;
   

    public float changeBehaviorRate = 3;
    public float randomValueForRandomTarget = 2;

    public float timeWhenMoveToTarget = 5;
    private float p_timeWhenMoveToTarget = 0;

    public float timeWhenMoveToRandomTarget = 2;
    private float p_timeWhenMoveToRandomTarget = 0;

    private float p_diffTime = 0;

    private Quaternion randomDir;

    private int currentWayPoint;
    private List<Vector3> myWaypoints;

    private GameManager m_manager;
 


    void Start()//Checkings 
    {
        if (myPath != null)
            myWaypoints = myPath.waypoints;
        if (m_manager == null)
            m_manager = (GameManager)GameObject.FindObjectOfType(typeof(GameManager));
    }

    void Update()//Movement 
    {
        Debug.DrawLine(transform.position, myWaypoints[currentWayPoint]);
        if (Vector3.Distance(transform.position, myWaypoints[currentWayPoint]) < 0.3f)
        {
            if (currentWayPoint + 1 < myWaypoints.Count)
            {
                currentWayPoint++;
            }
            else
            { 
				if(!itsZombie)
				{
					m_manager.HumanSaved();
					Delete();
				}
				else
				{
					m_manager.Defeat();
					Delete();
				}
                
            }
            
        }

        GodsRandom();
        Move();
        Rotation();
    }

    void GodsRandom()
    {
        if(moveToTarget && !moveToRandomTarget)
        {
            p_timeWhenMoveToTarget += Time.deltaTime;
            if (p_timeWhenMoveToTarget > timeWhenMoveToTarget)
            {
                moveToTarget = false;
                moveToRandomTarget = true;

                randomDir = Quaternion.Euler(0, Random.Range(0, 360), 0);
                p_timeWhenMoveToTarget = 0;

            }
        }

        if(moveToRandomTarget && !moveToTarget)
        {
            p_timeWhenMoveToRandomTarget += Time.deltaTime;
            if (p_timeWhenMoveToRandomTarget > timeWhenMoveToRandomTarget)
            {
                moveToTarget = true;
                moveToRandomTarget = false;

                p_timeWhenMoveToRandomTarget = 0;

            }

        }

    }

    void Move()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    void Rotation()
    {
        Quaternion rotation = new Quaternion();

        if (moveToTarget)
        {
            rotation = Quaternion.LookRotation(myWaypoints[currentWayPoint] - transform.position);
        }

        if (moveToRandomTarget)
        {
            rotation = randomDir;
        }
 
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * rotationSpeed);
    }

    void Damage(int dam)
    {
        hp -= dam;
        if(hp<0)
        {
            if (itsZombie)
                m_manager.ZombieKill(reward);
            if (!itsZombie)
                m_manager.HumanKill();
            Delete();
        }
    }

    void Delete()
    {
        Destroy(gameObject);
    }

}
