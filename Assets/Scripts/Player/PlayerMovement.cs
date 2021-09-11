using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody2D playerRb;
    [SerializeField] private float moveSpeed, jumpVelocity, lowJumpMultiplier, fallMultiplier;
    [SerializeField] private LayerMask groundLayers;
    [SerializeField] private bool isGrounded, isJumpPressed;

    private void HandleX() 
    {
        // By manipulating velocity directly we can allow Unity to do
        // all the hard physics stuff that comes with gravity and such
        float newXVelocity = Input.GetAxis("Horizontal") * moveSpeed * 100 * Time.fixedDeltaTime;    // 100 multiplier because slow
        playerRb.velocity = new Vector2(newXVelocity, playerRb.velocity.y);
    }

    private void HandleY() 
    {
        if (isJumpPressed && isGrounded) { playerRb.velocity += Vector2.up * jumpVelocity; isGrounded = false; }

        if (playerRb.velocity.y < 0) playerRb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.fixedDeltaTime;
        else if (playerRb.velocity.y > 0 && !isJumpPressed) playerRb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.fixedDeltaTime;
    }

    private void FixedUpdate()  // Because physics
    {
        HandleX();
        HandleY();
    }

    private void Update() => isJumpPressed = Input.GetButton("Jump");

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (((1 << other.gameObject.layer) & groundLayers) != 0) isGrounded = true; 
    }
}
