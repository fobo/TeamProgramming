using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieOnContact : MonoBehaviour
{
    bool amDie = false;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        GameObject colObj = collider.gameObject;
        if (colObj.tag == "Player" || colObj.tag == "Wall")
        {
            amDie = true;
        }
    }

    private void LateUpdate()
    {
        if(amDie)
        {
            Destroy(gameObject);
        }
    }
}
