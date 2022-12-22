using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Colission : MonoBehaviour
{

    public ParticleSystem Engine;
    public rocket_launch rl;
    public GameObject rocket;


    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Asteroid")
        {
            FindObjectOfType<audioManger>().play("crash");
            Debug.Log("collided");
            Rigidbody rb;
            rb = GetComponent<Rigidbody>();
            rb.isKinematic = false;
            rb.useGravity = true;
            Engine.Stop();
            rocket = GameObject.Find("Rocket");
            rl = (rocket_launch)rocket.GetComponent(typeof(rocket_launch));
            rl.startDestroySky();

            
        }
    }
    /*
    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Asteroid")
        {
            rocket = GameObject.Find("Rocket");
            rl = (rocket_launch)rocket.GetComponent(typeof(rocket_launch));
            rl.startDestroySky();

        }
    }
    */
}
