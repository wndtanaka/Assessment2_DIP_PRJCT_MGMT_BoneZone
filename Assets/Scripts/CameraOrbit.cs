using UnityEngine;
using System.Collections;

public class CameraOrbit : MonoBehaviour
{
    public Transform player;
    //distance between character and camera
    public float distance;

    private void Start()
    {
        // getting distance between camera and player
        distance = Vector3.Distance(transform.position, player.position);
    }

    private void LateUpdate()
    {
        RaycastHit hit;
        // if camera detects something behind or under it move camera to hitpoint so it doesn't go throught wall/floor
        if (Physics.Raycast(player.position, (transform.position - player.position).normalized, out hit, (distance <= 0 ? -distance : distance)))
        {
            transform.position = hit.point;
        }
    }
}