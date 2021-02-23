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
        GameObject[] possibleTargets = GameObject.FindGameObjectsWithTag("Player");
        GameObject closest = possibleTargets[0];
        foreach (GameObject target in possibleTargets)
        {

            //float distance = Vector3.Distance(other.position, transform.position);
            if (Vector3.Distance(target.transform.position, transform.position) < Vector3.Distance(closest.transform.position, transform.position))
            {
                closest = target;
            }
        }

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
