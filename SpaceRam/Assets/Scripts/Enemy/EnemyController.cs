using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    //public Transform ram_ship;
    private float moveSpeed = 1f;
    private Rigidbody2D rb;
    private Vector2 movement;
    public float stunTime = 0;
    public float hp = 20;
    public float max_hp = 20;

    [SerializeField]
    public float bounce_mod = 3;


    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag != "Player")
        {
            return;
        }

        Rigidbody2D col_rb = col.gameObject.GetComponent<Rigidbody2D>();

        if (col_rb.velocity.magnitude >= hp)
        {
            Vector2 resistance = (col_rb.velocity.normalized) * (hp / 2);
            col_rb.velocity = col_rb.velocity - resistance;
            Destroy(gameObject);
        }
        else
        {
            hp -= col_rb.velocity.magnitude;
            rb.velocity = col_rb.velocity/2;
            col_rb.velocity = (col_rb.velocity.normalized*-2);
            stunTime = 1f;
        }

    }


    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
    }

    GameObject aquireTarget()
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

        return closest;
    }

    // Update is called once per frame
    void Update()
    {
        if (stunTime > 0)
        {
            stunTime -= Time.deltaTime;
            return;
        }

        Vector3 direction = aquireTarget().transform.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        rb.rotation = angle;
        direction.Normalize();
        movement = direction;
    }

    private void FixedUpdate()
    {
       moveCharacter(movement);
    }
    void moveCharacter(Vector2 direction)
    {
        if (stunTime > 0) return;
        rb.MovePosition((Vector2)transform.position + (direction * moveSpeed * Time.deltaTime));

        
    }

    

}//class end
