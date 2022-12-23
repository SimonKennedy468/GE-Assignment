//Class to detect if asteroid actually collides with ship
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Colission : MonoBehaviour
{

    //get particle system fro engine, the rocket script and the rocket object
    public ParticleSystem Engine;
    public rocket rocketScript;
    public GameObject rocket;


    // check if asteroid collided
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Asteroid")
        {
            FindObjectOfType<audioManger>().play("crash");
            Debug.Log("collided");
            Rigidbody rb;
            rb = GetComponent<Rigidbody>();
            //make rocket ragdoll and fall
            rb.isKinematic = false;
            rb.useGravity = true;
            Engine.Stop();

            //stop rocket co-routines
            rocket = GameObject.Find("Rocket");
            rocketScript = (rocket)rocket.GetComponent(typeof(rocket));
            rocketScript.startDestroySky();

            
        }
    }
}
