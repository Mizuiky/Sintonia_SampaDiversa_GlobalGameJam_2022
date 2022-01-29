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

    private Rigidbody2D playerRigidbody;

    #endregion



    #region Properties

    #endregion

    void Start()
    {
        this.playerRigidbody = this.GetComponent<Rigidbody2D>();
        this.groundLayer = LayerMask.GetMask("Ground");
    }

    // Update is called once per frame
    void Update()
    {
        
        
    }

    private void FixedUpdate()
    {
        this.Move();

        this.Jump();
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(this.groundCheck.position, 0.05f);
    }

    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) && IsGrounded())
        {
            this.playerRigidbody.AddForce(Vector2.up * jumpForce);
        }
    }

    private void Move()
    {
        horizontalInput = Input.GetAxis("Horizontal");

        this.playerRigidbody.AddForce(Vector2.right * horizontalInput * moveSpeed * Time.deltaTime);
    }

    private bool IsGrounded()
    {
        Debug.Log("check is grounded");
        bool grounded = Physics2D.OverlapCircle(this.groundCheck.position, 0.05f, this.groundLayer);

        Debug.Log(grounded);

        return grounded;
    }
}
