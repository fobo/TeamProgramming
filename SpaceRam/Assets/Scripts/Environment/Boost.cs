using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boost : MonoBehaviour
{

    public float boostAmt = 0;
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
            rb.AddForce((Vector2)transform.right * boostAmt);
        }
        //rb.velocity = rb.velocity + (Vector2)transform.right * boostAmt;
    }
}
