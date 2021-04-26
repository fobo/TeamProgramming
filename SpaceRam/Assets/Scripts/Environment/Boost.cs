using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boost : MonoBehaviour
{

    public float boostAmt = 0; //50 to 1 magnitude a frame
    public float boostMaxMagnitude = -1;
    private Rigidbody2D rb = null;
    private bool playerIsColliding = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.gameObject.tag == "Player")
        {
            playerIsColliding = true;
            rb = collision.gameObject.GetComponent<Rigidbody2D>();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerIsColliding = false;
            rb = null;
        }
    }

    private void FixedUpdate()
    {
        if (rb != null && playerIsColliding == true)
        {
            if(boostMaxMagnitude == -1)
            {
                rb.AddForce((Vector2)transform.right * boostAmt);

            } else
            {
                Vector3 rbV = rb.velocity;
                Vector3 rbVnorm = rb.velocity.normalized;
                Vector3 correctDirNorm = transform.right;

                float magInBoostDir = Vector3.Dot(rbV, correctDirNorm);
                
                Vector3 projection = magInBoostDir * rbVnorm;
                Vector3 clampedProjection = Vector3.ClampMagnitude(projection, boostMaxMagnitude);
                //Debug.Log(magInBoostDir); //50 on add force == 1 vel magnitude

                if(magInBoostDir >= boostMaxMagnitude)
                {
                    rb.velocity = ((Vector3)rb.velocity - projection) + clampedProjection;
                    return;
                } else
                {
                    rb.AddForce((Vector2)transform.right * boostAmt);
                }
            }

        }
        //rb.velocity = rb.velocity + (Vector2)transform.right * boostAmt;
    }
}
