using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{

    Vector3 clickPoint;
    float rotationSpeed;
    float maxDashPower = 500; //50 == 1 magnitude
    float storedDashPower = 0;
    float bonkTimer = 0.5f;


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


    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Bounce();
    }

    void OnTriggerEnter2D(Collider2D col)
    {

        //Debug.Log("My Magnitude: "+rb.velocity.magnitude);

        

        Status theirStatus = col.gameObject.GetComponent<Status>();
        Rigidbody2D col_rb = col.gameObject.GetComponent<Rigidbody2D>();
        float myMagnitude = rb.velocity.magnitude;


        if (col.gameObject.tag == "Projectile" /*&& myStatus.projInvincibilityTime <= 0*/)
        {
            SoundManagerScript.PlaySound("shipHitSound");
            float damage = theirStatus.damage;
            myStatus.hp -= damage;
            //myStatus.projInvincibilityTime = 0.1f; //1/10th of a second invicibility on projectiles
            mySprite.color = new Color(1f, 1f, 1f, .5f);
        }

        if (myStatus.invincibilityTime > 0) return;

        if (!(theirStatus == null) && !(col_rb == null))
        {
            float theirHP = theirStatus.hp;
            float theirDamage = myMagnitude; //last var makes it so only half your hp is doing damage
            float myDamage = myMagnitude/10; //last variable makes it so enemies only use a fraction of their hp to damage
            //Debug.Log("My Damage: " + myDamage);
            float killBonus =  0; //reduces damage if enemy dies

            if (col.gameObject.tag == "Enemy")
            {

                if (theirDamage >= theirHP) //they got killed
                {
                    //cronch sound
                    SoundManagerScript.PlaySound("shipKillEnemySound");
                    gameManager.UpdateScore(10);
                    killBonus = 0.5f; //take half damage if they get killed
                    theirStatus.hp = 0;
                    //Destroy(col.gameObject);
                }
                else
                {
                    //bonk sound
                    SoundManagerScript.PlaySound("shipBounceEnemySound");
                    theirStatus.hp -= theirDamage;
                    col_rb.velocity = rb.velocity / 4;
                    rb.velocity = (col_rb.velocity.normalized * -1);
                    theirStatus.stunTime = bonkTimer;
                    myStatus.invincibilityTime = 0.5f; //Gain 1 second of invicibility on bonks so you don't get chain bonked
                }

                myStatus.hp -= myDamage - myDamage*killBonus; //Always take damage


            }
        }
    }


    private void LateUpdate()
    {

        //Debug.Log(rb.velocity.magnitude);
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
            rb.velocity -= (rb.velocity * 4) * Time.deltaTime;
            rb.angularVelocity = 0;

            storedDashPower = Mathf.Clamp(storedDashPower + (maxDashPower * Time.deltaTime), maxDashPower / 4, maxDashPower);

        } else
        {

            if (rb.velocity != Vector2.zero)
            {
                float angle = Mathf.Atan2(rb.velocity.y, rb.velocity.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
            }
        }

        //Debug.Log(Mathf.Round((curDashPower / maxDashPower)));
        anim.Play("ram_ship", 0, (storedDashPower / maxDashPower) * .999f); ; ; ; ; ; ;

        if (Input.GetMouseButton(1))
        {
            rb.velocity -= (rb.velocity * 4) * Time.deltaTime;
        }

        if (Input.GetMouseButtonUp(0))
        {
            //500 dash power, 50 == 1 magnitude
            //Debug.Log(transform.forward * dashPower);
            transform.rotation = Quaternion.LookRotation(Vector3.forward, clickPoint - transform.position);
            float currentDashPower = rb.velocity.magnitude * 50;
            if (storedDashPower < currentDashPower)
            {
                storedDashPower = currentDashPower;
            }
            rb.velocity = Vector2.zero;
            rb.AddForce(transform.up * storedDashPower); 
            storedDashPower = 0;
        }

        //Camera camera = Camera.main;
        //float topBoundary = 5;
        //float bottomBoundary = -5;
        
        //if (transform.position.y > topBoundary ||
        //    transform.position.y < bottomBoundary)
        //{
        //    //Bounce();
        //    rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * -1);
        //    transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, bottomBoundary, topBoundary), 0);

        //}
    }

    //void Bounce() {
    //    float curZ = transform.rotation.eulerAngles.z;
    //    transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, curZ + (270 - curZ) * 2); //270 because 360/0 is vertical and 270 is horizontal
        

    //}

}
