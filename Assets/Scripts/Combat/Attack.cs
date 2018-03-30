using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public Transform spawnPoint;
    public GameObject projectile;
    public float attackRange = 100f;
    public float force = 100f;
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
        GameObject projectileGO = Instantiate(projectile, spawnPoint.position, transform.rotation);
        projectileGO.GetComponent<Rigidbody>().AddForce(transform.forward * force, ForceMode.Impulse);
        Destroy(projectileGO, 2f);
        Debug.Log("PEW");
    }
}
