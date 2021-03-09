using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyP2 : MonoBehaviour
{
    public GameObject target;
    public float offset = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        target = Camera.main.GetComponent<CameraBehaviors>().target;
        if (target == null) return;
        transform.position = new Vector3(target.GetComponent<Transform>().position.x + offset, transform.position.y, transform.position.z);
    }

    
}
