using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionSurvivalScript : MonoBehaviour
{

    public float hp = 100;

    void OnCollisionEnter2D(Collision2D col)
    {
        //Debug.Log("Velocity: " + col.gameObject.GetComponent<Rigidbody2D>().velocity.magnitude);

        //if (col.gameObject.tag == "Player")
        //{
        //    Destroy(this.gameObject);
        //}
    }
}
