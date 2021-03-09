using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalCustom : MonoBehaviour
{
    public static GameObject aquireTarget(GameObject me, string tag)
    {
        GameObject[] possibleTargets = GameObject.FindGameObjectsWithTag(tag);
        if (possibleTargets.Length == 0)
        {
            return null;
        }
        GameObject closest = possibleTargets[0];
        foreach (GameObject target in possibleTargets)
        {

            //float distance = Vector3.Distance(other.position, transform.position);
            if (Vector3.Distance(target.transform.position, me.transform.position) < Vector3.Distance(closest.transform.position, me.transform.position))
            {
                closest = target;
            }
        }

        return closest;
    }
}
