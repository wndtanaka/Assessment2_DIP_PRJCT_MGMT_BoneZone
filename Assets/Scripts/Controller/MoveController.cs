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

    private Player m_Player;
    public Player Player
    {
        get
        {
            if (m_Player == null)
            {
                m_Player = GetComponent<Player>();
            }
            return m_Player;
        }
    }

    public delegate void OnCharacterIdle();
    public event OnCharacterIdle onCharacterIdle;

    public delegate void OnCharacterMove();
    public event OnCharacterMove onCharacterMove;

    public delegate IEnumerator OnCharacterJump();
    public event OnCharacterJump onCharacterJump;

    public void Idle()
    {
        onCharacterIdle();
    }

    public void Move(Vector2 direction)
    {
        if (direction != new Vector2(0, 0))
        {
            onCharacterMove();
        }
        else if (Player.playerState != Player.PLAYER_STATE.JUMPING)
        {
            onCharacterIdle();
        }
    

        // move direction using WASD
        transform.position += transform.forward * direction.x * Time.deltaTime + transform.right * direction.y * Time.deltaTime;
    }

    public void Jump()
    {
        StartCoroutine(onCharacterJump());
    }
}
