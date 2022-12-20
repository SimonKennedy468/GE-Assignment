using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.InputSystem;

public class NewBehaviourScript : MonoBehaviour
{
    List<GameObject> objects = new List<GameObject>();

    // Update is called once per frame
    void Update()
    {
        if(XR)
        //if (OVRInput.Get(OVR.Rawbutton.LHandTrigger)) ;
    }
    private void OnTriggerEnter(Collider other)
    {
        objects.Add(other.gameObject);
    }

    private void OnTriggerExit(Collider other)
    {
        objects.Remove(other.gameObject);
    }
}
