using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveController : MonoBehaviour
{
    private AnimationController m_AnimationController;
    public AnimationController AnimationController
    {
        get
        {
            if (m_AnimationController == null)
            {
                m_AnimationController = GetComponent<AnimationController>();
            }
            return m_AnimationController;
        }
    }

    public delegate void OnCharacterMove();
    public event OnCharacterMove onCharacterMove;

    public void Move(Vector2 direction)
    {
        if (direction != new Vector2(0, 0))
        {
            onCharacterMove();
        }
        else
        {
            AnimationController.OnIdle();
        }

        // move direction using WASD
        transform.position += transform.forward * direction.x * Time.deltaTime + transform.right * direction.y * Time.deltaTime;
    }
}
