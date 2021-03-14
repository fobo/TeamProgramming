using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManagerScript : MonoBehaviour
{
    public static AudioClip shipHitSound, shipPowerSound, shipDieSound, shipKillEnemySound, shipBounceEnemySound;
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
        }
    }
}

