using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingController : MonoBehaviour
{

    public enum TargetType
    {
        PARENTS,
        CUSTOM,
        NONE
    };

    public TargetType targetType = TargetType.PARENTS;
    public float customRange = 8;
    public string targetTag = "Player";

    public GameObject projectile;
    public float delay_between_shots = 1;
    public float variation_seconds = 0f;
    //public Transform Target;
    private Vector2 direction;
    private float current_delay;
    public float Force = 100;
    public MovementPatternController parentController;
    public float shot_delay = 1f; //shot delay setting
    private float remaining_shot_delay = 0f;
    private bool isShooting = false;

    // Start is called before the first frame update
    void Start()
    {
        current_delay = 0f;
        parentController = gameObject.GetComponentInParent<MovementPatternController>();
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
            if (remaining_shot_delay <= 0)
            {
                isShooting = false;
                FireProjectile();

            }
            else
            {
                float scaleAmt = 2 * (1 - (remaining_shot_delay / shot_delay));
                transform.localScale = new Vector3(scaleAmt, scaleAmt, scaleAmt);
            }
        }
        else
        {
            transform.localScale = new Vector3(0, 0, 0);
        }

        current_delay -= Time.deltaTime;
        if (current_delay <= 0)
        {
            isShooting = true;
            remaining_shot_delay = shot_delay;
            current_delay = delay_between_shots + Random.Range(variation_seconds * -1, variation_seconds);
        }
    }


    GameObject AquireTarget()
    {
        GameObject closest = null;
        if (targetType == TargetType.PARENTS)
        {
            if (parentController == null)
            {
                closest = GlobalCustom.aquireTarget(gameObject, targetTag, customRange);
                
            }
            else
            {
                closest = GlobalCustom.aquireTarget(gameObject, parentController.targetTag, parentController.detectRange);
            }

        }
        else if (targetType == TargetType.CUSTOM)
        {
            closest = GlobalCustom.aquireTarget(gameObject, targetTag, customRange);
        }

        return closest;
    }

    void FireProjectile()
    {
        GameObject closest = AquireTarget();
        if (closest == null) return;



        Vector2 targetPos = closest.transform.position;
        direction = targetPos - (Vector2)transform.position;

        GameObject newBullet = Instantiate(projectile, transform.position, Quaternion.LookRotation(Vector3.forward, direction));
        Rigidbody2D newRB = newBullet.GetComponent<Rigidbody2D>();
        if (newRB != null)
        {
            newRB.AddForce(direction.normalized * Force);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, 0.1f);
    }

    private void OnDrawGizmosSelected()
    {

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, customRange);
        GameObject closest = AquireTarget();
        if (closest == null) return;
        Gizmos.DrawLine(transform.position, closest.transform.position);
    }
}
