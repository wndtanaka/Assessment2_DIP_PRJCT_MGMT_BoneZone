using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    public float Horizontal;
    public float Vertical;
    public Vector2 MouseInput;
    public bool Fire1;
    public bool Cut;
    public bool MouseWheelUp;
    public bool MouseWheelDown;
    public bool Reload;
    public bool Interact;
    public bool Jump;
    public bool Pause;
    public bool Resume;
    public bool QuitGame;

    void Update()
    {
        // storing movement and mouse input on variable
        Vertical = Input.GetAxisRaw("Vertical");
        Horizontal = Input.GetAxisRaw("Horizontal");
        MouseInput = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
        Fire1 = Input.GetButton("Fire1");
        Cut = Input.GetKey(KeyCode.C);
        MouseWheelUp = Input.GetAxis("Mouse ScrollWheel") > 0;
        MouseWheelDown = Input.GetAxis("Mouse ScrollWheel") < 0;
        Reload = Input.GetKeyDown(KeyCode.R);
        Interact = Input.GetKeyUp(KeyCode.E);
        Jump = Input.GetButtonDown("Jump");
        Pause = Input.GetKeyDown(KeyCode.P);
        Resume = Input.GetKeyDown(KeyCode.Q);
        QuitGame = Input.GetKeyDown(KeyCode.Escape);
    }
}
