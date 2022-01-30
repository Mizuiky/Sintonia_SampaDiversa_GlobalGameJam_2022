using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region Private Fields

    private float horizontalInput;

    [SerializeField]
    private float moveSpeed;
    [SerializeField]
    private float jumpForce;

    [SerializeField]
    private Transform groundCheck;
    
    private LayerMask groundLayer;
    private Vector3 lastCheckpoint;
    private Rigidbody2D playerRigidbody;
    public float deathAnimationDuration = 1f;
    public SpriteRenderer playerSprite;
    private bool isDead = false;
    private bool canJump = false;

    private Animator animator;

    #endregion

    #region Properties

    #endregion

    void Start()
    {
        this.playerRigidbody = this.GetComponent<Rigidbody2D>();
        animator = this.GetComponentInChildren<Animator>();

        this.groundLayer = LayerMask.GetMask("Ground");
        lastCheckpoint = gameObject.transform.position; 
    }

    // Update is called once per frame
    void Update()
    {
        this.horizontalInput = Input.GetAxis("Horizontal");
        if (Input.GetKeyDown(KeyCode.UpArrow) && IsGrounded())
        {
            canJump = true;
        }

    }

    private void FixedUpdate()
    {
        if (!isDead)
        {
            this.Move();

            if (canJump)
            {
                Jump();
                canJump = false;
            }

        }

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(this.groundCheck.position, 0.25f);
    }

    private void Move()
    {
        // make condition to change input according with the player , if is player 1 horizontal and vertical, if player 2 another one..
        this.playerRigidbody.velocity = new Vector2(horizontalInput * moveSpeed * Time.deltaTime, this.playerRigidbody.velocity.y);
    }

    private void Jump()
    {
        Debug.Log("entrou no jump");

        animator.Play("PlayerJump");
        this.playerRigidbody.AddForce(new Vector2(0f, 1 * jumpForce), ForceMode2D.Force);
    }

    private bool IsGrounded()
    {
        Debug.Log("check is grounded");
        bool grounded = Physics2D.OverlapCircle(this.groundCheck.position, 0.25f, this.groundLayer);

        Debug.Log(grounded);

        return grounded;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Spike")
        {
            StartCoroutine(WarpPlayerToCheckPoint());        
        }
    }

    IEnumerator WarpPlayerToCheckPoint()
    {
        playerSprite.enabled = false;
        gameObject.transform.position = lastCheckpoint;
        isDead = true;
        
        //colocar animação de morte aqui

        yield return new WaitForSeconds(1f);

        playerSprite.enabled = true;
        isDead = false;

        
    }
}
