using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    //public Transform ram_ship;
    private float moveSpeed = 1f;
    private Rigidbody2D rb;
    private Vector2 movement;


    int hp = 20;


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
        rb.MovePosition((Vector2)transform.position + (direction * moveSpeed * Time.deltaTime));
    }

    

}//class end
