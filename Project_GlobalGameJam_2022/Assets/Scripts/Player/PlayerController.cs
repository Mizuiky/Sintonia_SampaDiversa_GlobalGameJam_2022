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

    #endregion

    #region Properties

    #endregion

    void Start()
    {
        this.playerRigidbody = this.GetComponent<Rigidbody2D>();
        this.groundLayer = LayerMask.GetMask("Ground");
        lastCheckpoint = gameObject.transform.position; 
    }

    // Update is called once per frame
    void Update()
    {
        if (!isDead)
        {
            this.Move();

            this.Jump();

        }
    }

    private void FixedUpdate()
    {
        
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(this.groundCheck.position, 0.25f);
    }

    private void Move()
    {
        this.horizontalInput = Input.GetAxis("Horizontal");

        // make condition to change input according with the player , if is player 1 horizontal and vertical, if player 2 another one..
        this.playerRigidbody.velocity = new Vector2(horizontalInput * moveSpeed * Time.deltaTime, this.playerRigidbody.velocity.y);
    }

    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) && IsGrounded())
        {
            Debug.Log("entrou no jump");
            this.playerRigidbody.AddForce(new Vector2(0f, 1 * jumpForce), ForceMode2D.Force);
        }
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
