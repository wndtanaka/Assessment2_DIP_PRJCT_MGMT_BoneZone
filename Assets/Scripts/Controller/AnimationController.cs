using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
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

    Animator anim;
    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();
        MoveController.onCharacterMove += OnAnimation;
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnAnimation()
    {
        anim.SetBool("isMoving", true);
    }

    public void OnIdle()
    {
        anim.SetBool("isMoving", false);
    }

    public void OnMove()
    {
        //anim.SetBool("isMoving", true);
    }

    public void OnJump()
    {

    }

    public void OnAttack()
    {
    
    }
}
