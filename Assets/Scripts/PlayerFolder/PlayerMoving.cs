using System;
using UnityEngine;

namespace ParkourGame.PlayerFolder
{
    public class PlayerMoving : MonoBehaviour
    {
        #region Variables

        [Header("Moving")]
        [SerializeField] [Min(0)] private float _moveSpeed;
        [SerializeField] [Min(0)] private float _jumpForce;

        [Header("Check Ground and Wall")]
        [SerializeField] [Min(0)] private float _groundCheckDistance;
        [SerializeField] private LayerMask _whatIsGround;

        [SerializeField] private Transform _wallCheck;
        [SerializeField] private Vector2 _wallCheckSize;

        [Header("Component")]
        [SerializeField] private Rigidbody2D _rb;
        [SerializeField] private PlayerAnimation _playerAnimation;

        [Header("Slide info")]
        [SerializeField] private float _slideSpeed;
        [SerializeField] private float _slideTimer;
        [SerializeField] private float _slideCooldown;

        private bool _canDoubleJump;

        private bool _isGrounded;
        private bool _isRunning;
        private bool _isSliding;
        private bool _isWall;
        private float _slideCooldownCounter;

        private float _slideTimerCounter;

        #endregion

        #region Unity lifecycle

        private void Update()
        {
            _slideTimerCounter -= Time.deltaTime;
            _slideCooldownCounter -= Time.deltaTime;

            Animation();
            if (!WallCheck())
            {
                Moving();
            }

            if (GroundCheck())
            {
                _canDoubleJump = true;
            }

            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space))
            {
                Jump();
            }

            if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.LeftShift))
            {
                Slide();
            }

            CheckForSlide();
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawLine(transform.position,
                new Vector2(transform.position.x, transform.position.y - _groundCheckDistance));
            Gizmos.DrawWireCube(_wallCheck.position, _wallCheckSize);
        }

        #endregion

        #region Private methods

        private void Animation()
        {
            _playerAnimation.SetFloatRun(_rb.velocity.x);
            _playerAnimation.SetBoolJump(GroundCheck());
            _playerAnimation.SetFloatJump(_rb.velocity.y);
            _playerAnimation.SetBoolSlide(_isSliding);
        }

        private void CheckForSlide()
        {
            if (_slideTimerCounter < 0)
            {
                _isSliding = false;
            }
        }

        private bool GroundCheck()
        {
            return Physics2D.Raycast(transform.position, Vector2.down, _groundCheckDistance, _whatIsGround);
        }

        private void Jump()
        {
            if (GroundCheck())
            {
                _canDoubleJump = true;
                _rb.velocity = new Vector2(_moveSpeed, _jumpForce);
            }
            else if (_canDoubleJump)
            {
                _canDoubleJump = false;
                _rb.velocity = new Vector2(_moveSpeed, _jumpForce);
            }
        }

        private void Moving()
        {
            if (_isSliding)
            {
                _rb.velocity = new Vector2(_slideSpeed, _rb.velocity.y);
            }
            else
            {
                _rb.velocity = new Vector2(_moveSpeed, _rb.velocity.y);
            }
        }

        private void Slide()
        {
            if (_rb.velocity.x != 0 && _slideCooldownCounter < 0)
            {
                _isSliding = true;
                _slideTimerCounter = _slideTimer;
                _slideCooldownCounter = _slideCooldown;
            }
        }

        private bool WallCheck()
        {
            return Physics2D.BoxCast(_wallCheck.position, _wallCheckSize, 0, Vector2.zero, 0, _whatIsGround);
        }

        #endregion
    }
}