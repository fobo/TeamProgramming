﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManagerScript : MonoBehaviour
{
    public static AudioClip shipHitSound, shipPowerSound, shipDieSound, shipKillEnemySound, shipBounceEnemySound,
        shipCollectPickupSound, shipTeleportSound, shipSpeedBoost, shipCollectCoin;
    static AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {   
        //temp sounds i guess?
        shipHitSound = Resources.Load<AudioClip>("beep-tapped");
        shipPowerSound = Resources.Load<AudioClip>("tap-zipper");
        shipDieSound = Resources.Load<AudioClip>("beep-sharpstring");
        shipKillEnemySound = Resources.Load<AudioClip>("tap-crisp");
        shipBounceEnemySound = Resources.Load<AudioClip>("tap-percussive");
        shipCollectPickupSound = Resources.Load<AudioClip>("blubb-charge");
        shipTeleportSound =Resources.Load<AudioClip>("portal-effect");
        shipSpeedBoost = Resources.Load<AudioClip>("speed-up");
        shipCollectCoin = Resources.Load<AudioClip>("coinPickup");
        audioSource = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {

    }

    public static void PlaySound(string clip)
    {
        switch (clip)
        {
            case "shipHitSound":
                audioSource.PlayOneShot(shipHitSound);
                break;
            case "shipPowerSound":
                audioSource.PlayOneShot(shipPowerSound);
                break;
            case "shipDieSound":
                audioSource.PlayOneShot(shipDieSound);
                break;
            case "shipKillEnemySound":
                audioSource.PlayOneShot(shipKillEnemySound);
                break;
            case "shipBounceEnemySound":
                audioSource.PlayOneShot(shipBounceEnemySound);
                break;
            case "shipCollectPickupSound":
                audioSource.PlayOneShot(shipCollectPickupSound);
                break;
            case "shipTeleportSound":
                audioSource.PlayOneShot(shipTeleportSound);
                break;
            case "shipSpeedBoost":
                audioSource.PlayOneShot(shipSpeedBoost);
                break;
            case "shipCollectCoin":
                audioSource.PlayOneShot(shipCollectCoin);
                break;
        }
    }
}

