using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraChange : MonoBehaviour
{

    public Camera new_camera = null;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Camera current_camera = Camera.current;


            new_camera.enabled = true;
            new_camera.tag = "MainCamera";
            current_camera.enabled = false;
            current_camera.tag = "Untagged";
        }
    }
}
