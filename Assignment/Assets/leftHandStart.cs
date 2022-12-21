using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class leftHandStart : MonoBehaviour
{
    private ActionBasedController controller;

    public GameObject rocket;
    public rocket_launch rl;
    public bool landing;

    Vector3 landingLook = new Vector3(0, 100, 0);

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<ActionBasedController>();
        controller.selectAction.action.performed += Action_performed;
        controller.activateAction.action.performed += Action_performed1;

    }

    private void Action_performed1(InputAction.CallbackContext obj)
    {
        rocket = GameObject.Find("Rocket");
        rl = (rocket_launch)rocket.GetComponent(typeof(rocket_launch));
        rl.startDestroySky();
        landing = true;
        
    }

    // Update is called once per frame
    // Update is called once per frame
    void Update()
    {
        if (landing == true)
        {
            StartCoroutine(land());
        }
    }

    private void Action_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        
        rocket = GameObject.Find("Rocket");
        rl = (rocket_launch)rocket.GetComponent(typeof(rocket_launch));
        rl.startLaunch();
        rl.restart();
    }

    public IEnumerator land()
    {
        Vector3 direction = landingLook - rocket.transform.position;
        Quaternion toRotate = Quaternion.FromToRotation(Vector3.up, direction);
        rocket.transform.rotation = toRotate;
        Rigidbody rb;
        rb = rocket.GetComponent<Rigidbody>();
        rb.isKinematic = true;
        rl.gameOver = true;

        rocket.transform.position = Vector3.MoveTowards(rocket.transform.position, new Vector3(0, 2, 0), 15 * Time.deltaTime);

        if(rocket.transform.position == new Vector3(0,2,0))
        {
            landing = false;
        }
        yield return null;
    }
}
