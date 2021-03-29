using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingController : MonoBehaviour
{

    public GameObject projectile;
    public float frequency_seconds = 1;
    //public Transform Target;
    private Vector2 direction;
    private float current_delay;
    public float Force = 100;
    private EnemyController parentController;

    // Start is called before the first frame update
    void Start()
    {
        current_delay = frequency_seconds;
        parentController = gameObject.GetComponentInParent<EnemyController>();
    }

    

    // Update is called once per frame
    void Update()
    {
        if (parentController.GetComponentInParent<Status>().stunTime > 0)
        {
            return;
        }

        current_delay -= Time.deltaTime;
        if (current_delay <= 0)
        {
            FireProjectile();
            current_delay = frequency_seconds;
        }
    }
   


    void FireProjectile()
    {
        GameObject closest = GlobalCustom.aquireTarget(gameObject, "Player",8);
        if (closest == null) return;

        Vector2 targetPos = closest.transform.position;
        direction = targetPos - (Vector2)transform.position;

        GameObject newBullet = Instantiate(projectile, transform.position, Quaternion.LookRotation(Vector3.forward, direction));
        newBullet.GetComponent<Rigidbody2D>().AddForce(direction.normalized * Force);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, 0.1f);
    }
}
