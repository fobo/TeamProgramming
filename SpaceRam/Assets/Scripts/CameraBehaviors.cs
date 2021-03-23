using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviors : MonoBehaviour
{
    public GameObject target; //You dont need to drag and drop

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        target = GlobalCustom.aquireTarget(gameObject,"Player");
        if (target == null) return;
        transform.position = new Vector3(target.GetComponent<Transform>().position.x, transform.position.y, transform.position.z);
        

    }
}
