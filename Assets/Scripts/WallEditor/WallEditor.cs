using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallEditor : MonoBehaviour
{
    public float rayRange = 5f;

    EditableWall editableWall;
    Vector3 offset;

    void Start()
    {

    }

    void Update()
    {
        offset = transform.position + new Vector3(0, 1, 0);
        // press Q and E to move the wall to the left and right respectively
        if (GameManager.Instance.InputController.Interact)
        {
            CheckWall();
        }
    }

    void CheckWall()
    {
        // checking the wall in rayRange if there is any wall with "MoveableWall" layer attached
        RaycastHit hit;
        if (Physics.Raycast(offset, transform.forward, out hit, rayRange))
        {
            // getting editableWall component
            editableWall = hit.transform.GetComponent<EditableWall>();

            // switching between enums
            switch (editableWall.walls)
            {
                case Walls.DRAWABLE:
                    editableWall.DrawWall(hit);
                    break;
                case Walls.ERASEABLE:
                    editableWall.EraseWall(hit);
                    break;
                case Walls.HACKABLE:
                    editableWall.HackWall();
                    break;
                case Walls.SHIFTABLE:
                    editableWall.ShiftWall();
                    break;
                default:
                    break;
            }
        }
    }
}
