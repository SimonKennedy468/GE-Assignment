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
    List<GameObject> waypoints = new List<GameObject>();
    List<GameObject> flightWaypoints = new List<GameObject>();
    Vector3 launchEnd;
    public GameObject sky;
    public GameObject rotPoint;
    public Color blueNight;
    public Color purpleNight;
    public Color skyShift = Color.blue;

    public ParticleSystem Stars;


    // Start is called before the first frame update
    void Start()
    {
        
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

            GameObject go = new GameObject();
            Vector3 pos = launchEnd + new Vector3(x, y, 0);
            go.transform.Translate(pos);
            waypoints.Add(go);
        }
    }

    void createSky()
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
            x = Mathf.Sin(angle) * 10;
            y = Mathf.Cos(angle) * 10;

            GameObject go = new GameObject();
            Vector3 pos = launchEnd + new Vector3(x, y, 0);
            go.transform.Translate(pos);

            flightWaypoints.Add(go);
        }


        Mesh m = new Mesh();
        sky = new GameObject();
        rotPoint = new GameObject();

        sky = GameObject.CreatePrimitive(PrimitiveType.Plane);
        x = (flightWaypoints[0].transform.position.x + flightWaypoints[1].transform.position.x) * 0.5f;
        y = (flightWaypoints[0].transform.position.y + flightWaypoints[1].transform.position.y) * 0.5f;
        z = (flightWaypoints[0].transform.position.z + flightWaypoints[1].transform.position.z) * 0.5f;
        sky.transform.position = new Vector3(x, y, z);
        sky.transform.Rotate(90, 0, 0);
        sky.transform.localScale = new Vector3(3, 3, 3);

        rotPoint.transform.position = sky.transform.position + new Vector3(50, 0, 0);


        Stars.transform.position = rotPoint.transform.position - new Vector3(35, 0, -0.5f);
        Stars.Play();


        sky.GetComponent<Renderer>().material.color = blueNight;

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
                            transform.rotation = toRotate;
                            

                        }
                    }
                }
            }

            else if (reachedSpinEnd == true)
            {
                if(flightWaypoints.Count == 0)
                {
                    createSky();
                }
                else
                {

                    Renderer rend = sky.GetComponent<Renderer>();
                    


                    if (Vector3.Distance(this.transform.position, flightWaypoints[currentWaypoint].transform.position) < 3)
                    {
                        currentWaypoint++;

                        Debug.Log("Current mat color" + rend.material.color);
                        Debug.Log("target Color" + blueNight);

                        if(currentWaypoint == 1)
                        {
                            skyShift = blueNight;
                            
                        }
                        
                        if (currentWaypoint > 1)
                        {
                            currentWaypoint = 0;
                            skyShift = purpleNight;
 
                        }
                    }

                    Vector3 directon = rotPoint.transform.position - transform.position;
                    Quaternion toRotate = Quaternion.FromToRotation(Vector3.up, directon);
                    Debug.Log("Current rotation ---------------------------------------------------------------->" + toRotate);
                    transform.rotation = toRotate;

                    transform.position = Vector3.MoveTowards(transform.position, flightWaypoints[currentWaypoint].transform.position, speed * Time.deltaTime);

                    rend.material.color = Color.Lerp(rend.material.color, skyShift, 0.01f);

                }
            }
        }
    }
}
