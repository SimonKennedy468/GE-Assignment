//Class to control right hand controller and its inputs 
//refrenced from https://www.youtube.com/watch?v=u6Rlr2021vw
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class rightHandReturn : MonoBehaviour
{

    private ActionBasedController controller;

    //Store asteroid colliders 
    public GameObject asteroid1;
    public GameObject asteroid2;
    public GameObject asteroid3;
    public GameObject asteroid4;
    public GameObject asteroid5;
    public GameObject asteroid6;

    //store sockets holding the asteroids
    public GameObject socket1;
    public GameObject socket2;
    public GameObject socket3;
    public GameObject socket4;
    public GameObject socket5;
    public GameObject socket6;

    public GameObject rocket;
    public rocket rl;

    // Start is called before the first frame update
    void Start()
    {
        //get the controller and check for the grip and trigger inputs
        controller = GetComponent<ActionBasedController>();
        controller.selectAction.action.performed += Action_performed;
        controller.activateAction.action.performed += Action_performed1;

    }
    //check for trigger input and retur asteroids to the holder sockets 
    private void Action_performed1(InputAction.CallbackContext obj)
    {
        asteroid1.transform.position = socket1.transform.position;
        asteroid2.transform.position = socket2.transform.position;
        asteroid3.transform.position = socket3.transform.position;
        asteroid4.transform.position = socket4.transform.position;
        asteroid5.transform.position = socket5.transform.position;
        asteroid6.transform.position = socket6.transform.position;
    }

    //check for grip input and start/restart the launch
    private void Action_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {

        rocket = GameObject.Find("Rocket");
        rl = (rocket)rocket.GetComponent(typeof(rocket));
        rl.restart();
    }
}
