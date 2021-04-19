using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pickup : MonoBehaviour
{

    public enum Type
    {
        healSmall, // 10 hp
        healLarge,  // 20 hp
        levelChange,
        coin
    }
    public Type type = Type.healSmall;
    public Sprite sprHealSmall, sprHealLarge, sprLevelChange;
    public string levelName = "Debug_Level";
    public int pointValue;
    private GameManager gameManager;

    private void Start()
    {
        SpriteRenderer sprite = GetComponent<SpriteRenderer>();
        //spriteRenderer.sprite = newSprite; 
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();

        

            switch (type)
            {
                case (Type.healSmall):
                    sprite.sprite = sprHealSmall;
                    break;
                case (Type.healLarge):
                    sprite.sprite = sprHealLarge;
                    break;
                case (Type.levelChange):
                    sprite.sprite = sprLevelChange;
                    break;
            default:
                break;

        }
    }
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
                case (Type.levelChange):
                    SceneManager.LoadScene(levelName);
                    break;
                case (Type.coin):
                    addScore(collision, pointValue);
                    break;
            }
        }
    }
    private void healTarget(Collider2D collision, float healAmount)
    {

        //heals player for healAmount amount
        SoundManagerScript.PlaySound("shipCollectPickupSound");
        collision.gameObject.GetComponent<Status>().hp += healAmount;
            Destroy(gameObject);
        
    }
    private void addScore(Collider2D collision, int point)
    {
        //add sound effects here (Maybe cooler sound effects for more points?
        //SoundManagerScript.PlaySound("shipCollectCoin");
        gameManager.UpdateScore(point); // adds to score total
        Destroy(gameObject);
    }
}
