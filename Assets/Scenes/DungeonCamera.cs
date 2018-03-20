using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonCamera : MonoBehaviour
{
    public Transform player;
    public float distance =8;
    public float damping = 1;

    public Vector3 offset;
    RaycastHit hit;

    // Use this for initialization
    void Start()
    {
        offset = transform.position - player.transform.position;
    }

    void LateUpdate()
    {
        Quaternion rotation = Quaternion.LookRotation(player.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation,rotation,Time.deltaTime * damping);

        Vector3 desiredPosition = player.transform.position + offset;
        Vector3 position = Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime * damping);
        transform.position = position;

        transform.LookAt(player.transform.position);

        if (Physics.Raycast(player.position, (transform.position - player.position).normalized, out hit, (distance <= 0 ? -distance : distance)))
        {
            transform.position = hit.point + new Vector3(0, 0, 0f); ;
        }
    }
}
