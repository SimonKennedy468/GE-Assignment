using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class returnAsteroids : MonoBehaviour
{

    public GameObject asteroid1;
    public GameObject asteroid2;
    public GameObject asteroid3;
    // Start is called before the first frame update
    public InputDevice rightController;
    
    
    

    void Start()
    {
        initController();
    }



    private void initController()
    {
        List<InputDevice> foundControllers = new List<InputDevice>();
        InputDevices.GetDevicesWithCharacteristics(InputDeviceCharacteristics.Controller | InputDeviceCharacteristics.Right, foundControllers);
        if (foundControllers.Count > 0)
        {
            rightController = foundControllers[0];
        }
    }

    // Update is called once per frame
    private void Update()
    {
        buttonPress();
    }
    private void buttonPress()
    {
        var inputFeatures = new List<UnityEngine.XR.InputFeatureUsage>();
        if (rightController.TryGetFeatureUsages(inputFeatures))
        {
            foreach (var feature in inputFeatures)
            {
                if (feature.type == typeof(bool))
                {
                    bool featureValue;
                    if (rightController.TryGetFeatureValue(feature.As<bool>(), out featureValue))
                    {
                        Debug.Log(string.Format("Bool feature {0}'s value is {1}", feature.name, featureValue.ToString()));
                    }
                }
            }
        }
    }
}
