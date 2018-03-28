using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public Transform spawnPoint;
    public GameObject projectile;
    public float attackRange = 100f;
    // Use this for initialization
    void Start()
    {

    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Attacking();
        }
    }
    void Attacking()
    {
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, attackRange))
        {
            GameObject projectileGO = Instantiate(projectile, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(projectileGO, 1f);
            Debug.Log("PEW");
        }
    }
}
