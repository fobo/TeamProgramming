using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeTime : MonoBehaviour
{
    // Start is called before the first frame update

    public float lifeTimeInSeconds = 5;

    // Update is called once per frame
    void Update()
    {
        lifeTimeInSeconds -= Time.deltaTime;
        if(lifeTimeInSeconds <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
