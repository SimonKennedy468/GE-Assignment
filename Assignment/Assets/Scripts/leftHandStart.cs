//Class to controll the left hand controllers buttons using the new input method 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;


public class leftHandStart : MonoBehaviour
{
    //get controller 
    private ActionBasedController controller;

    //get rocket, 
    public GameObject rocket;
    public rocket rocketScript;
    public bool landing;

    Vector3 landingLook = new Vector3(0, 100, 0);

    // Start is called before the first frame update
    void Start()
    {
        //get the controller and check for the grip and trigger inputs 
        controller = GetComponent<ActionBasedController>();
        controller.selectAction.action.performed += Action_performed;
        controller.activateAction.action.performed += Action_performed1;

    }

    //check if trigger is pressed and starts to land the rocket
    private void Action_performed1(InputAction.CallbackContext obj)
    {
        rocket = GameObject.Find("Rocket");
        rocketScript = (rocket)rocket.GetComponent(typeof(rocket));
        rocketScript.startDestroySky();
        landing = true;
        
    }


    // Update is called once per frame
    // check if ship is currently landing 
    void Update()
    {
        if (landing == true)
        {
            StartCoroutine(land());
        }
    }

    //check if user has used grip and lands rocket 
    private void Action_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        
        rocket = GameObject.Find("Rocket");
        rocketScript = (rocket)rocket.GetComponent(typeof(rocket));
        rocketScript.restart();
    }

    //co-routing to land ship
    public IEnumerator land()
    {
        //set rocket to point up, and set as kinematic to prevent ragdoll
        Vector3 direction = landingLook - rocket.transform.position;
        Quaternion toRotate = Quaternion.FromToRotation(Vector3.up, direction);
        rocket.transform.rotation = toRotate;
        Rigidbody rb;
        rb = rocket.GetComponent<Rigidbody>();
        rb.isKinematic = true;
        rocketScript.gameOver = true;

        //return rocket to original position
        rocket.transform.position = Vector3.MoveTowards(rocket.transform.position, new Vector3(0, 2, 0), 15 * Time.deltaTime);

        //end landing co-routine upon completion
        if(rocket.transform.position == new Vector3(0,2,0))
        {
            landing = false;
        }
        yield return null;
    }
}
