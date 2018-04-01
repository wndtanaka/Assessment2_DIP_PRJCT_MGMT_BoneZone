using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    public float Horizontal;
    public float Vertical;
    public Vector2 MouseInput;
    public bool Fire1;
    public bool MouseWheelUp;
    public bool MouseWheelDown;
    public bool Reload;
    public bool Interact;

    void Update()
    {
        // storing movement and mouse input on variable
        Vertical = Input.GetAxis("Vertical");
        Horizontal = Input.GetAxis("Horizontal");
        MouseInput = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
        Fire1 = Input.GetButton("Fire1");
        MouseWheelUp = Input.GetAxis("Mouse ScrollWheel") > 0;
        MouseWheelDown = Input.GetAxis("Mouse ScrollWheel") < 0;
        Reload = Input.GetKeyDown(KeyCode.R);
        Interact = Input.GetKeyDown(KeyCode.E);
    }
}
