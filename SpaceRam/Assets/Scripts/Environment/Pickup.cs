using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{

    public enum Type
    {
        healSmall, // 10 hp
        healLarge  // 20 hp
    }
    public Type type = Type.healSmall;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //checks if player colliding
        if (collision.gameObject.tag == "Player")
        {
            switch (type)
            {
                case (Type.healSmall):
                    healTarget(collision, 10);
                    break;
                case (Type.healLarge):
                    healTarget(collision, 20);
                    break;

            }
        }
    }
    private void healTarget(Collider2D collision, float healAmount)
    {

        //heals player for healAmount amount
        SoundManagerScript.PlaySound("shipCollectPickup");
        collision.gameObject.GetComponent<Status>().hp += healAmount;
            Destroy(gameObject);
        
    }
}
