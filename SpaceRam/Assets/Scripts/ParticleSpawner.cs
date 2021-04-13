using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSpawner : MonoBehaviour
{

    //attach this script to a projectile or other object to have it spawn objects that play an animation



    /*
     * create object behind projectile
     * 
     * 
     */

    public float repeatDelay = 0.2f;
    public float offset = 0.2f;
    public GameObject spawnee; //drag and drop the prefab into this in the inspector
    private float currDelay = 0f;
    private void Update()
    {
        if (currDelay <= 0) {
            currDelay = repeatDelay;
            Vector2 spawnLocation = (Vector2)transform.position + (Vector2)transform.up * -1 * offset;
            Instantiate(spawnee, spawnLocation, new Quaternion(0, 0, 0, 0));
        }
        else
        {
            currDelay -= Time.deltaTime;
        }
    }
}
