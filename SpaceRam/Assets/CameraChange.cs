using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraChange : MonoBehaviour
{


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Camera current_camera = Camera.main;
            current_camera.transform.position = new Vector3(transform.position.x,transform.position.y,current_camera.transform.position.z);
        }
    }
}
