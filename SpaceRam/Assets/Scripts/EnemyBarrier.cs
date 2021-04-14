using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBarrier : MonoBehaviour
{

    public GameObject[] guardians;


    
    void Update()
    {
        foreach (GameObject current in guardians)
        {
            if (current != null) return;
        }

        Destroy(gameObject);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        if (guardians.Length == 0) return;
        foreach (GameObject guard in guardians)
        {
            if (guard == null) continue;
            Gizmos.DrawLine(transform.position, guard.transform.position);
        }
    }
}
