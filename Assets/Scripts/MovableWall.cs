using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovableWall : MonoBehaviour
{
    Vector3 shiftedPosition;
    Vector3 originalPosition;
    bool isMoved = false;
    // Use this for initialization
    void Start()
    {
        originalPosition = transform.position;
    }

    public void ShiftWall()
    {

        if (!isMoved)
        {
            shiftedPosition = transform.position + new Vector3(0, 0, 5);
            transform.position = shiftedPosition;
            isMoved = true;
        }
        else
        {
            transform.position = originalPosition;
            isMoved = false;
        }

    }
    public void HackWall()
    {
        Destroy(gameObject);
    }
}
