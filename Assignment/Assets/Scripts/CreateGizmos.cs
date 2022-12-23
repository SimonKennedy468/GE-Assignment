/*class to create gizmos and view in scene view. 
 *kept in seperate class as gizmos will follow ship otherwise
 *also need to reset variables manually seperate from rocket script
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateGizmos : MonoBehaviour
{
    //store vars for waypoints and distance 
    public float launchDistance = 5;
    public int numWayPoints = 3;
    public int radius = 15;

    public void OnDrawGizmos()
    {
            float x;
            float y;
            float angle;

            angle = 1 * Mathf.PI * 2;
            y = Mathf.Cos(angle) * radius;

            Vector3 launchEnd = this.transform.position + new Vector3(0, y + launchDistance, 0);
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(launchEnd, 1);

            //draw gimos in circle around the "launch distance" point  
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
