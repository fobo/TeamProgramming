using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingController : MonoBehaviour
{

    public GameObject projectile;
    public float frequency_seconds = 1;
    public Transform Target;
    private Vector2 direction;
    private float current_delay;

    // Start is called before the first frame update
    void Start()
    {
        current_delay = frequency_seconds;
    }

    // Update is called once per frame
    void Update()
    {


        current_delay -= Time.deltaTime;
        if (current_delay <= 0)
        {
            FireProjectile();
            current_delay = frequency_seconds;
        }
    }

    void FireProjectile()
    {
        Vector2 targetPos = Target.position;
        direction = targetPos - (Vector2)transform.position;

    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, 0.1f);
    }
}
