using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MoveController))]
public class Player : MonoBehaviour
{
    [System.Serializable]
    public class MouseInput
    {
        // storing damping and sensitivity in a MouseInput class
        public Vector2 damping;
        public Vector2 sensitivity;
    }

    [SerializeField]
    float speed;
    [SerializeField]
    MouseInput mouseControl; // class accessor
    
    private MoveController m_MoveController;
    public MoveController MoveController
    {
        // getting moveController script component 
        get
        {
            if (m_MoveController == null)
            {
                m_MoveController = GetComponent<MoveController>();
            }
            return m_MoveController;
        }
    }

    private Crosshair m_Crosshair;
    private Crosshair Crosshair
    {
        get
        {
            if (m_Crosshair == null)
            {
                m_Crosshair = GetComponentInChildren<Crosshair>();
            }
            return m_Crosshair;
        }
    }

    InputController playerInput;
    Vector2 mouseInput;

    void Awake()
    {
        // get playerInput Manager from GameManager
        playerInput = GameManager.Instance.InputController;
        // assign gameobject that attached to this script to the LocalPlayer in the GameManager
        GameManager.Instance.LocalPlayer = this;
    }

    void Update()
    {
        // calculate player direction input and pass them to Move function on MoveController script
        Vector2 direction = new Vector2(playerInput.Vertical * speed, playerInput.Horizontal * speed);
        MoveController.Move(direction);

        // player rotation using mouseInput
        mouseInput.x = Mathf.Lerp(mouseInput.x, playerInput.MouseInput.x, 1f / mouseControl.damping.x);
        mouseInput.y = Mathf.Lerp(mouseInput.y, playerInput.MouseInput.y, 1f / mouseControl.damping.y);

        transform.Rotate(Vector3.up * mouseInput.x * mouseControl.sensitivity.x);

        //Crosshair.LookHeight(mouseInput.y * mouseControl.sensitivity.y);
    }
}
