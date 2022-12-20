using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Colission : MonoBehaviour
{

    public ParticleSystem Engine;

    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Asteroid")
        {
            Debug.Log("collided");
            Rigidbody rb;
            rb = GetComponent<Rigidbody>();
            rb.useGravity = true;
            Engine.Stop();
            Destroy(GetComponent<rocket_launch>());
        }
    }
}
