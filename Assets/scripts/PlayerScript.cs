using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    [SerializeField] private Rigidbody2D myRigidBody;

    [Header("Movement")] // a header in unity
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpHeight;

    [Header("Collision")]
    [SerializeField] private float groundCheckDist;
    [SerializeField] private LayerMask whatIsGround;
    private bool isGrounded;

    private float xIn;
    private Animator anim;

    private bool facingRight = true;
    private int facingDir = 1;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    private void Update()
    {
        HandleCollision();
        HandleInput();
        HandleMovement();
        HandleFlip();
        HandleAnimations();
    }

    // gets x value from game and checks to see if jump is available
    private void HandleInput()
    {
        xIn = Input.GetAxisRaw("Horizontal");

        if ((Input.GetKeyDown(KeyCode.W) | Input.GetKeyDown(KeyCode.Space)) && isGrounded) Jump();
    }
    
    // adjusts y value
    private void Jump() => myRigidBody.linearVelocity = new Vector2(myRigidBody.linearVelocity.x, jumpHeight);

    private void HandleCollision()
    {
        isGrounded = Physics2D.Raycast(transform.position, Vector2.down, groundCheckDist, whatIsGround);
    }

    private void HandleAnimations()
    {
        anim.SetFloat("xVelocity", myRigidBody.linearVelocity.x);
    }

    // checks to see if character needs to be flipped
    private void HandleFlip()
    {
        if (xIn < 0 && facingRight || xIn > 0 && !facingRight) Flip();
    }

    // flips the sprite when facing left or right
    private void Flip()
    {
        facingDir *= -1;
        transform.Rotate(0, 180, 0);
        facingRight = !facingRight;
    }

    // draws a white line in the scene tab from center of character to bottom of character
    private void OnDrawGizmos() => Gizmos.DrawLine(transform.position, new Vector2(transform.position.x, transform.position.y - groundCheckDist));

    // adjusts x value
    private void HandleMovement() => myRigidBody.linearVelocity = new Vector2(xIn * moveSpeed, myRigidBody.linearVelocity.y);
}
