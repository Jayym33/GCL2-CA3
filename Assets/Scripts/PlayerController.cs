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

    Rigidbody2D rb;

    //for animation//
    /*private Animator anim;*/
    private bool isFacingRight;

    private Collider2D _collider;
    private Vector2 _respwanPT;

    private Vector2 _initialSpawnPoint;

    //public bool hasKey = false;

    /*[Header("Sound Effects")]
    public AudioSource jumpSFX;
    public AudioSource collectSFX;
    */

    //additional stuff//
    //public PlayerHealth health;

    void Awake()
    {
        //health = GetComponent<PlayerHealth>();
        //anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

        _collider = GetComponent<Collider2D>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        isFacingRight = true;

        //health = GetComponent<PlayerHealth>();

        _initialSpawnPoint = transform.position; // TRUE starting point
        _respwanPT = _initialSpawnPoint;          // First respawn is start
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

        isGrounded = Physics2D.OverlapCircle(
            point: groundCheck.position, 
            radius: groundCheckRadius,
            layerMask: realGround);

        //Check if player is jumping
        //Legacy Input System
        bool ifJumping = Input.GetButtonDown("Jump");

        //new input system
        //bool ifJumping = InputSystem.actions.FindAction("Jump").IsPressed;

        if(ifJumping && isGrounded)
        {
            rb.linearVelocityY = jumpSpeed;
            /*if (!jumpSFX.isPlaying)
            {
                jumpSFX.Play();
            }*/
           
        }

        if (horizontalMovement != 0)
        {
            //anim.SetBool("isMoving", true);
        }
        else
        {
            //anim.SetBool("isMoving", false);
        }

        //anim.SetBool("isJumping", !isGrounded);

        /*if (health.DamageTaken)
        {
            anim.SetTrigger("Hurt");
            health.DamageTaken = false;
        }*/

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
        rb.linearVelocityY = jumpSpeed/2;
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

}
