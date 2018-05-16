using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melee : MonoBehaviour
{
    [SerializeField]
    float damage = 50f;
    List<GameObject> nearEnemies = new List<GameObject>();

    private void Update()
    {
        if (GameManager.Instance.InputController.Cut)
        {
            Attack();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            nearEnemies.Add(other.gameObject);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            nearEnemies.Remove(other.gameObject);
        }
    }

    void Attack()
    {
        Vector3 pos = transform.position;
        for (int i = 0; i < nearEnemies.Count; i++)
        {
            if (nearEnemies[i] == null)
            {
                return;
            }
            Vector3 vec = nearEnemies[i].transform.position;
            Vector3 direction = vec - pos;

            if (Vector3.Dot(direction, transform.forward) < 0.7)
            {
                nearEnemies[i].GetComponent<EnemyAI>().TakeDamage(damage);
            }
        }
    }
}


