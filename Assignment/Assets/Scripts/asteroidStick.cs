using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class asteroidStick : MonoBehaviour
{
    public GameObject parent;

    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Sky")
        {
            Rigidbody rb = GetComponent<Rigidbody>();
            Rigidbody rbParent = parent.GetComponent<Rigidbody>();

            rb.constraints = RigidbodyConstraints.FreezePosition;
            rbParent.constraints = RigidbodyConstraints.FreezePosition;
            Debug.Log("Hit sky");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Sky")
        {
            Rigidbody rb = GetComponent<Rigidbody>();
            Rigidbody rbParent = parent.GetComponent<Rigidbody>();

            rb.constraints = RigidbodyConstraints.None;
            rbParent.constraints = RigidbodyConstraints.None;
            Debug.Log("Hit sky");
        }
    }
}
