using UnityEngine;
using System.Collections;

public class CameraLook : MonoBehaviour
{
    public Camera mainCamera;
    public Transform player;

    public float distance = 7f;//distance between character and camera

    float x = 0f;
    float y = 0f;
    //x and y side speed, how fast your camera moves in x way and in y way
    public float xSpeed = 120f;
    public float ySpeed = 120f;
    //Minium and maximum distance between player and camera
    public float distanceMin = 0f;
    public float distanceMax = 15f;
    //checks if first person mode is on
    private bool click = false;
    //stores cameras distance from player
    private float curDist = 0;

    private void Start()
    {
        //make variable from our euler angles
        Vector3 angles = transform.eulerAngles;

        mainCamera = Camera.main;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void LateUpdate()
    {
        x += Input.GetAxis("Mouse X") * xSpeed * distance * 0.02f;
        y -= Input.GetAxis("Mouse Y") * ySpeed * 0.02f;
        //set rotation
        Quaternion rotation = Quaternion.Euler(y, x, 0);
        //changes distance between max and min distancy by mouse scroll
        //distance = Mathf.Clamp(distance - Input.GetAxis("Mouse ScrollWheel") * 5, distanceMin, distanceMax);
        //negative distance of camera
        Vector3 negDistance = new Vector3(0.0f, 0.0f, -distance);
        //cameras postion
        Vector3 position = rotation * negDistance + player.position;
        //rotation and position of our camera to different variables
        transform.rotation = rotation;
        transform.position = position;
        //cameras x rotation
        float cameraX = transform.rotation.x;
        
        // sets character rotation to follow camera rotation
        player.eulerAngles = new Vector3(cameraX, transform.eulerAngles.y, transform.eulerAngles.z);

        //store raycast hit
        RaycastHit hit;
        //if camera detects something behind or under it move camera to hitpoint so it doesn't go throught wall/floor
        if (Physics.Raycast(player.position, (transform.position - player.position).normalized, out hit, (distance <= 0 ? -distance : distance)))
        {
            transform.position = hit.point + new Vector3(1,0,-1f); ;
        }
    }
}