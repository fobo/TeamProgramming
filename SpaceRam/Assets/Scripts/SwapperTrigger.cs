using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwapperTrigger : MonoBehaviour
{

    public string targetTag = "Player";
    public GameObject swap = null;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == targetTag)
        {
            if(swap != null) {
                Instantiate(swap, transform.position, Quaternion.identity);
            }
            Destroy(transform.parent.gameObject);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireCube(transform.position, new Vector3(1, 1, 1));
    }
    
}
