﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Status : MonoBehaviour
{
    public float max_hp = 5f;
    public float hp = 5f;
    public float hp_regen = 0;
    public float stunTime = 0f;
    public bool hasLifeTime = false;
    public float lifeTime = 0;
    public float invincibilityTime = 0;
    public float damage = 0f;

    private void Update()
    {
        reduceTimers();
        resolveRegens();
    }

    void resolveRegens()
    {
        hp += hp_regen * Time.deltaTime;

        if (hp > max_hp)
        {
            hp = max_hp;
        }
    }

    void reduceTimers()
    {
        if (hasLifeTime)
        {
            lifeTime -= Time.deltaTime;
            if (lifeTime <= 0)
            {
                Destroy(gameObject);
            }
        }

        if (stunTime > 0) { 
            stunTime -= Time.deltaTime;
        }

        if (invincibilityTime > 0)
        {
            invincibilityTime -= Time.deltaTime;
        }
    }
}
