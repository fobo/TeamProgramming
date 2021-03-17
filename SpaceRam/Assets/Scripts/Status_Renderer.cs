using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Status_Renderer : MonoBehaviour
{

    private GameObject hp_child;
    private float host_hp;
    private float host_max_hp;
    private float default_hp_x_scale;
    public float y_offset = -0.5f;
    private void Start()
    {
        if (transform.root == transform)
        {
            Debug.Log("Status Renderer is not a child. Me die.");
        }
        hp_child = gameObject.transform.Find("hp").gameObject;
        default_hp_x_scale = hp_child.transform.localScale.x;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Status host_status = gameObject.GetComponentInParent<Status>();

        transform.rotation = Quaternion.Euler(0, 0, 0);
        //change the 0.5f to something else
        transform.position = new Vector3(gameObject.transform.parent.transform.position.x, 
                                         gameObject.transform.parent.transform.position.y+y_offset, 
                                         gameObject.transform.parent.transform.position.z);

        if (transform.root == transform)
        {
            Destroy(this);
        }
        if (host_status != null)
        {
            host_max_hp = host_status.max_hp;
            host_hp = host_status.hp; 


        } else
        {
            Debug.Log("No controller parent");
            Destroy(this);
        }

        float percentage_hp = host_hp / host_max_hp;
        hp_child.transform.localScale = new Vector3(default_hp_x_scale * percentage_hp, hp_child.transform.localScale.y, hp_child.transform.localScale.z);

    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, 0.1f);
    }
}
