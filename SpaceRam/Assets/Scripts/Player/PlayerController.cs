using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{

    Vector3 clickPoint;
    float rotationSpeed;
    float maxDashPower = 1000;
    float curDashPower = 0;
    Animator anim;
    Rigidbody2D rb;
    Status myStatus;
    SpriteRenderer mySprite;

    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        myStatus = GetComponent<Status>();
        clickPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mySprite = GetComponent<SpriteRenderer>();
        rotationSpeed = 1440;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        //anim.speed = 0f;
        //score = 0;
        // score
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        //scoreText = GetComponent<Text>();
        //game.FindGameObjectWithTag("ScoreTextTag");
    }

    void OnTriggerEnter2D(Collider2D col)
    {


        if (myStatus.invincibilityTime > 0) return;
        Status col_status = col.gameObject.GetComponent<Status>();
        Rigidbody2D col_rb = col.gameObject.GetComponent<Rigidbody2D>();

        if (col.gameObject.tag == "Projectile")
        {
            SoundManagerScript.PlaySound("shipHitSound");
            float damage = col_status.damage;
            myStatus.hp -= damage;
            myStatus.invincibilityTime = 1f;
            mySprite.color = new Color(1f, 1f, 1f, .5f);
        }

        if (!(col_status == null) && !(col_rb == null))
        {
            
            if (col.gameObject.tag == "Enemy")
            {

                if (rb.velocity.magnitude >= col_status.hp)
                {
                    //cronch sound
                    SoundManagerScript.PlaySound("shipKillEnemySound");
                    Vector2 resistance = (rb.velocity.normalized) * (col_status.hp / 3);
                    rb.velocity = rb.velocity - resistance;
                    gameManager.UpdateScore(10);
                    Destroy(col.gameObject);
                }
                else
                {
                    //bonk sound
                    SoundManagerScript.PlaySound("shipBounceEnemySound");
                    col_status.hp -= rb.velocity.magnitude;
                    myStatus.hp -= rb.velocity.magnitude / 4;
                    col_rb.velocity = rb.velocity / 2;
                    rb.velocity = (col_rb.velocity.normalized * -2);
                    col_status.stunTime = 1f;
                    myStatus.invincibilityTime = 1f;
                }
            }
        }
    }


    private void LateUpdate()
    {
        if (myStatus.hp <= 0)
        {
            SoundManagerScript.PlaySound("shipDieSound");
            Destroy(gameObject);
            gameManager.GameOver();
        }

        if (myStatus.invincibilityTime > 0)
        {
            mySprite.color = new Color(1f, 1f, 1f, .5f);
        } 
        else
        {
            mySprite.color = new Color(1f, 1f, 1f, 1f);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        clickPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);



        if (Input.GetMouseButton(0))
        {
            var step = rotationSpeed * Time.deltaTime;
            var rotationGoal = Quaternion.LookRotation(Vector3.forward, clickPoint - transform.position);
            //Debug.Log(rotationGoal * Quaternion.Inverse(transform.rotation));
            transform.rotation = Quaternion.RotateTowards(transform.rotation, rotationGoal, step);
            rb.velocity -= (rb.velocity*4) * Time.deltaTime;
            rb.angularVelocity = 0;

            curDashPower = Mathf.Clamp(curDashPower+ (maxDashPower*Time.deltaTime), maxDashPower/4, maxDashPower);
        
        }

        //Debug.Log(Mathf.Round((curDashPower / maxDashPower)));
        anim.Play("ram_ship", 0, (curDashPower / maxDashPower)*.999f); ; ; ; ; ; ;

        if (Input.GetMouseButton(1))
        {
            rb.velocity -= (rb.velocity * 4) * Time.deltaTime;
        }

        if (Input.GetMouseButtonUp(0))
        {
            //Debug.Log(transform.forward * dashPower);
            transform.rotation = Quaternion.LookRotation(Vector3.forward, clickPoint - transform.position);
            rb.velocity = Vector2.zero;
            rb.AddForce(transform.up * curDashPower);
            curDashPower = 0;
        }

        Camera camera = Camera.main;
        float topBoundary = 5;
        float bottomBoundary = -5;
        float padding = 0f; //in case a bug starts happening where the ship gets stuck in the boundaries and starts vibrating, increase this value

        if (transform.position.y > topBoundary || 
            transform.position.y < bottomBoundary) {
            float curZ = transform.rotation.eulerAngles.z;
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, curZ + (270-curZ)*2); //270 because 360/0 is vertical and 270 is horizontal
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * -1);
            transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, bottomBoundary + padding, topBoundary - padding),0);
            
        }
    }
}
