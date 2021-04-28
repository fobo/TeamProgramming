using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeOnStart : MonoBehaviour
{

    public float change_x_scale_by = 0f;
    public float change_y_scale_by = 0f;


    void Start()
    {
        transform.localScale += new Vector3(change_x_scale_by, change_y_scale_by, 1f);
    }

}
