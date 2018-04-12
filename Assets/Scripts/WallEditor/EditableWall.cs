using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditableWall : MonoBehaviour
{
    public Walls walls;
    [SerializeField]
    GameObject wall;

    Vector3 shiftedPosition;
    Vector3 originalPosition;
    Vector3 shiftedOffset = new Vector3(0, 0, 5);
    bool isShifted = false;
    bool isDrew = false;
    Collider boxCollider;

    void Start()
    {
        // storing origianl position of the wall for shifting purposes
        originalPosition = transform.position;
        boxCollider = GetComponent<BoxCollider>();
    }
    public void ShiftWall()
    {
        // toggling shifting wall to the offset position and original position
        if (!isShifted)
        {
            shiftedPosition = transform.position + shiftedOffset;
            transform.position = shiftedPosition;
            Debug.Log("Shifting Wall");
            isShifted = true;
        }
        else
        {
            transform.position = originalPosition;
            Debug.Log("Shifting Wall");
            isShifted = false;
        }
    }
    public void HackWall()
    {
        Debug.Log("Hacking Wall");
        boxCollider.isTrigger = true;
    }
    public void DrawWall(RaycastHit hit)
    {
        Destroy(gameObject);
        Instantiate(wall, hit.transform.position, hit.transform.rotation);
        Debug.Log("Placed a Wall");
    }
    public void EraseWall(RaycastHit hit)
    {
        Destroy(gameObject);
        Instantiate(wall, hit.transform.position, hit.transform.rotation);
        Debug.Log("Erased a Wall");
    }
}
public enum Walls
{
    DRAWABLE,
    ERASEABLE,
    HACKABLE,
    SHIFTABLE
}
