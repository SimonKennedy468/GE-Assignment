using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rocket_launch : MonoBehaviour
{

    public float launchDistance = 5;
    public int numWayPoints = 100;
    public int radius = 10;
    public float speed = 15;
    public int currentWaypoint = 0;
    
    List<GameObject> waypoints = new List<GameObject>();
    List<GameObject> flightWaypoints = new List<GameObject>();
    Vector3 launchEnd;
    public GameObject sky;
    public GameObject rotPoint;
    public GameObject rotPoint2;
    public Color blueNight;
    public Color purpleNight;
    public Color skyShift = Color.blue;

    public ParticleSystem Stars;
    public ParticleSystem Engine;


    public bool reachedLaunchEnd = false;
    public bool reachedSpinEnd = false;
    public bool avoiding = false;
    public bool gameOver = false;
    public bool createdSky = false;
    public bool launch = false;
    public bool skyDestroyed = false;
    public bool dodgeForward = false;
   
    public void startLaunch()
    {
        launch = true;
    }

    // Start is called before the first frame update
    IEnumerator flight()
    {
        
            if (Vector3.Distance(this.transform.position, flightWaypoints[currentWaypoint].transform.position) < 3)
            {
                currentWaypoint++;


                if (currentWaypoint > 1)
                {
                    currentWaypoint = 0;

                }
            }

            Vector3 directon = rotPoint.transform.position - transform.position;
            Quaternion toRotate = Quaternion.FromToRotation(Vector3.up, directon);
            transform.rotation = toRotate;

            transform.position = Vector3.MoveTowards(transform.position, flightWaypoints[currentWaypoint].transform.position, speed * Time.deltaTime);

        yield return null;
    }
    public IEnumerator skyChange()
    {
        if (Vector3.Distance(this.transform.position, flightWaypoints[currentWaypoint].transform.position) < 3)
        {
            currentWaypoint++;

            if (currentWaypoint == 1)
            {
                skyShift = blueNight;

            }

            if (currentWaypoint > 1)
            {
                currentWaypoint = 0;
                skyShift = purpleNight;

            }
        }
        Renderer rend = sky.GetComponent<Renderer>();
        rend.material.color = Color.Lerp(rend.material.color, skyShift, 0.01f);
        yield return null;
    }

    IEnumerator avoid()
    {
        Vector3 directon = rotPoint.transform.position - transform.position;
        Quaternion toRotate = Quaternion.FromToRotation(Vector3.up, directon);
        transform.rotation = toRotate;

        if(dodgeForward == true)
        {
            transform.position = Vector3.MoveTowards(transform.position, directon, speed * Time.deltaTime);
        }

        else if(dodgeForward == false)
        {
            directon = rotPoint2.transform.position - transform.position;
            transform.position = Vector3.MoveTowards(transform.position, directon, speed * Time.deltaTime);
        }

        directon = rotPoint.transform.position - transform.position;
        toRotate = Quaternion.FromToRotation(Vector3.up, directon);
        transform.rotation = toRotate;



        yield return null;
    }


    public void createSky()
    {
        float x;
        float y;
        float z;
        float angle;

        angle = 1 * Mathf.PI * 2;
        y = Mathf.Cos(angle) * radius;

        Vector3 rotPos = this.transform.position + new Vector3(0, y, 0);

        for (int i = 0; i < numWayPoints; i++)
        {
            angle = i * Mathf.PI * 2 / 2;
            x = Mathf.Sin(angle) * 10;
            y = Mathf.Cos(angle) * 10;

            GameObject go = new GameObject();
            Vector3 pos = rotPos + new Vector3(x, y, 0);
            go.transform.Translate(pos);

            flightWaypoints.Add(go);
        }


        Mesh m = new Mesh();
        sky = new GameObject();
        rotPoint = new GameObject();
        rotPoint2 = new GameObject();

        sky = GameObject.CreatePrimitive(PrimitiveType.Plane);
        x = (flightWaypoints[0].transform.position.x + flightWaypoints[1].transform.position.x) * 0.5f;
        y = (flightWaypoints[0].transform.position.y + flightWaypoints[1].transform.position.y) * 0.5f;
        z = (flightWaypoints[0].transform.position.z + flightWaypoints[1].transform.position.z) * 0.5f;
        sky.transform.position = new Vector3(x, y, z);
        sky.transform.Rotate(90, 0, 0);

        rotPoint.transform.position = sky.transform.position + new Vector3(50, 0, 0);
        rotPoint2.transform.position = sky.transform.position + new Vector3(-50, 0, 0);


        Stars.transform.position = rotPoint.transform.position - new Vector3(35, 0, -0.5f);
        Stars.transform.localScale = new Vector3(1, 1, 1);
        Stars.Play();


        sky.GetComponent<Renderer>().material.color = blueNight;


    }

    public IEnumerator growSky()
    {
        if (sky.transform.localScale.x < 3)
        {

            sky.transform.localScale += Vector3.one * Time.deltaTime * 3;
        }
        else
        {
            createdSky = true;
        }
        yield return null;
    }

    public void startDestroySky()
    {

        StartCoroutine(destroySky());
        StopCoroutine(skyChange());
        StopCoroutine(flight());
        gameOver = true;
        
    }
    public IEnumerator destroySky()
    {
        
        Stars.Pause();

        sky.transform.localScale -= Vector3.one * Time.deltaTime * 3;
        Stars.transform.localScale -= Vector3.one * Time.deltaTime * 3;

        if (sky.transform.localScale.x < 0.1f)
        {
            Destroy(sky);
            skyDestroyed = true;
        }
        yield return null;

    }

    public void restart()
    {
        
        if(gameOver == true)
        {
            reachedLaunchEnd = false;
            reachedSpinEnd = false;
            avoiding = false;
            gameOver = false;
            createdSky = false;
            launch = true;
            skyDestroyed = false;

            flightWaypoints.Clear();

            //Vector3 direction = new Vector3(0,100,0) - transform.position;
            //Quaternion toRotate = Quaternion.FromToRotation(Vector3.up, direction);
            //transform.rotation = toRotate;
        }
        
        
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

   

    // Update is called once per frame
    void Update()
    {
        if(launch == true)
        {
            if (reachedLaunchEnd == false)
            {
                this.transform.Translate(0, speed * Time.deltaTime, 0);
                Engine.Play();

                if (Vector3.Distance(this.transform.position, launchEnd) < 3)
                {
                    reachedLaunchEnd = true;
                    Debug.Log("Reached Launch End");
                }
            }
            else
            {
                if (reachedSpinEnd == false)
                {
                    if (currentWaypoint != numWayPoints)
                    {

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
                                transform.rotation = toRotate;


                            }
                        }
                    }
                }

                else if (reachedSpinEnd == true)
                {
                    if (gameOver == false)
                    {
                        if (flightWaypoints.Count == 0)
                        {
                            createSky();
                        }
                        if (createdSky == false)
                        {
                            StartCoroutine(growSky());
                        }
                        if (avoiding == false)
                        {
                            StartCoroutine(flight());
                        }

                        StartCoroutine(skyChange());
                    }
                    else
                    {
                        if(skyDestroyed == false)
                        {
                            StartCoroutine(destroySky());
                        }
                        
                    }


                }
            }
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Danger")
        {

            StopCoroutine(flight());
            StartCoroutine(avoid());
            avoiding = true;
            
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Danger")
        {

            StopCoroutine(flight());
            StartCoroutine(avoid());
            avoiding = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Danger")
        {
            if (dodgeForward == true)
            {
                dodgeForward = false;
            }
            else if(dodgeForward == false)
            {
                dodgeForward = true;
            }
            avoiding = false;
        }
    }
}
