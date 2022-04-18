using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _playerRb;
    [SerializeField] private float _moveSpeed, _jumpVelocity, _lowJumpMultiplier, _fallMultiplier;
    [SerializeField] private LayerMask _groundLayers;
    [SerializeField] private bool _isGrounded, _isJumpPressed;

    private void HandleX() 
    {
        // By manipulating velocity directly we can allow Unity to do
        // all the hard physics stuff that comes with gravity and such
        float newXVelocity = Input.GetAxis("Horizontal") * _moveSpeed * 100 * Time.fixedDeltaTime;    // 100 multiplier because slow
        _playerRb.velocity = new Vector2(newXVelocity, _playerRb.velocity.y);
    }

    private void HandleY() 
    {
        if (_isJumpPressed && _isGrounded) { _playerRb.velocity += Vector2.up * _jumpVelocity; _isGrounded = false; }

        if (_playerRb.velocity.y < 0) _playerRb.velocity += Vector2.up * Physics2D.gravity.y * (_fallMultiplier - 1) * Time.fixedDeltaTime;
        else if (_playerRb.velocity.y > 0 && !_isJumpPressed) _playerRb.velocity += Vector2.up * Physics2D.gravity.y * (_lowJumpMultiplier - 1) * Time.fixedDeltaTime;
    }

    private void FixedUpdate()  // Because physics
    {
        HandleX();
        HandleY();
    }

    private void Update() => _isJumpPressed = Input.GetButton("Jump");

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (((1 << other.gameObject.layer) & _groundLayers) != 0) _isGrounded = true; 
    }
}
