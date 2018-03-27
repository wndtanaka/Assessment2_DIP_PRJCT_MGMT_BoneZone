using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonCamera : MonoBehaviour
{
    public Transform player, cameraAnchor, mainCam;
    public float distance = 8;
    public float damping = 1;

    private float rotX, rotY;

    public Vector3 offset;
    RaycastHit hit;
    public LayerMask layerMask;

    void LateUpdate()
    {
        mainCam.transform.localPosition = offset;

        if (Input.GetMouseButton(1))
        {
            rotX += Input.GetAxis("Mouse X") * 10;
            rotY -= Input.GetAxis("Mouse Y") * 10;

        }

        rotY = Mathf.Clamp(rotY, -60f, 60f);
        mainCam.LookAt(cameraAnchor);
        cameraAnchor.localRotation = Quaternion.Euler(rotY, rotX, 0);

        cameraAnchor.position = new Vector3(player.position.x, player.position.y, player.position.z);

        if (Physics.Raycast(player.position, (transform.position - player.position).normalized, out hit, (distance <= 0 ? -distance : distance), ~layerMask)) // checking if there is any gameobject between player and camera tha are not in layerMask
        {
            transform.position = hit.point;
        }
    }
}
