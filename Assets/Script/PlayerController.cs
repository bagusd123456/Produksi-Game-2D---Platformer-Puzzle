using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;

    [Header("Character Status")]
    public bool isGrounded;
    public bool isJumping;
    [SerializeField]
    public int jumpCount;

    [Header("Movement")]
    private Vector2 moveInput;
    public float moveSpeed;

    [Header("Jumping Setup")]
    public Transform feetPos;
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

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        isGrounded = Physics.Raycast(feetPos.position, Vector3.down, checkRadius, groundLayer);
        #region Jumping
        if (isJumping && rb.velocity.y < 0)
        {
            isJumping = false;
        }

        Debug.Log(rb.velocity.y);
        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            isJumping = true;
            jumpTimeCounter = jumpTime;
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
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
                rb.velocity += (fallMultiplier - 1) * Physics.gravity.y * Time.deltaTime * Vector3.up;
            }
        }

        else if (Input.GetKeyUp(KeyCode.Space))
        {
            rb.velocity += (lowJumpMultiplier - 1) * Physics.gravity.y * Time.deltaTime * Vector3.up;
            //rb.AddForce(Vector3.down * rb.velocity.y * (1 - lowJumpMultiplier), ForceMode.Impulse);
        }
        #endregion

        CheckInput();
    }

    void CheckInput()
    {
        // Move Input
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");
        moveInput.Normalize();
        
        Move();
    }

    void CheckAnim()
    {
        // Movement
        //spriteAnim.SetBool("isGrounded", isGrounded);
        //spriteAnim.SetFloat("moveSpeed", rb.velocity.magnitude);
    }

    void Move()
    {
        rb.velocity = new Vector3(moveInput.x * moveSpeed, rb.velocity.y, moveInput.y * moveSpeed);
    }

    void Jump()
    {
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(feetPos.position, checkRadius);
    }

}
