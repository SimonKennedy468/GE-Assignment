using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rocket_launch : MonoBehaviour
{

    public float launchDistance = 5;
    public int numWayPoints = 3;
    public int radius = 15;
    public float speed = 5;
    public float spinSpeed = 10;
    public int currentWaypoint = 0;
    public bool reachedLaunchEnd = false;
    public bool reachedSpinEnd = false;
    List<GameObject> waypoints = new List<GameObject>();
    List<GameObject> flightWaypoints = new List<GameObject>();
    Vector3 launchEnd;
    public Transform target;
    public GameObject sky;
    public int currSkyMat = 0;

    public Material blueNight;
    public Material purpleNight;
    public Material redNight;
    public Material greenNight;

    public float journeyTime = 25f;

    private float startTime;
    // Start is called before the first frame update
    void Start()
    {
        
    }


    /*
    public void OnDrawGizmos()
    {
        if(!Application.isPlaying)
        {
            float x = 0;
            float y = 0;
            float angle = 0;

            angle = 1 * Mathf.PI * 2;
            y = Mathf.Cos(angle) * radius;

            Vector3 launchEnd = this.transform.position + new Vector3(0, y + launchDistance, 0);
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
        }
    }
    */
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

            GameObject go = new GameObject();
            Vector3 pos = launchEnd + new Vector3(x, y, 0);
            go.transform.Translate(pos);
            waypoints.Add(go);
        }
    }

    void createFlightPoints()
    {
        float x;
        float y;
        float z;
        float angle;

        angle = 1 * Mathf.PI * 2;
        y = Mathf.Cos(angle) * radius;

        launchEnd = this.transform.position + new Vector3(0, y, 0);

        for (int i = 0; i < numWayPoints; i++)
        {
            angle = i * Mathf.PI * 2 / 2;
            x = Mathf.Sin(angle) * 7;
            y = Mathf.Cos(angle) * 7;

            GameObject go = new GameObject();
            Vector3 pos = launchEnd + new Vector3(x, y, 0);
            go.transform.Translate(pos);

            flightWaypoints.Add(go);
        }


        Mesh m = new Mesh();
        sky = new GameObject();
        sky = GameObject.CreatePrimitive(PrimitiveType.Plane);
        x = (flightWaypoints[0].transform.position.x + flightWaypoints[1].transform.position.x) * 0.5f;
        y = (flightWaypoints[0].transform.position.y + flightWaypoints[1].transform.position.y) * 0.5f;
        z = (flightWaypoints[0].transform.position.z + flightWaypoints[1].transform.position.z) * 0.5f;
        sky.transform.position = new Vector3(x, y, z);
        sky.transform.Rotate(90, 0, 0);
        sky.transform.localScale = new Vector3(3, 3, 3);
        sky.GetComponent<Renderer>().material = blueNight;

        //GameObject stars = new GameObject("stars");
        //stars.AddComponent(ParticleSystem);


        //stars.GetComponent(MeshFilter).mesh = m;
    }
    /*
    IEnumerator spin()
    {
        Quaternion next_wp = Quaternion.LookRotation(launchEnd - this.transform.position);
        this.transform.rotation = Quaternion.Slerp(this.transform.rotation, next_wp, speed * Time.deltaTime);
        this.transform.Translate(0, speed * Time.deltaTime, 0);
        yield return null;
    }
    */
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
                //startTime = Time.time;
            }
        }
        else
        {
            if (reachedSpinEnd == false)
            {
                if (currentWaypoint != numWayPoints)
                {
                    

                    Debug.Log("current waypoint ------------------------------>" + currentWaypoint);
                    Debug.Log(reachedSpinEnd);

                    transform.position = Vector3.MoveTowards(transform.position, waypoints[currentWaypoint].transform.position, speed * Time.deltaTime);


                    /*
                     * Previous attempts to get ship to rotate around in a spin, these do not work
                     */


                    //Vector3 slerp
                    /*
                    Vector3 startcen = waypoints[currentWaypoint] - launchEnd - new Vector3(0, 1, 0);
                    Vector3 endcen = waypoints[currentWaypoint + 1] - launchEnd - new Vector3(0, 1, 0);
                    float complete = (Time.time - startTime) / journeyTime;

                    transform.position = Vector3.Slerp(startcen, endcen, complete);
                    transform.position += launchEnd;
                    */

                    //fromToRotation

                    //Vector3 directon = waypoints[currentWaypoint].transform.position - transform.position;
                    //Quaternion toRotate = Quaternion.FromToRotation(transform.up, directon);
                    //Debug.Log("Current rotation ---------------------------------------------------------------->" + toRotate);
                    //transform.rotation = Quaternion.Lerp(transform.rotation, toRotate, spinSpeed * Time.deltaTime);
                    //transform.rotation = toRotate;
                    //this.transform.Translate(0, speed * Time.deltaTime, 0);

                    //default
                    /*
                    Quaternion next_wp = Quaternion.LookRotation(waypoints[currentWaypoint].transform.position - this.transform.position);
                    this.transform.rotation = Quaternion.Slerp(this.transform.rotation, next_wp, speed * Time.deltaTime);
                    this.transform.position = Vector3.MoveTowards(transform.position, waypoints[currentWaypoint].transform.position, speed * Time.deltaTime);
                    */
                    //co-routine
                    /*
                    StartCoroutine(spin());
                    reachedSpinEnd = true;
                    currentWaypoint = 0;
                    */
                    //rotate towards
                    /*
                    Vector3 targetDirection = waypoints[currentWaypoint] - this.transform.position;
                    Vector3 newDirection = Vector3.RotateTowards(this.transform.up, targetDirection, speed * Time.deltaTime, 0.0f);
                    Debug.DrawRay(transform.position, newDirection, Color.blue);

                    this.transform.rotation = Quaternion.LookRotation(newDirection);
                    this.transform.Translate(0, speed * Time.deltaTime, 0);
                    */

                    //lookat mod
                    //Transform target = waypoints[currentWaypoint].





                    if (Vector3.Distance(this.transform.position, waypoints[currentWaypoint].transform.position) < 3)
                    {
                        currentWaypoint++;

                        if (currentWaypoint == numWayPoints)
                        {
                            currentWaypoint = 0;
                            reachedSpinEnd = true;

                        }
                        else
                        {
                            Vector3 directon = waypoints[currentWaypoint].transform.position - transform.position;
                            Quaternion toRotate = Quaternion.FromToRotation(Vector3.up, directon);
                            Debug.Log("Current rotation ---------------------------------------------------------------->" + toRotate);
                            //transform.rotation = Quaternion.Slerp(transform.rotation, toRotate, spinSpeed * Time.deltaTime);
                            transform.rotation = toRotate;
                            

                        }


                    }

                    
                    
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
                    if (Vector3.Distance(this.transform.position, flightWaypoints[currentWaypoint].transform.position) < 3)
                    {
                        currentWaypoint++;
                        
                        if (currentWaypoint > 1)
                        {
                            currentWaypoint = 0;
                        }
                        
                        if(currSkyMat == 0)
                        {
                            sky.GetComponent<Renderer>().material = blueNight;
                        }
                        if (currSkyMat == 1)
                        {
                            sky.GetComponent<Renderer>().material = purpleNight;
                        }
                        if (currSkyMat == 2)
                        {
                            sky.GetComponent<Renderer>().material = redNight;
                        }
                        if (currSkyMat == 3)
                        {
                            sky.GetComponent<Renderer>().material = greenNight;
                        }
                        currSkyMat++;
                        if (currSkyMat > 3)
                        {
                            currSkyMat = 0;
                        }
                        

                        //Vector3 directon = flightWaypoints[currentWaypoint].transform.position - transform.position;
                        //Quaternion toRotate = Quaternion.FromToRotation(Vector3.up, directon);

                        //toRotate = Quaternion.Lerp(this.transform.rotation, toRotate, spinSpeed * Time.deltaTime);

                        //attempt to restrict rotation to z axis
                        //Quaternion toRotateEuler = Quaternion.Slerp(this.transform.rotation, toRotate, spinSpeed * Time.deltaTime);
                        //toRotateEuler = Quaternion.Euler(new Vector3(0f, 0f, toRotateEuler.eulerAngles.z));
                        //transform.rotation = toRotateEuler;

                        //transform.rotation = toRotate;



                    }


                    
                    transform.position = Vector3.MoveTowards(transform.position, flightWaypoints[currentWaypoint].transform.position, speed * Time.deltaTime);

                }
            }
        }
    }
}
