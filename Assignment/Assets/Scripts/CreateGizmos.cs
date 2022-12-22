using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateGizmos : MonoBehaviour
{

    public float launchDistance = 5;
    public int numWayPoints = 3;
    public int radius = 15;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnDrawGizmos()
    {
        //if (!Application.isPlaying)
        //{
            float x = 0;
            float y = 0;
            float angle = 0;

            angle = 1 * Mathf.PI * 2;
            y = Mathf.Cos(angle) * radius;

            Vector3 launchEnd = this.transform.position + new Vector3(0, y + launchDistance, 0);
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(launchEnd, 1);

            for (int i = 0; i < numWayPoints; i++)
            {
                angle = i * Mathf.PI * 2 / numWayPoints;
                x = Mathf.Sin(angle) * radius;
                y = Mathf.Cos(angle) * radius;

                Vector3 pos = launchEnd + new Vector3(x, y, 0);

                Gizmos.color = Color.green;
                Gizmos.DrawWireSphere(pos, 1);
            }
        //}
    }
}
