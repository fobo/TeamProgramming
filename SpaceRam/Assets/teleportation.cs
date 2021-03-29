using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class teleportation : MonoBehaviour
{
    public GameObject Portal;
    public GameObject Player;
    public float delayBetweenTeleports = 2f;
    public float downTime = 0f;
    // Start is called before the first frame update

    private void Update()
    {
        if (downTime > 0) downTime -= Time.deltaTime;
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (downTime > 0) return;
        if (other.gameObject.tag == "Player")
        {
            Teleport();
        }
    }

    void Teleport()
    {
        downTime = delayBetweenTeleports;
        if (Portal == null) {
            Portal = GlobalCustom.aquireTarget(gameObject, "Portal");
        }
        Portal.GetComponent<teleportation>().downTime = delayBetweenTeleports;

        Player = GlobalCustom.aquireTarget(gameObject, "Player");
        Player.transform.position = new Vector2(Portal.transform.position.x, Portal.transform.position.y);
    }
}
