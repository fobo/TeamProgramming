using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementPatternController : MonoBehaviour
{
    private Rigidbody2D rb;
    private BoxCollider2D box2d;
    private Collider2D collider2d;
    private CapsuleCollider2D capsule2d;
    private Vector2 movement;
    private Status myStatus;
    private GameObject target;
    private List<GameObject> currentOverlaps = new List<GameObject>();

    public enum PatternType
    {
        GUARD, //Drifts back to a single point where it spawned
        APPROACH, //Heads towards the player but tries to keep a distance weakly
        PATROL
    };

    public enum MoveType
    {
        MOMENTUM,
        STRICT
    };

    //public Transform ram_ship;
    public PatternType patternType = PatternType.GUARD;
    public MoveType moveType = MoveType.MOMENTUM;
    public float moveSpeed = 5f;
    public float maxSpeed = 10f;
    public float rotationOffset = 0f;
    public float approachRange = 4f;
    public float detectRange = 6f;
    public float socialDistanceSpeed = 3f;
    public Vector3 home;
    public Vector3[] patrolRoute; //change to special prefab array


    private void OnTriggerEnter2D(Collider2D other)
    {
        if(!currentOverlaps.Contains(other.gameObject))
        {
            currentOverlaps.Add(other.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (currentOverlaps.Contains(other.gameObject))
        {
            currentOverlaps.Remove(other.gameObject);
        }
    }

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

        if (rb == null)
        {
            Debug.Log("No Rigid Body, EnemyController deletes itself");
            Destroy(gameObject);
        }
        if (collider2d == null)
        {
            Debug.Log("No 2D Collider, EnemyController deletes itself");
            Destroy(gameObject);
        }
    }
    

    void Update()
    {
        if (myStatus != null && myStatus.hasLifeTime != true) DieIfDead();
    }

    void DieIfDead()
    {
        if (myStatus.hp <= 0) Destroy(gameObject);
    }

    private void FixedUpdate()
    {
        //Don't do movement if stunned, but you can't be stunned without a status
        if (myStatus != null && myStatus.stunTime > 0) return;
        UpdateTarget();
        FaceTarget();
        OverlapBehaviors();

        switch (patternType)
        {
            case PatternType.GUARD:
                GuardMovement();
                break;
            case PatternType.APPROACH:
                ApproachMovement();
                break;
            case PatternType.PATROL:

                break;

        }
    }

    void OverlapBehaviors()
    {
        if (currentOverlaps.Count <= 0) {
            return;
        }

        foreach(GameObject overlappy in currentOverlaps)
        {
            if(overlappy.tag == "Enemy") { 
                Vector3 direction = overlappy.transform.position - transform.position;
                direction.Normalize();
                rb.AddForce(direction*socialDistanceSpeed*-1);
            }
        }
    }

    void MoveTowards(Vector3 goalPosition)
    {
        switch (moveType)
        {
            case MoveType.MOMENTUM:
                MoveMomentum(goalPosition);
                break;
            case MoveType.STRICT:
                MoveStrict(goalPosition);
                break;
        }
    }

    void MoveStrict(Vector3 goalPosition)
    {
        Vector3 direction = home - transform.position;
        direction.Normalize();
        rb.velocity = (direction * moveSpeed);
        float distance = Vector3.Distance(goalPosition, transform.position);
        if (distance <= rb.velocity.magnitude*Time.deltaTime)
        {
            rb.velocity = Vector2.zero;
           transform.position = goalPosition;
        }
        
    
    }

    void MoveMomentum(Vector3 goalPosition)
    {

    }

    void GuardMovement()
    {
        //Vector3 direction = home - transform.position;
        //direction.Normalize();
        //rb.velocity = (direction * Mathf.Min(moveSpeed, moveSpeed * 2 * Mathf.Abs(home.magnitude - transform.position.magnitude)) * Time.deltaTime);
        MoveStrict(home); //change to movetowards
    }

    void ApproachMovement()
    {
        //Check distance from player
        if (target == null) return;
        float distance = Vector3.Distance(transform.position, target.transform.position);
        if(distance > approachRange)
        {
            Vector3 direction = target.transform.position - transform.position;
            direction.Normalize();
            rb.AddForce(direction * moveSpeed);
            rb.velocity = Vector2.ClampMagnitude(rb.velocity, maxSpeed);
        }
        //If too far away add force up to a clamped speed (clamp magnitude of velocity)
        //if not, reduce speed

    }


    void UpdateTarget()
    {
        target = GlobalCustom.aquireTarget(gameObject, "Player", detectRange);
    }
    void FaceTarget()
    {
        if (target == null) return;
        Vector3 targetDirection = target.transform.position - transform.position;
        float angle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg;
        rb.rotation = angle + rotationOffset;
    }


    void OnDrawGizmosSelected()
    {
        //Approach circle
        Gizmos.color = new Color(1, 1, 0, 0.75f);
        Gizmos.DrawWireSphere(transform.position, approachRange);

        //Detect Range Circle
        Gizmos.color = new Color(1, 1, 1, 0.75f);
        Gizmos.DrawWireSphere(transform.position, detectRange);
    }





}//class end
