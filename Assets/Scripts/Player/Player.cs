using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(MoveController))]
[RequireComponent(typeof(AnimationController))]
public class Player : MonoBehaviour
{
    public enum PLAYER_STATE
    {
        IDLE,
        MOVING,
        JUMPING,
        ATTACKING,
        DEAD
    }
    public PLAYER_STATE playerState;

    [System.Serializable]
    public class MouseInput
    {
        // storing damping and sensitivity in a MouseInput class
        public Vector2 damping;
        public Vector2 sensitivity;
    }

    [SerializeField]
    float currentHealth;
    [SerializeField]
    float maxHealth = 100;
    [SerializeField]
    float speed;
    [SerializeField]
    MouseInput mouseControl;
    [SerializeField]
    float minimumMoveTreshold;
    [SerializeField]
    AudioController footSteps;

    public PlayerAim playerAim;
    public GameObject winningMenu, aimingPivot;
    public GameObject winningPoint;
    public GameObject gameOverMenu;
    public AudioSource grunt;
    public int damage = 10;  // will draw damage from enemy script later
    public Image healthBar;

    bool canPlayGrunt = true;
    bool isJumping = false;

    InputController playerInput;
    Vector2 mouseInput;
    Vector3 previousPosition;

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

    private PlayerShoot m_PlayerShoot;
    public PlayerShoot PlayerShoot
    {
        get
        {
            if (m_PlayerShoot == null)
            {
                m_PlayerShoot = GetComponent<PlayerShoot>();
            }
            return m_PlayerShoot;
        }
    }

    void Awake()
    {

    }

    void Start()
    {
        // assign gameobject that attached to this script to the LocalPlayer in the GameManager
        GameManager.Instance.LocalPlayer = this;
        // get playerInput Manager from GameManager
        playerInput = GameManager.Instance.InputController;
        currentHealth = maxHealth;
    }

    void Update()
    {
        Jump();
        Move();
        LookAround();
        Cut();
        if (Input.GetKeyDown(KeyCode.L))
        {
            TakeDamage(20);
        }
    }

    void Move()
    {
        // calculate player direction input and pass them to Move function on MoveController script
        Vector2 direction = new Vector2(playerInput.Vertical * speed, playerInput.Horizontal * speed);
        MoveController.Move(direction);

        if (Vector3.Distance(transform.position, previousPosition) > minimumMoveTreshold && playerState != PLAYER_STATE.JUMPING)
        {
            footSteps.Play();
        }
        previousPosition = transform.position;
    }

    void LookAround()
    {
        // player rotation using mouseInput
        mouseInput.x = Mathf.Lerp(mouseInput.x, playerInput.MouseInput.x, 1f / mouseControl.damping.x);
        mouseInput.y = Mathf.Lerp(mouseInput.y, playerInput.MouseInput.y, 1f / mouseControl.damping.y);

        transform.Rotate(Vector3.up * mouseInput.x * mouseControl.sensitivity.x);

        Crosshair.LookHeight(mouseInput.y * mouseControl.sensitivity.y);

        playerAim.SetRotation(mouseInput.y * mouseControl.sensitivity.y);
    }

    void Jump()
    {
        if (playerInput.Jump)
        {
            isJumping = true;
            MoveController.Jump();
        }
    }

    void Cut()
    {
        Animator anim = GetComponent<Animator>();
        if (playerInput.Cut)
        {
            anim.SetBool("isAttacking", true);
            Debug.Log(canPlayGrunt);
            if (canPlayGrunt)
            {
                StartCoroutine(CanPlayAgain());
            }
        }
        else
        {
            anim.SetBool("isAttacking", false);
        }
    }

    IEnumerator CanPlayAgain()
    {
        grunt.Play();
        canPlayGrunt = false;
        yield return new WaitForSeconds(0.6f);
        canPlayGrunt = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject == winningPoint)
        {
            winningMenu.SetActive(true);
            aimingPivot.SetActive(false);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            Time.timeScale = 0;
        }

        if (collision.gameObject.tag == "Enemy")
        {
            TakeDamage(damage);
        }
    }
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        healthBar.fillAmount = currentHealth / maxHealth;
        if (currentHealth <= 0)
        {
            gameOverMenu.SetActive(true);
            Time.timeScale = 0;
        }
    }
}
