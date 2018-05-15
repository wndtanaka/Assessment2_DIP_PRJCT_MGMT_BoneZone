﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public float lookRadius = 20f;
    public Animator enemyAnim;
    public Transform target;
    public AudioSource bulletHitSFX;
    bool isEntered = false;
    NavMeshAgent nav;
    Vector3 direction;

    // Use this for initialization
    void Start()
    {
        nav = GetComponent<NavMeshAgent>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void LateUpdate()
    {
        float distance = Vector3.Distance(target.position, transform.position);

        if (distance <= lookRadius)
        {
            nav.SetDestination(target.position);
            enemyAnim.SetBool("isMoving", true); 

            if (distance <= nav.stoppingDistance + 1)
            {
                FaceTarget();
            }
            lookRadius = 40f;
        }
        else
        {
            lookRadius = 10f;
        }
    }

    void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }

    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Bullet")
        {
            bulletHitSFX.Play();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            enemyAnim.SetBool("isAttacking", true);
            isEntered = true;
            Debug.Log(isEntered);
            StartCoroutine(NotAttack()); 
        }
    }

    IEnumerator NotAttack()
    {
        yield return new WaitForSeconds(0.6f);
        enemyAnim.SetBool("isAttacking", false);
    }
}
