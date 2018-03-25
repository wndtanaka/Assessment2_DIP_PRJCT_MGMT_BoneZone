using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallEditor : MonoBehaviour
{
    public float rayRange = 5f;

    Vector3 rayOffset = new Vector3(0, 3, 0);

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.transform.position,Camera.main.transform.forward, out hit,rayRange))
        {
            CheckWall(hit);
        }
    }
    void CheckWall(RaycastHit hit)
    {
        if (hit.transform.gameObject.layer == LayerMask.NameToLayer("MoveableWall"))
        {
            Debug.Log("Wall Detected");
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position + rayOffset, transform.position + rayOffset + Vector3.forward * rayRange);
    }
}
