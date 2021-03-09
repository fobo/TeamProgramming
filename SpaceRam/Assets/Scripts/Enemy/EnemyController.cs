using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    //public Transform ram_ship;
    private float moveSpeed = 500f;
    private Rigidbody2D rb;
    private BoxCollider2D box2d;
    private Vector2 movement;
    private Status myStatus;


    //void OnTriggerEnter2D(Collider2D col)
    //{
    //    if(col.gameObject.tag != "Player")
    //    {
    //        return;
    //    }

    //    Rigidbody2D col_rb = col.gameObject.GetComponent<Rigidbody2D>();

    //    if (col_rb.velocity.magnitude >= hp)
    //    {
    //        Vector2 resistance = (col_rb.velocity.normalized) * (hp / 2);
    //        col_rb.velocity = col_rb.velocity - resistance;
    //        Destroy(gameObject);
    //    }
    //    else
    //    {
    //        hp -= col_rb.velocity.magnitude;
    //        rb.velocity = col_rb.velocity/2;
    //        col_rb.velocity = (col_rb.velocity.normalized*-2);
    //        stunTime = 1f;
    //    }

    //}


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        box2d = GetComponent<BoxCollider2D>();
        myStatus = GetComponent<Status>();
    }

    //GameObject aquireTarget()
    //{
    //    GameObject[] possibleTargets = GameObject.FindGameObjectsWithTag("Player");
    //    if(possibleTargets.Length == 0)
    //    {
    //        return null;
    //    }
    //    GameObject closest = possibleTargets[0];
    //    foreach (GameObject target in possibleTargets)
    //    {

    //        //float distance = Vector3.Distance(other.position, transform.position);
    //        if (Vector3.Distance(target.transform.position, transform.position) < Vector3.Distance(closest.transform.position, transform.position))
    //        {
    //            closest = target;
    //        }
    //    }

    //    return closest;
    //}

    // Update is called once per frame
    void Update()
    {
        if (myStatus.stunTime > 0)
        {
            return;
        }

        BoxCollider2D cameraBox = Camera.main.GetComponent<BoxCollider2D>();
        if (cameraBox != null) {
            if (!box2d.IsTouching(cameraBox))
            {
                Vector3 dir;// = Camera.main.transform.position - transform.position;
                float vDistFromCamera = Camera.main.transform.position.y - transform.position.y;
                float hDistFromCamera = Camera.main.transform.position.x - transform.position.x;
                if (Mathf.Abs(vDistFromCamera) > Mathf.Abs(hDistFromCamera))
                {
                    dir = new Vector3(0, vDistFromCamera, 0);
                } else
                {
                    dir = new Vector3(hDistFromCamera, 0, 0);
                }

                dir.Normalize();
                moveCharacter(dir);
            }
        }

        GameObject target = GlobalCustom.aquireTarget(gameObject,"Player");
        if(target == null)
        {
            return;
        }
        Vector3 direction = target.transform.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        rb.rotation = angle;
        //direction.Normalize();
        //movement = direction;
    }

    private void FixedUpdate()
    {
       //moveCharacter(movement);
    }
    void moveCharacter(Vector2 direction)
    {
        //if (myStatus.stunTime > 0) return;
        rb.velocity = (direction * moveSpeed * Time.deltaTime);
    }




}//class end
