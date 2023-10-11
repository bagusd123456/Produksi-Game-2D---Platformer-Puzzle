using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region Sound
    private SoundManager soundManager;
    private MenuManager menuManager;
    public AudioSource audioSource;
    public AudioSource audioSource2;
    #endregion
    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer sprite;
    #region Slope
    private float slopeDownAngle;
    private float slopeSideAngle;
    private float lastSlopeAngle;
    private bool isOnSlope;
    private bool canWalkOnSlope;
    private CapsuleCollider2D cc;

    private Vector2 newVelocity;
    private Vector2 newForce;
    private Vector2 capsuleColliderSize;
    private Vector2 slopeNormalPerp;
    #endregion

    [Header("Character Status")]
    public bool isGrounded;
    public bool isJumping;
    public bool canJump;
    [SerializeField]
    public int jumpCount;
    [HideInInspector]public int velocitySpeedX;

    [Header("Movement")]
    private Vector2 moveInput;
    public float moveSpeed;

    [Header("Jumping Setup")]
    public Transform feetPos;
    public Transform feetRay;
    public float checkRadius;
    public LayerMask groundLayer;

    [Header("Jumping Value")]
    [SerializeField]
    public float jumpForce = 2.5f;
    [SerializeField]
    private float jumpTimeCounter = 0.24f;
    [SerializeField]
    public float jumpTime = 0.4f;
    [SerializeField]
    public float jumpMultiplier = 1.8f;
    [SerializeField]
    public float fallMultiplier = 2.5f;
    [SerializeField]
    public float lowJumpMultiplier = 1f;

    [Header("Slope Parameter")]
    
    [SerializeField]
    private float slopeCheckDistance;
    [SerializeField]
    private float maxSlopeAngle;
    [SerializeField]
    public PhysicsMaterial2D noFriction;
    [SerializeField]
    public PhysicsMaterial2D fullFriction;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        cc = GetComponent<CapsuleCollider2D>();
        audioSource = GetComponent<AudioSource>();
        soundManager = GameObject.FindGameObjectWithTag("Manager").GetComponent<SoundManager>();
        menuManager = GameObject.FindGameObjectWithTag("Manager").GetComponent<MenuManager>();
    }

    void Update()
    {
        isGrounded = Physics2D.Raycast(feetPos.position, Vector3.down, checkRadius, groundLayer);
        #region Checker
        if(anim != null)
        {
            anim.SetInteger("Velocity", velocitySpeedX);
        }
        
        #endregion
        #region Jumping
        if (isJumping && rb.velocity.y <= 0)
        {
            isJumping = false;
        }

        //Debug.Log(rb.velocity.y);
        if ((isGrounded || canJump) && Input.GetKeyDown(KeyCode.Space))
        {
            isJumping = true;
            canJump = false;
            jumpTimeCounter = jumpTime;
            rb.AddForce(Vector3.up * jumpForce, ForceMode2D.Impulse);

            //Play Sound
            if (soundManager.jumping != null && !audioSource2.isPlaying)
            {
                audioSource2.PlayOneShot(soundManager.jumping, 0.8f);
            }
        }
        if (Input.GetKey(KeyCode.Space) && isJumping == true)
        {
            if (jumpTimeCounter > 0)
            {
                rb.AddForce(jumpForce * jumpMultiplier * Vector3.up);
                jumpTimeCounter -= Time.deltaTime;
            }
            else if (jumpTimeCounter > 0 && rb.velocity.y < 0)
            {
                rb.velocity += (fallMultiplier - 1) * Physics.gravity.y * Time.deltaTime * Vector2.up;
            }
        }

        else if (Input.GetKeyUp(KeyCode.Space))
        {
            rb.velocity += (lowJumpMultiplier - 1) * Physics.gravity.y * Time.deltaTime * Vector2.up;
            //rb.AddForce(Vector3.down * rb.velocity.y * (1 - lowJumpMultiplier), ForceMode.Impulse);
        }
        #endregion

        CheckInput();
        CheckAnim();

        if(moveInput.x != 0 && isGrounded)
        {
            //Play Sound
            if (soundManager.walking != null && !audioSource.isPlaying && menuManager.isPaused == false)
            {
                audioSource.PlayOneShot(soundManager.walking, 0.6f);
            }
        }
        else
        {
            if (soundManager.walking != null && audioSource.isPlaying)
            {
                audioSource.Stop();
                /*Mathf.Lerp(audioSource.volume, 0, Time.deltaTime);
                if(audioSource.volume == 0)
                {
                    audioSource.volume = 1f;
                }*/
            }
        }


        //Debug.Log("Character Speed: " + velocitySpeedX);
    }

    private void FixedUpdate()
    {
        SlopeCheck();
    }

    void SlopeCheck()
    {
        Vector2 checkPos = transform.position - (Vector3)(new Vector2(0f, cc.size.y / 2f));

        if (isGrounded && !isJumping && slopeDownAngle <= maxSlopeAngle)
        {
            canJump = true;
        }

        SlopeCheckHorizontal(checkPos);
        SlopeCheckVertical(checkPos);
    }

    private void SlopeCheckHorizontal(Vector2 checkPos)
    {
        RaycastHit2D slopeHitFront = Physics2D.Raycast(checkPos, transform.right, slopeCheckDistance, groundLayer);
        RaycastHit2D slopeHitBack = Physics2D.Raycast(checkPos, -transform.right, slopeCheckDistance, groundLayer);

        if (slopeHitFront)
        {
            isOnSlope = true;

            slopeSideAngle = Vector2.Angle(slopeHitFront.normal, Vector2.up);

        }
        else if (slopeHitBack)
        {
            isOnSlope = true;

            slopeSideAngle = Vector2.Angle(slopeHitBack.normal, Vector2.up);
        }
        else
        {
            slopeSideAngle = 0.0f;
            isOnSlope = false;
        }

    }

    private void SlopeCheckVertical(Vector2 checkPos)
    {
        RaycastHit2D hit = Physics2D.Raycast(checkPos, Vector2.down, slopeCheckDistance, groundLayer);

        if (hit)
        {

            slopeNormalPerp = Vector2.Perpendicular(hit.normal).normalized;

            slopeDownAngle = Vector2.Angle(hit.normal, Vector2.up);

            if (slopeDownAngle != lastSlopeAngle)
            {
                isOnSlope = true;
            }

            lastSlopeAngle = slopeDownAngle;

            Debug.DrawRay(hit.point, slopeNormalPerp, Color.blue);
            Debug.DrawRay(hit.point, hit.normal, Color.green);

        }

        if (slopeDownAngle > maxSlopeAngle || slopeSideAngle > maxSlopeAngle)
        {
            canWalkOnSlope = false;
        }
        else
        {
            canWalkOnSlope = true;
        }

        if (isOnSlope && canWalkOnSlope && moveInput.x == 0.0f)
        {
            rb.sharedMaterial = fullFriction;
        }
        else
        {
            rb.sharedMaterial = noFriction;
        }
    }

    void CheckInput()
    {
        // Move Input
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");
        //moveInput.Normalize();
        
        Move();
    }

    void CheckAnim()
    {
        velocitySpeedX = (int)rb.velocity.normalized.x;
        if(moveInput.x > 0)
        {
            sprite.flipX = true;
        }
        else if (moveInput.x < 0)
        {
            sprite.flipX = false;
        }
        // Movement
        //spriteAnim.SetBool("isGrounded", isGrounded);
        //spriteAnim.SetFloat("moveSpeed", rb.velocity.magnitude);
    }

    void Move()
    {
        /*rb.velocity = new Vector3(moveInput.x * moveSpeed, 
            rb.velocity.y, moveInput.y * moveSpeed);*/
        if (isGrounded && !isOnSlope && !isJumping) //if not on slope
        {
            //Debug.Log("This one");
            newVelocity.Set(moveSpeed * moveInput.x, 0.0f);
            rb.velocity = newVelocity;

            
        }
        else if (isGrounded && isOnSlope && canWalkOnSlope && !isJumping) //If on slope
        {
            newVelocity.Set(moveSpeed * slopeNormalPerp.x * -moveInput.x, moveSpeed * slopeNormalPerp.y * -moveInput.x);
            rb.velocity = newVelocity;
        }
        else if (!isGrounded) //If in air
        {
            newVelocity.Set(moveSpeed * moveInput.x, rb.velocity.y);
            rb.velocity = newVelocity;
        }
    }

    void Jump()
    {
        
    }

    public IEnumerator FadeOut(AudioSource audioSource, float FadeTime)
    {
        float startVolume = audioSource.volume;

        while (audioSource.volume > 0)
        {
            audioSource.volume -= startVolume * Time.deltaTime / FadeTime;

            yield return null;
        }

        audioSource.Stop();
        if (!audioSource.isPlaying)
        {
            audioSource.volume = 1f;
        }
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(feetPos.position, checkRadius);
    }

}
