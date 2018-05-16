using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Projectile : MonoBehaviour
{
    [SerializeField]
    float speed;
    [SerializeField]
    float timeToLive;
    [SerializeField]
    float damage;
    

    void Start()
    {
        // this will destroy the projectile for timeToLive seconds, after it instantiated
        Destroy(gameObject, timeToLive);   
    }

    void Update()
    {
        // move the gameObject forward, can change it to rigidbody.AddForce() if like
        transform.Translate(Vector3.forward * speed * Time.deltaTime);    
    }

    void OnTriggerEnter(Collider other)
    {
        // get EnemyAI component on triggered object
        EnemyAI hit = other.transform.GetComponent<EnemyAI>();
        if (hit == null)
        {
            return;
        }
        hit.TakeDamage(damage);
    }

    void OnCollisionEnter(Collision col)
    {
        Destroy(gameObject);
    }
}
