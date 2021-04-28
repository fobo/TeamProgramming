using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalCustom : MonoBehaviour
{
    public static GameObject aquireTarget(GameObject me, string tag)
    {
        return aquireTarget(me, tag, -1);
    }

    public static GameObject aquireTarget(GameObject me, string tag, float range)
    {
        if (tag == "notag") return null;

        GameObject[] possibleTargets = GameObject.FindGameObjectsWithTag(tag);
        if (possibleTargets.Length == 0)
        {
            return null;
        }
        GameObject closest = null;
        foreach (GameObject target in possibleTargets)
        {
            if (target.Equals(me)) continue;

            float currentDistance = Vector2.Distance((Vector2)target.transform.position, (Vector2)me.transform.position);
            Debug.Log(currentDistance);

            if (range > 0)
            {
                if (currentDistance > range)
                {
                    continue;
                }

                
            }

            //float distance = Vector3.Distance(other.position, transform.position);

            if (closest == null)
            {
                closest = target;
            } 
            else if (currentDistance < Vector2.Distance((Vector2)closest.transform.position, (Vector2)me.transform.position))
            {
                closest = target;
            }
        }

        return closest;
    }
}
