/*Class to manage the rocket launch, flight and avoiding behavoirs of the rocket
 * it also generates the Sky and stars, along with their shrinking and destruction
 */


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rocket : MonoBehaviour
{
    //Variables to be used to dictate distance, waypoints, etc
    public float launchDistance = 20;
    public int numWayPoints = 100;
    public int radius = 10;
    public float speed = 15;
    public int currentWaypoint = 0;

    //Variables to dictate ingame objects and assets
    List<GameObject> waypoints = new List<GameObject>();
    List<GameObject> flightWaypoints = new List<GameObject>();
    Vector3 launchEnd;
    public GameObject sky;
    public GameObject rotPoint;
    public Color blueNight;
    public Color purpleNight;
    public Color skyShift = Color.blue;
    public ParticleSystem Stars;
    public ParticleSystem Engine;

    //Booleans to dictate the running of certain co-routines
    public bool reachedLaunchEnd = false;
    public bool reachedSpinEnd = false;
    public bool avoiding = false;
    public bool gameOver = false;
    public bool createdSky = false;
    public bool launch = false;
    public bool skyDestroyed = false;
    public bool dodgeForward = false;


    //Function to start launch of the ship, only sets booleans to check later,
    //as relevant co-routines arent accessible from other scripts. Also plays 
    //the relevant sound effects
    public void startLaunch()
    {
        //make sure ship has already launched
        if (launch == false)
        {
            FindObjectOfType<audioManger>().play("launch");
            FindObjectOfType<audioManger>().play("flight");
        }
        launch = true;
    }

    //Co-routine that sets up the static "flight" of the ship once its stopped its flight.
    //creates a waypoint far on the X axis that it looks at to simulate the "waving" of the ship
    //(should be a sine wave but coudlnt get implemented. messy but it works!
    IEnumerator flight()
    {
        //check if ship has reached waypoint
        if (Vector3.Distance(this.transform.position, flightWaypoints[currentWaypoint].transform.position) < 2)
        {
            currentWaypoint++;
            //reset waypoints once all are reached 
            if (currentWaypoint > 1)
            {
                currentWaypoint = 0;

            }
        }


        //get vector of the distant point, and rotate the top of the ship towards it 
        Vector3 directon = rotPoint.transform.position - transform.position;
        Quaternion toRotate = Quaternion.FromToRotation(Vector3.up, directon);
        transform.rotation = toRotate;

        transform.position = Vector3.MoveTowards(transform.position, flightWaypoints[currentWaypoint].transform.position, speed * Time.deltaTime);

        yield return null;
    }

    //this changes the colour of the sky from blue to purple night materials
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

    //routine to make ship avoid danger if detected, moves forward towards the rotation point
    IEnumerator avoid()
    {
        Vector3 directon = rotPoint.transform.position - transform.position;
        Quaternion toRotate = Quaternion.FromToRotation(Vector3.up, directon);
        transform.rotation = toRotate;

        transform.position = Vector3.MoveTowards(transform.position, directon, speed * Time.deltaTime);

        yield return null;
    }


    //function to create the sky hologram background and display the moving star particles. 
    public void createSky()
    {
        float x;
        float y;
        float angle;

        //use sine and cosine to calculate 2 gameobjects above and below the ship to use as waypoints
        for (int i = 0; i < numWayPoints; i++)
        {
            angle = i * Mathf.PI * 2 / 2;
            x = Mathf.Sin(angle) * 10;
            y = Mathf.Cos(angle) * 10;

            GameObject go = new GameObject();
            Vector3 pos = this.transform.position + new Vector3(x, y, 0);
            go.transform.Translate(pos);

            flightWaypoints.Add(go);
        }

        //create sky plane 
        sky = new GameObject();
        rotPoint = new GameObject();

        sky = GameObject.CreatePrimitive(PrimitiveType.Plane);
        sky.transform.position = this.transform.position;
        sky.transform.Rotate(90, 0, 0);
        sky.tag = "Sky";

        //move the rotation point away from the plane
        rotPoint.transform.position = sky.transform.position + new Vector3(50, 0, 0);

        //set position and size 
        Stars.transform.position = rotPoint.transform.position - new Vector3(35, 0, -0.5f);
        Stars.transform.localScale = new Vector3(1, 1, 1);
        //start stars animation
        Stars.Play();


        sky.GetComponent<Renderer>().material.color = blueNight;


    }

    //co-routine to grow the sky's size 
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

    //function to stary the process of destroy sky and crash ship on colission
    public void startDestroySky()
    {

        StartCoroutine(destroySky());
        StopCoroutine(skyChange());
        StopCoroutine(flight());
        FindObjectOfType<audioManger>().pause("flight");

        gameOver = true;

    }

    //co-routine to stop the stars and shrink the sky on landing or crash
    public IEnumerator destroySky()
    {

        Stars.Pause();

        sky.transform.localScale -= Vector3.one * Time.deltaTime * 3;
        Stars.transform.localScale -= Vector3.one * Time.deltaTime * 3;
        sky.transform.position = this.transform.position;

        if (sky.transform.localScale.x < 0.1f)
        {
            Destroy(sky);
            skyDestroyed = true;
        }
        yield return null;

    }

    //relaunch after a crash or landing. Currently bugged if you launch again during the spin
    public void restart()
    {

        if (gameOver == true)
        {


            reachedLaunchEnd = false;
            reachedSpinEnd = false;
            avoiding = false;
            gameOver = false;
            createdSky = false;
            launch = false;
            skyDestroyed = false;
            dodgeForward = false;


            startLaunch();

            FindObjectOfType<audioManger>().play("launch");
            FindObjectOfType<audioManger>().play("flight");

            flightWaypoints.Clear();
        }


    }

    //Start method to get the waypoints for the spin
    private void Awake()
    {

        float x;
        float y;
        float angle;

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
        //check if ship has just launched and move straight up to the 1st waypoint
        if (launch == true)
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
                //check if spin animation is done, if its not move to the next waypoint of the spin and rotate it in the correct orientation
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
                    //check if it has crashed , and run necessary sky and flight methods 
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
                        if (skyDestroyed == false)
                        {
                            StartCoroutine(destroySky());
                        }

                    }


                }
            }
        }

    }

    //check if rocket will be hit by asteroid.
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Danger")
        {

            StopCoroutine(flight());
            StartCoroutine(avoid());
            avoiding = true;

        }
    }


    //continue to avoid if rocket is in danger of being hit 
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Danger")
        {

            StopCoroutine(flight());
            StartCoroutine(avoid());
            avoiding = true;
        }
    }

    //rocket no longer in danger of being hit 
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Danger")
        {
            if (dodgeForward == true)
            {
                dodgeForward = false;
            }
            else if (dodgeForward == false)
            {
                dodgeForward = true;
            }
            avoiding = false;
        }
    }
}
