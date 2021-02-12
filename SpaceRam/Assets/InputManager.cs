using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{


    Vector3 clickPoint;
    float rotationSpeed;
    float dashPower = 1000;
    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        clickPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        rotationSpeed = 1440;
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        clickPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetMouseButton(0))
        {
            var step = rotationSpeed * Time.deltaTime;
            var rotationGoal = Quaternion.LookRotation(Vector3.forward, clickPoint - transform.position);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, rotationGoal, step);
            rb.velocity -= (rb.velocity*4) * Time.deltaTime;
        }

        if(Input.GetMouseButtonUp(0))
        {
            Debug.Log(transform.forward * dashPower);
            transform.rotation = Quaternion.LookRotation(Vector3.forward, clickPoint - transform.position);
            rb.velocity = Vector2.zero;
            rb.AddForce(transform.up * dashPower);
        }

        Camera camera = Camera.main;
        float topBoundary = (camera.transform.position.y - camera.orthographicSize);
        float bottomBoundary = (camera.transform.position.y + camera.orthographicSize);

        if (transform.position.y < topBoundary || 
            transform.position.y > bottomBoundary) {
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z*-1);
            transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, topBoundary, bottomBoundary));
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * -1);
        }
    }
}
