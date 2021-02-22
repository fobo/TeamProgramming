using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    int hp = 20;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter2D(Collision2D col)
    {
        Debug.Log("Test");
        Debug.Log(col.gameObject.tag);

        if (col.gameObject.tag == "Player")
        {
            Destroy(this.gameObject);
        }
    }

}//class end
