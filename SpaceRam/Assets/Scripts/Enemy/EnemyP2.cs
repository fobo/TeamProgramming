using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyP2 : MonoBehaviour
{
    public GameObject target;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(target.GetComponent<Transform>().position.x + 8, transform.position.y, transform.position.z);
    }
}
