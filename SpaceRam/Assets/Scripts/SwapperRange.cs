using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwapperRange : MonoBehaviour
{
    public string targetTag = "Player";
    public float range = 2;
    public GameObject swap = null;

    // Update is called once per frame
    void Update()
    {
        GameObject target = GlobalCustom.aquireTarget(gameObject, targetTag, range);
        if (target != null)
        {
            if(swap != null)
            {
                Instantiate(swap,transform.position, Quaternion.identity);
            }
            Destroy(transform.parent.gameObject);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireCube(transform.position, new Vector3(1, 1, 1));
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position, range);
    }


}
