using UnityEngine;

public class ClimbGamePlayerMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody2D playerRb;
    [SerializeField] private float moveSpeed, jumpVelocity, lowJumpMultiplier, fallMultiplier;
    [SerializeField] private LayerMask groundLayers;
    [SerializeField] private bool wasJumpPressed, isJumpHeld, isGrounded, isDoubleJumpAvailable = true;

    private void HandleX() 
    {
        // By manipulating velocity directly we can allow Unity to do
        // all the hard physics stuff that comes with gravity and such
        float newXVelocity = Input.GetAxis("Horizontal") * moveSpeed * 100 * Time.fixedDeltaTime;    // 100 multiplier because slow
        playerRb.velocity = new Vector2(newXVelocity, playerRb.velocity.y);
    }

    private void HandleY() 
    {
        if (wasJumpPressed) 
        {
            if (isGrounded) { playerRb.velocity += Vector2.up * jumpVelocity; isGrounded = false; }
            else if (isDoubleJumpAvailable) { playerRb.velocity += Vector2.up * jumpVelocity; isDoubleJumpAvailable = false; }
        }

        if (playerRb.velocity.y < 0) playerRb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        else if (playerRb.velocity.y > 0 && !isJumpHeld) playerRb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
    }

    private void Update()
    {
        wasJumpPressed = Input.GetButtonDown("Jump");
        isJumpHeld = Input.GetButton("Jump");
        
        HandleX();
        HandleY();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (((1 << other.gameObject.layer) & groundLayers) != 0) { isGrounded = true; isDoubleJumpAvailable = true; }
    }
}
