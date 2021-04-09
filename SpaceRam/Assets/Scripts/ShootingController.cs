using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingController : MonoBehaviour
{

    public GameObject projectile;
    public float frequency_seconds = 1;
    public float variation_seconds = 0f;
    //public Transform Target;
    private Vector2 direction;
    private float current_delay;
    public float Force = 100;
    public EnemyController parentController;
    public float shot_delay = 1f; //shot delay setting
    private float remaining_shot_delay = 0f;
    private bool isShooting = false;

    // Start is called before the first frame update
    void Start()
    {
        current_delay = frequency_seconds;
        parentController = gameObject.GetComponentInParent<EnemyController>();
    }

    

    // Update is called once per frame
    void Update()
    {
        if (parentController != null && parentController.GetComponentInParent<Status>().stunTime > 0)
        {
            return;
        }

        if (isShooting)
        {
            remaining_shot_delay -= Time.deltaTime;
            if(remaining_shot_delay <= 0)
            {
                isShooting = false;
                FireProjectile();

            } else
            {
                float scaleAmt = 2*(1 - (remaining_shot_delay / shot_delay));
                transform.localScale = new Vector3(scaleAmt, scaleAmt, scaleAmt);
            }
        } else
        {
            transform.localScale = new Vector3(0, 0, 0);
        }

        current_delay -= Time.deltaTime;
        if (current_delay <= 0)
        {
            isShooting = true;
            remaining_shot_delay = shot_delay;
            current_delay = frequency_seconds + Random.Range(variation_seconds*-1, variation_seconds);
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
