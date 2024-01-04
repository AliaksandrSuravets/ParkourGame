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

        [Header("Check Ground")]
        [SerializeField] [Min(0)] private float _groundCheckDistance;
        [SerializeField] private LayerMask _whatIsGround;

        [Header("Component")]
        [SerializeField] private Rigidbody2D _rb;

        private bool _isGrounded;

        #endregion

        #region Unity lifecycle

        private void Update()
        {
            //_isGrounded = GroundCheck();
            Moving();

            if (Input.GetKeyDown(KeyCode.Space))
            {
                Jump();
            }
        }

        #endregion

        #region Private methods

        private bool GroundCheck()
        {
            return Physics2D.Raycast(transform.position, Vector2.down, _groundCheckDistance, _whatIsGround);
        }

        private void Jump()
        {
            if (GroundCheck())
            {
                _rb.velocity = new Vector2(_moveSpeed, _jumpForce);
            }
            
        }

        private void Moving()
        {
            _rb.velocity = new Vector2(_moveSpeed, _rb.velocity.y);
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawLine(transform.position , new Vector2(transform.position.x, transform.position.y - _groundCheckDistance));
        }

        #endregion
    }
}