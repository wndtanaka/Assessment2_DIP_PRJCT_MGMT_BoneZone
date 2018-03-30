using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    public float Horizontal;
    public float Vertical;
    public Vector2 MouseInput;
    public bool Fire1;
    public bool Reload;
    public bool MouseWheelUp;
    public bool MouseWheelDown;

    void Update()
    {
        // storing movement and mouse input on variable
        Vertical = Input.GetAxis("Vertical");
        Horizontal = Input.GetAxis("Horizontal");
        MouseInput = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
        Fire1 = Input.GetButton("Fire1");
        Reload = Input.GetKeyDown(KeyCode.R);
        MouseWheelUp = Input.GetAxis("Mouse ScrollWheel") > 0;
        MouseWheelDown = Input.GetAxis("Mouse ScrollWheel") < 0;
    }
}
