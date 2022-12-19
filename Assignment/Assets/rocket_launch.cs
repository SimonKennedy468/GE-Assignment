using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rocket_launch : MonoBehaviour
{

    public float launchDistance = 5;
    public int numWayPoints = 3;
    public int radius = 15;
    public float speed = 5;
    public int currentWaypoint = 0;
    public bool reachedLaunchEnd = false;
    public bool reachedSpinEnd = false;
    List<Vector3> waypoints = new List<Vector3>();
    List<Vector3> flightWaypoints = new List<Vector3>();
    Vector3 launchEnd;
    public Transform target;


    // Start is called before the first frame update
    void Start()
    {
        
    }



    public void OnDrawGizmos()
    {
        //if(!Application.isPlaying)
        //{
            float x = 0;
            float y = 0;
            float angle = 0;

            angle = 1 * Mathf.PI * 2;
            y = Mathf.Cos(angle) * radius;

            Vector3 launchEnd = transform.position + new Vector3(0, y + launchDistance, 0);
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(launchEnd, 1);

            for (int i = 0; i < numWayPoints; i++)
            {
                angle = i * Mathf.PI * 2 / numWayPoints;
                x = Mathf.Sin(angle) * radius;
                y = Mathf.Cos(angle) * radius;

                Vector3 pos = launchEnd + new Vector3(x, y, 0);

                Gizmos.color = Color.green;
                Gizmos.DrawWireSphere(pos, 1);
            }
        //}
    }

    private void Awake()
    {

        float x;
        float y;
        float angle ;

        angle = 1 * Mathf.PI * 2;
        y = Mathf.Cos(angle) * radius;

        launchEnd = transform.position + new Vector3(0, y + launchDistance, 0);

        for (int i = 0; i < numWayPoints; i++)
        {
            angle = i * Mathf.PI * 2 / numWayPoints;
            x = Mathf.Sin(angle) * radius;
            y = Mathf.Cos(angle) * radius;

            Vector3 pos = launchEnd + new Vector3(x, y, 0);
            waypoints.Add(pos);
        }
    }

    void createFlightPoints()
    {
        float x;
        float y;
        float angle;

        angle = 1 * Mathf.PI * 2;
        y = Mathf.Cos(angle) * radius;

        launchEnd = this.transform.position + new Vector3(0, y, 0);

        for (int i = 0; i < numWayPoints; i++)
        {
            angle = i * Mathf.PI * 2 / 2;
            x = Mathf.Sin(angle) * 5;
            y = Mathf.Cos(angle) * 5;

            Vector3 pos = launchEnd + new Vector3(x, y, 0);
            flightWaypoints.Add(pos);
        }
    }

    // Update is called once per frame
    void Update()
    {
    
        if(reachedLaunchEnd == false)
        {
            this.transform.Translate(0, speed * Time.deltaTime, 0);
            Debug.Log(Vector3.Distance(this.transform.position, launchEnd));

            if(Vector3.Distance(this.transform.position, launchEnd) < 3)
            {
                reachedLaunchEnd = true;
            }
        }
        else
        {
            if (reachedSpinEnd == false)
            {
                if (currentWaypoint < numWayPoints)
                {
                    if (Vector3.Distance(this.transform.position, waypoints[currentWaypoint]) < 3)
                    {
                        currentWaypoint++;
                        if(currentWaypoint == numWayPoints - 1)
                        {
                            reachedSpinEnd = true;
                            currentWaypoint = 0;
                        }
                    }

                    Debug.Log("current waypoint ------------------------------>" + currentWaypoint);
                    Debug.Log(reachedSpinEnd);


                    Quaternion next_wp = Quaternion.LookRotation(waypoints[currentWaypoint] - this.transform.position);
                    Quaternion rotation_fix = Quaternion.Euler(0, 90, 0);
                    this.transform.rotation = Quaternion.Slerp(this.transform.rotation, next_wp, speed * Time.deltaTime);
                    this.transform.Translate(0, 0, speed * Time.deltaTime);

                    

                    /*
                    Vector3 targetDirection = waypoints[currentWaypoint] - this.transform.position;
                    Vector3 newDirection = Vector3.RotateTowards(this.transform.up, targetDirection, speed * Time.deltaTime, 0.0f);
                    Debug.DrawRay(transform.position, newDirection, Color.blue);

                    this.transform.rotation = Quaternion.LookRotation(newDirection);
                    this.transform.Translate(0, speed * Time.deltaTime, 0);
                    */
                }
            }

            else if (reachedSpinEnd == true)
            {
                if(flightWaypoints.Count == 0)
                {
                    createFlightPoints();
                }
                else
                {
                    if (Vector3.Distance(this.transform.position, flightWaypoints[currentWaypoint]) < 3)
                    {
                        currentWaypoint++;
                        if (currentWaypoint > 1)
                        {
                            currentWaypoint = 0;
                        }
                    }
                    

                    Quaternion next_wp = Quaternion.LookRotation(flightWaypoints[currentWaypoint] - this.transform.position);
                    this.transform.rotation = Quaternion.Lerp(this.transform.rotation, next_wp, speed * Time.deltaTime);
                    this.transform.Translate(0, 0, speed * Time.deltaTime);
                }
            }
        }
    }
}
