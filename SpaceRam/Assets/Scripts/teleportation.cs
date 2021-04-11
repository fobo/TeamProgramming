using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class teleportation : MonoBehaviour
{
    public GameObject Portal;
    public GameObject Player;
    public Animator animator;
    public float delayBetweenTeleports = 2f;
    // Start is called before the first frame update

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        float downTime = animator.GetFloat("DisabledTimer");
        if (downTime > 0) animator.SetFloat("DisabledTimer", downTime - Time.deltaTime);
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (animator.GetFloat("DisabledTimer") > 0) return;
        if (other.gameObject.tag == "Player")
        {
            Teleport();
        }
    }

    void Teleport()
    {
        animator.SetFloat("DisabledTimer", delayBetweenTeleports);
        if (Portal == null) {
            Portal = GlobalCustom.aquireTarget(gameObject, "Portal");
        }
        Portal.GetComponent<teleportation>().animator.SetFloat("DisabledTimer", delayBetweenTeleports);

        SoundManagerScript.PlaySound("shipCollectPickupSound");
        Player = GlobalCustom.aquireTarget(gameObject, "Player");
        Player.transform.position = new Vector2(Portal.transform.position.x, Portal.transform.position.y);
    }
}
