using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Transform ram_ship;
    public float moveSpeed = 20f;
    private Rigidbody2D rb;
    private Vector2 movement;


    int hp = 20;


    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = ram_ship.position - transform.position;
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

    void OnCollisionEnter2D(Collision2D col)
    {
        Debug.Log("Test");
        Debug.Log(col.gameObject.tag);

        if (col.gameObject.tag == "Player")
        {
            Destroy(this.gameObject);
        }
    }

}//class end
