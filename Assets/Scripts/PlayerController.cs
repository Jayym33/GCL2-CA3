using System.Collections.Generic;
using System.Collections;
using UnityEngine.InputSystem;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    public float jumpSpeed;
    public bool _active = true;

    [Header("Ground Detection")]
    public Transform groundCheck;
    public float groundCheckRadius;
    public LayerMask realGround;
    public bool isGrounded;

    [Header("Ladder")]
    public float climbSpeed = 4f;

    private bool isOnLadder = false;
    private bool isClimbing = false;

    Rigidbody2D rb;

    //for animation//
    private Animator anim;
    public bool hasHammer = false;

    [Header("Hammer")]
    public GameObject hammerHitBox;
    public float hammerDuration = 12f;   // How long the hammer lasts
    private float hammerTimer = 0f;

    private bool isFacingRight;

    //==================== SHIELD ====================//

    [Header("Shield")]

    // Keeps track of whether Mario currently has a shield
    public bool hasShield = false;

    // How long the shield lasts
    public float shieldDuration = 5f;

    // Counts down until the shield expires
    private float shieldTimer = 0f;

    // Shield object that surrounds Mario
    public GameObject shieldVisual;

    private Collider2D _collider;
    private Vector2 _respwanPT;

    private Vector2 _initialSpawnPoint;

    //public bool hasKey = false;

    /*[Header("Sound Effects")]
    public AudioSource jumpSFX;
    public AudioSource collectSFX;
    */

    //additional stuff//
    public PlayerHealth health;

    void Awake()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

        _collider = GetComponent<Collider2D>();

        health = GetComponent<PlayerHealth>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        isFacingRight = true;

        //health = GetComponent<PlayerHealth>();

        _initialSpawnPoint = transform.position; // TRUE starting point
        _respwanPT = _initialSpawnPoint;          // First respawn is start

        // Hammer hitbox starts disabled
        if (hammerHitBox != null)
        {
            hammerHitBox.SetActive(false);
        }

        // Shield visual starts disabled
        if (shieldVisual != null)
        {
            shieldVisual.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!_active)
        {
            return;
        }

        //Old/ legacy Input System:
        //float horizontalMovement = Input.GetAxis("Horizontal");

        //New Input System:
        float horizontalMovement = InputSystem.actions.FindAction("Move").ReadValue<Vector2>().x;
        float verticalMovement = InputSystem.actions.FindAction("Move").ReadValue<Vector2>().y;

        //Move at moveSpeed in the appropriate direction
        if (horizontalMovement > 0)
        {
            rb.linearVelocityX = moveSpeed;
        }
        else if (horizontalMovement < 0)
        {
            rb.linearVelocityX = -moveSpeed;
        }
        else
        {
            rb.linearVelocityX = 0;
        }

        // Ladder climbing
        // Mario cannot climb while holding the hammer
        if (hasHammer)
        {
            isClimbing = false;
            rb.gravityScale = 1;
        }
        else
        {
            isClimbing = isOnLadder;

            if (isClimbing)
            {
                rb.gravityScale = 0;
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, verticalMovement * climbSpeed);
            }
            else
            {
                rb.gravityScale = 1;
            }
        }
        anim.SetBool("IsClimbing", isClimbing);
        anim.SetFloat("ClimbSpeed", Mathf.Abs(verticalMovement));

        isGrounded = Physics2D.OverlapCircle(
            point: groundCheck.position,
            radius: groundCheckRadius,
            layerMask: realGround);

        //Check if player is jumping
        //Legacy Input System
        bool ifJumping = Input.GetButtonDown("Jump");

        //new input system
        //bool ifJumping = InputSystem.actions.FindAction("Mario_Jump").IsPressed;

        if (ifJumping && isGrounded && !isClimbing)
        {
            rb.linearVelocityY = jumpSpeed;

            /*if (!jumpSFX.isPlaying)
            {
                jumpSFX.Play();
            }*/

            //anim.SetBool("IsJumping", !isGrounded);
        }

        if (horizontalMovement != 0)
        {
            anim.SetBool("IsWalking", true);
        }
        else
        {
            anim.SetBool("IsWalking", false);
        }

        anim.SetBool("IsJumping", !isGrounded);
        anim.SetBool("HasHammer", hasHammer);

        /*if (health.DamageTaken)
        {
            anim.SetTrigger("Hurt");
            health.DamageTaken = false;
        }*/

        //==================== HAMMER TIMER ====================//

        if (hasHammer)
        {
            hammerTimer -= Time.deltaTime;

            if (hammerTimer <= 0)
            {
                hasHammer = false;

                if (hammerHitBox != null)
                {
                    hammerHitBox.SetActive(false);
                }
            }
        }

        //==================== SHIELD TIMER ====================//

        if (hasShield)
        {
            // Count down every frame
            shieldTimer -= Time.deltaTime;

            // Shield expires after 5 seconds
            if (shieldTimer <= 0)
            {
                hasShield = false;

                // Hide shield visual
                if (shieldVisual != null)
                {
                    shieldVisual.SetActive(false);
                }

                Debug.Log("Shield expired!");
            }
        }

        if (!isFacingRight && horizontalMovement > 0)
        {
            Flip();
        }
        else if (isFacingRight && horizontalMovement < 0)
        {
            Flip();
        }
    }

    public void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 localScale = transform.localScale;
        localScale.x *= -1f;
        transform.localScale = localScale;
    }

    public void MiniJump()
    {
        rb.linearVelocityY = jumpSpeed / 2;
    }

    public void EnterLadder()
    {
        isOnLadder = true;
    }

    public void ExitLadder()
    {
        isOnLadder = false;
        isClimbing = false;

        rb.gravityScale = 1;

        // Stop any upward movement
        if (rb.linearVelocity.y > 0)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, rb.linearVelocity.y * 0.2f);
        }
    }

    public void SetRespwanPoint(Vector2 position)
    {
        _respwanPT = (Vector2)position;
    }

    public void RespawnToSP()
    {
        if (!_active) return;

        _active = false;
        _collider.enabled = false;

        //  FALL DAMAGE = ONLY 1 DAMAGE
        //health.TakeDamage(1);

        StartCoroutine(FallRespawnRoutine());
    }

    private IEnumerator FallRespawnRoutine()
    {
        yield return new WaitForSeconds(1.2f);

        transform.position = _respwanPT;

        _active = true;
        _collider.enabled = true;
    }

    public void ResetSpawnToInitial()
    {
        _respwanPT = _initialSpawnPoint;
    }

    public void Die()
    {
        if (!_active) return; // prevents soft-lock double death

        _active = false;
        _collider.enabled = false;

        MiniJump();

        SceneManager.LoadScene("Lose");
    }

    //==================== HAMMER ====================//

    // Starts the hammer timer when Mario picks up a hammer
    public void StartHammerTimer()
    {
        hasHammer = true;
        hammerTimer = hammerDuration;

        if (hammerHitBox != null)
        {
            hammerHitBox.SetActive(true);
        }
    }

    //==================== SHIELD ====================//

    // Called when Mario picks up a shield
    public void StartShieldTimer()
    {
        // Give Mario the shield
        hasShield = true;

        // Reset timer
        shieldTimer = shieldDuration;

        // Show shield around Mario
        if (shieldVisual != null)
        {
            shieldVisual.SetActive(true);
        }

        Debug.Log("Shield activated!");
    }

    // Removes the shield after blocking one barrel
    public void RemoveShield()
    {
        hasShield = false;

        shieldTimer = 0f;

        if (shieldVisual != null)
        {
            shieldVisual.SetActive(false);
        }

        Debug.Log("Shield broke!");
    }
}