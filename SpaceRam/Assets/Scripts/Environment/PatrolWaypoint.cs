using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolWaypoint : MonoBehaviour
{
    //This class may be used later for customizing behavior 
    //between points (such as changing an object to approach mode)
    float maximumApproachTime = -1; //Used to determine how long before an object gives up on approaching this waypoint


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, 0.25f);
    }
}
