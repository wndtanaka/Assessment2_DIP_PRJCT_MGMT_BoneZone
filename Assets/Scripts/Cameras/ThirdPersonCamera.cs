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

    // Update is called once per frame
    void Update()
    { 
        // finding targetPosition of the camera based on target where the camera looks in x,y,z axis
        Vector3 targetPosition = cameraLookTarget.position + localPlayer.transform.forward * cameraOffset.z + localPlayer.transform.up * cameraOffset.y + localPlayer.transform.right * cameraOffset.x;

        // assign targetRotation, to make the camera always look at the same direction as the localPlayer looking
        Quaternion targetRotation = Quaternion.LookRotation(cameraLookTarget.position - targetPosition, Vector3.up);

        // updating the position of the camera with added damping
        transform.position = Vector3.Lerp(transform.position, targetPosition, damping * Time.deltaTime);
        // updating the rotation of the camera with added damping
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, damping * Time.deltaTime);
    }
}
