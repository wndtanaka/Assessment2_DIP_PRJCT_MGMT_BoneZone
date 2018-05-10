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
    public static bool isJumping;
    Animator anim;
    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();
        MoveController.onCharacterMove += OnMove;
        MoveController.onCharacterJump += OnJump;
        MoveController.onCharacterIdle += OnIdle;
    }

    // Update is called once per frame
    void Update()
    {
        isJumping = anim.GetBool("isJumping"); 
    }
    
    public IEnumerator OnJump()
    {
        Player.playerState = Player.PLAYER_STATE.JUMPING;
        anim.SetBool("isJumping", true);
        yield return new WaitForSeconds(1);
        anim.SetBool("isJumping", false);
        Player.playerState = Player.PLAYER_STATE.IDLE;
    }

    void OnAnimation()
    {
        //anim.SetBool("isMoving", true);
    }

    public void OnIdle()
    {
        Player.playerState = Player.PLAYER_STATE.IDLE;
        anim.SetBool("isMoving", false);
    }

    public void OnMove()
    {
        Player.playerState = Player.PLAYER_STATE.MOVING;
        anim.SetBool("isMoving", true);
    }

    public void OnAttack()
    {

    }
}
