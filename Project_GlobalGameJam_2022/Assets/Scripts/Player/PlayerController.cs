using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region Private Fields

    private float horizontalInput;
    private float verticalInput;

    private string horizontalAxis;
    private string verticalAxis;

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

    [SerializeField]
    private PlayerType playerType;
    private bool canDoInput = true;

    #endregion

    #region Properties

    #endregion

    void Awake()
    {
        BPMSwitchNative.OnLockPlayer += this.ChangeInputState;

        GetInput();
    }

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
        this.horizontalInput = Input.GetAxisRaw(horizontalAxis);
        this.verticalInput = Input.GetAxisRaw(verticalAxis);

        if (IsGrounded())
        {
            canJump = true;
        }
    }

    void Ondisable()
    {
        BPMSwitchNative.OnLockPlayer -= this.ChangeInputState;
    }

    private void GetInput()
    {
        switch (this.playerType)
        {
            case PlayerType.Indian:
                this.horizontalAxis = "Horizontal";
                this.verticalAxis = "Vertical";
                break;
            case PlayerType.ModernIndian:
                this.horizontalAxis = "ModernHorizontal";
                this.verticalAxis = "ModernVertical";
                break;
        }
    }

    private void FixedUpdate()
    {
        if (!isDead && canDoInput)
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

    private void ChangeInputState(PlayerType playerToStop)
    {
        Debug.Log("changeinput state");

        Debug.Log("Playertostop" + playerToStop.ToString());

        if (this.playerType == playerToStop)
        {
            Debug.Log("Playertype == player to stop");
            this.canDoInput = false;
        }
        else
        {
            Debug.Log("else");
            this.canDoInput = true;
        }
    }

    private void Move()
    {       
        // make condition to change input according with the player , if is player 1 horizontal and vertical, if player 2 another one..
        this.playerRigidbody.velocity = new Vector2(horizontalInput * moveSpeed * Time.deltaTime, this.playerRigidbody.velocity.y);
    }

    private void Jump()
    {
        //Debug.Log("entrou no jump");

        animator.Play("PlayerJump");
        this.playerRigidbody.AddForce(new Vector2(0f, verticalInput * jumpForce), ForceMode2D.Force);
    }

    private bool IsGrounded()
    {
        //Debug.Log("check is grounded");
        bool grounded = Physics2D.OverlapCircle(this.groundCheck.position, 0.25f, this.groundLayer);

        //Debug.Log(grounded);

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
