using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructable : MonoBehaviour
{
    [SerializeField]
    float health;

    float damageTaken;

    public float HitPointsRemaining
    {
        get
        {
            return health - damageTaken; // returning a float of remaining health after substract by damageTaken
        }
    }

    public bool IsAlive
    {
        get
        {
            return HitPointsRemaining > 0; // returns true if HitPointsRemaining over 0
        }
    }

    public delegate void OnDeath();
    public event OnDeath onDeath;

    public delegate void OnDamageReceived();
    public event OnDamageReceived onDamageReceived;

    //public event System.Action OnDeath;
    //public event System.Action OnDamageReceived;

    public virtual void Die()
    {
        if (!IsAlive)
        {
            return;
        }
        if (onDeath != null)
        {
            onDeath();
        }
    }
    public virtual void TakeDamage(float amount)
    {
        damageTaken += amount;
        if (onDamageReceived != null)
        {
            onDamageReceived();
        }
        if (HitPointsRemaining <= 0)
        {
            Die();
        }
    }
    public void Reset()
    {
        damageTaken = 0;
    }
}
