using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class rightHandReturn : MonoBehaviour
{
    private ActionBasedController controller;
    public GameObject asteroid1;
    public GameObject asteroid2;
    public GameObject asteroid3;
    public GameObject asteroid4;
    public GameObject asteroid5;
    public GameObject asteroid6;

    public GameObject socket1;
    public GameObject socket2;
    public GameObject socket3;
    public GameObject socket4;
    public GameObject socket5;
    public GameObject socket6;

    public GameObject rocket;
    public rocket_launch rl;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<ActionBasedController>();
        controller.selectAction.action.performed += Action_performed;
        controller.activateAction.action.performed += Action_performed1;

    }

    private void Action_performed1(InputAction.CallbackContext obj)
    {
        asteroid1.transform.position = socket1.transform.position;
        asteroid2.transform.position = socket2.transform.position;
        asteroid3.transform.position = socket3.transform.position;
        asteroid4.transform.position = socket4.transform.position;
        asteroid5.transform.position = socket5.transform.position;
        asteroid6.transform.position = socket6.transform.position;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void Action_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {

        rocket = GameObject.Find("Rocket");
        rl = (rocket_launch)rocket.GetComponent(typeof(rocket_launch));
        rl.startLaunch();
        rl.restart();
    }
}
