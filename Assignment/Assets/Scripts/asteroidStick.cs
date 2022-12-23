//Class to make the asteroids stick to the sky plane once they collide

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class asteroidStick : MonoBehaviour
{
    //get the outer collider of the asteroid that the rocket
    //uses to detect danger
    public GameObject parent;
    

    //check if it has hit the sky plane, then use its rigidbody
    //to freeze its position
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Sky")
        {
            Rigidbody rb = GetComponent<Rigidbody>();
            Rigidbody rbParent = parent.GetComponent<Rigidbody>();

            rb.constraints = RigidbodyConstraints.FreezePosition;
            rbParent.constraints = RigidbodyConstraints.FreezePosition;
        }
    }

    //when the sky is shrunk and is no longer colliding with the sky, release the freeze constraint
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Sky")
        {
            Rigidbody rb = GetComponent<Rigidbody>();
            Rigidbody rbParent = parent.GetComponent<Rigidbody>();

            rb.constraints = RigidbodyConstraints.None;
            rbParent.constraints = RigidbodyConstraints.None;
        }
    }
}
