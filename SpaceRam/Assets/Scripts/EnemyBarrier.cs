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
}
