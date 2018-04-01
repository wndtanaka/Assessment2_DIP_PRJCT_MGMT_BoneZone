using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpItem : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Player")
        {
            return;
        }
        PickUp(other.transform);
    }

    void PickUp(Transform item)
    {
        OnPickUp(item);
    }

    public virtual void OnPickUp(Transform item)
    {
        //TODO PickUpItem
        Debug.Log("Pickup: " + name);
    }
}
