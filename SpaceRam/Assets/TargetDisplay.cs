using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetDisplay : MonoBehaviour
{
    float r = .25f;

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, r);
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, r * 0.75f);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, r * 0.5f);
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, r * 0.25f);
    }
}
