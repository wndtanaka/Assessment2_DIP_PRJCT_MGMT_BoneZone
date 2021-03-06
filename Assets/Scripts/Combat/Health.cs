﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : Destructable
{
    [SerializeField]
    float inSeconds;

    public override void Die()
    {
        base.Die();

        GameManager.Instance.Respawner.Respawn(gameObject, inSeconds);
    }

    void OnEnable()
    {
        Reset();
    }

    public override void TakeDamage(float amount)
    {   
        base.TakeDamage(amount);
        Debug.Log("Remaining Health: " + HitPointsRemaining);
    }
}
