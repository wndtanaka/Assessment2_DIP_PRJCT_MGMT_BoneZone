using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallEditor : MonoBehaviour
{
    public float rayRange = 5f;

    MovableWall movableWall;
    Vector3 offset;

    // Use this for initialization
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        offset = transform.position + new Vector3(0, 3, 0);
        // press Q and E to move the wall to the left and right respectively
        if (Input.GetKeyDown(KeyCode.E))
        {
            CheckWall();
        }
    }
    void CheckWall()
    {
        // checking the wall in rayRange if there is any wall with "MoveableWall" layer attached
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, rayRange))
        {
            if (hit.transform.gameObject.layer == LayerMask.NameToLayer("ShiftableWall"))
            {
                movableWall = hit.transform.GetComponent<MovableWall>();
                Debug.Log("Shifting Wall");
                movableWall.ShiftWall();
            }
            if (hit.transform.gameObject.layer == LayerMask.NameToLayer("HackableWall"))
            {
                movableWall = hit.transform.GetComponent<MovableWall>();
                Debug.Log("Hacking Wall");
                movableWall.HackWall();
            }
        }
    }
}
