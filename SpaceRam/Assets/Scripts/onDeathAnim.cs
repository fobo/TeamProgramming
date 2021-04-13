using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class onDeathAnim : MonoBehaviour
{
    public GameObject postMortemObject; // drag and drop the death anim here in inspector
    private void OnDestroy()
    {
        Instantiate(postMortemObject, (Vector2)transform.position, new Quaternion(0, 0, 0, 0));
    }
}
