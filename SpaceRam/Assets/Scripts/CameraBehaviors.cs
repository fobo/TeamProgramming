using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviors : MonoBehaviour
{
    public GameObject target;

    void Update()
    {
        if (target == null)
        {
            target = GlobalCustom.aquireTarget(gameObject,"Player");
            if (target == null)
                return;
        } 
            
            
        transform.position = new Vector3(target.GetComponent<Transform>().position.x, target.GetComponent<Transform>().position.y, transform.position.z);
        

    }
}
