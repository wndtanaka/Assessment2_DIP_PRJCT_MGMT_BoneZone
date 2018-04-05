using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    [SerializeField]
    Vector3 cameraOffset;
    [SerializeField]
    float damping;
    [SerializeField]
    float distanceToPlayer;

    public LayerMask layerMask;

    Transform cameraLookTarget;
    Player localPlayer;

    void Awake()
    {
        // Events delegate assign the Player to the localPlayer
        GameManager.Instance.onLocalPlayerJoined += HandleOnLocalPlayerJoined;
    }
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void HandleOnLocalPlayerJoined(Player player)
    {
        localPlayer = player;
        // assign CameraLookTarget gameobject to this cameraLookTargetVariable
        cameraLookTarget = localPlayer.transform.Find("CameraLookTarget");

        if (cameraLookTarget == null)
        {
            cameraLookTarget = localPlayer.transform;
        }
    }

    void LateUpdate()
    {
        // finding targetPosition of the camera based on target where the camera looks in x,y,z axis
        Vector3 targetPosition = cameraLookTarget.position + localPlayer.transform.forward * cameraOffset.z + localPlayer.transform.up * cameraOffset.y + localPlayer.transform.right * cameraOffset.x;

        // assign targetRotation, to make the camera always look at the same direction as the localPlayer looking
        Quaternion targetRotation = Quaternion.LookRotation(cameraLookTarget.position - targetPosition, Vector3.up);

        Vector3 collisionTargetPoint = cameraLookTarget.position + localPlayer.transform.up;

        //store raycast hit
        RaycastHit hit;

        Debug.DrawLine(targetPosition, collisionTargetPoint, Color.blue);
        // if camera detects something between camera and player, then the camera wiill move to the hitpoint
        if (Physics.Linecast(collisionTargetPoint, targetPosition, out hit, ~layerMask))
        {
            Vector3 hitPoint = new Vector3(hit.point.x + hit.normal.x * .2f, hit.point.y, hit.point.z + hit.normal.z * .2f);
            // change targetPosition to the new vector
            targetPosition = new Vector3(hitPoint.x, targetPosition.y, hitPoint.z);
        }

        // updating the position of the camera with added damping
        transform.position = Vector3.Lerp(transform.position, targetPosition, damping * Time.deltaTime);
        // updating the rotation of the camera with added damping
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, damping * Time.deltaTime);
    }

    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.blue;
    //    Gizmos.DrawLine(transform);
    //}
}
