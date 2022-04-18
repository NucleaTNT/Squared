using System.Collections;
using UnityEngine;

namespace Dev.NucleaTNT.Squared.PlayerScripts
{
    public class ClimbGamePlayerMovement : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D _playerRb;
        [SerializeField] private float _moveSpeed, _jumpVelocity, _lowJumpMultiplier, _fallMultiplier;
        [SerializeField] private bool _wasJumpPressed, _isJumpHeld, _isGrounded, _isDoubleJumpAvailable = true;
        [SerializeField] private bool _isSideSwapOnCooldown = false;

        public bool IsSideSwapOnCooldown => _isSideSwapOnCooldown;
        public float JumpVelocity => _jumpVelocity;

        private void HandleX()
        {
            // By manipulating velocity directly we can allow Unity to do
            // all the hard physics stuff that comes with gravity and such
            float newXVelocity =
                Input.GetAxis("Horizontal") * _moveSpeed * 100 * Time.fixedDeltaTime; // 100 multiplier because slow
            _playerRb.velocity = new Vector2(newXVelocity, _playerRb.velocity.y);
        }

        private void HandleY()
        {
            if (_wasJumpPressed)
            {
                if (_isGrounded)
                {
                    _playerRb.velocity += Vector2.up * _jumpVelocity;
                    _isGrounded = false;
                }
                else if (_isDoubleJumpAvailable)
                {
                    _playerRb.velocity += (Vector2.up * _jumpVelocity) + ((_playerRb.velocity.y < 0)
                        ? new Vector2(0, -(_playerRb.velocity.y))
                        : Vector2.zero);
                    _isDoubleJumpAvailable = false;
                }
            }

            if (_playerRb.velocity.y < 0)
                _playerRb.velocity += Vector2.up * Physics2D.gravity.y * (_fallMultiplier - 1) * Time.deltaTime;
            else if (_playerRb.velocity.y > 0 && !_isJumpHeld)
                _playerRb.velocity += Vector2.up * Physics2D.gravity.y * (_lowJumpMultiplier - 1) * Time.deltaTime;
        }

        private void Update()
        {
            _wasJumpPressed = Input.GetButtonDown("Jump");
            _isJumpHeld = Input.GetButton("Jump");

            HandleX();
            HandleY();
        }

        public void ResetJump()
        {
            _isGrounded = true;
            _isDoubleJumpAvailable = true;
        }

        public IEnumerator SideSwapCooldownCoroutine(float cooldownTime)
        {
            _isSideSwapOnCooldown = true;
            yield return new WaitForSecondsRealtime(cooldownTime);
            _isSideSwapOnCooldown = false;
        }
    }
}