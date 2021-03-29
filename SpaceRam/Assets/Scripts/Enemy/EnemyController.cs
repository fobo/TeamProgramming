using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    //public Transform ram_ship;
    private float moveSpeed = 100f;
    private Rigidbody2D rb;
    private BoxCollider2D box2d;
    private Collider2D collider2d;
    private CapsuleCollider2D capsule2d;
    private Vector2 movement;
    private Status myStatus;
    public bool goHome = true;
    private Vector3 home;


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
        home = gameObject.transform.position;
        rb = GetComponent<Rigidbody2D>();
        box2d = GetComponent<BoxCollider2D>();
        capsule2d = GetComponent<CapsuleCollider2D>();
        if (box2d == null && capsule2d == null)
        {
            Debug.Log("Dumby you forgot a collider!");
            Destroy(this);
        } else if (box2d == null)
        {
            collider2d = (Collider2D)capsule2d;
        } else
        {
            collider2d = (Collider2D)box2d;
        }
        myStatus = GetComponent<Status>();
    }

    

    // Update is called once per frame
    void Update()
    {
        if(myStatus.hp <= 0)
        {
            Destroy(gameObject);
        }

        if (myStatus.stunTime > 0)
        {
            return;
        }

        //BoxCollider2D cameraBox = Camera.main.GetComponent<BoxCollider2D>();
        //if (cameraBox != null) {
        //    if (!collider2d.IsTouching(cameraBox))
        //    {
        //        Vector3 dir;// = Camera.main.transform.position - transform.position;
        //        float vDistFromCamera = Camera.main.transform.position.y - transform.position.y;
        //        float hDistFromCamera = Camera.main.transform.position.x - transform.position.x;
        //        if (Mathf.Abs(vDistFromCamera) > Mathf.Abs(hDistFromCamera))
        //        {
        //            dir = new Vector3(0, vDistFromCamera, 0);
        //        } else
        //        {
        //            dir = new Vector3(hDistFromCamera, 0, 0);
        //        }

        //        dir.Normalize();
        //        moveCharacter(dir);
        //    }
        //}

        GameObject target = GlobalCustom.aquireTarget(gameObject,"Player", 8);
        if(target == null)
        {
            return;
        }
        Vector3 direction = home - transform.position;
        direction.Normalize();
        Vector3 targetDirection = target.transform.position - transform.position;
        float angle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg;
        rb.rotation = angle;
        movement = direction;
    }

    private void FixedUpdate()
    {
        if(goHome) { 
            moveCharacter(movement);
        }
    }
    void moveCharacter(Vector2 direction)
    {
        if (myStatus.stunTime > 0) return;
        rb.velocity = (direction * Mathf.Min(moveSpeed, moveSpeed * 2 * Mathf.Abs(home.magnitude - transform.position.magnitude)) * Time.deltaTime);
        
    }




}//class end
