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
        foreach (GameObject guard in guardians)
        {
            Gizmos.DrawLine(transform.position, guard.transform.position);
        }
    }
}
