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
        // get Destructable component on triggered object
        Destructable destructable = other.transform.GetComponent<Destructable>();
        if (destructable == null)
        {
            return;
        }
        destructable.TakeDamage(damage);
    }

    void OnCollisionEnter(Collision col)
    {
        Destroy(gameObject, 0.05f);
    }
}
