 
using System.Collections;
using Cinemachine;
using ParkourGame.Service;
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
        [SerializeField] private SpriteRenderer _sr;
        [SerializeField] private CinemachineVirtualCamera _camera;
        
        [Header("Slide info")]
        [SerializeField] private float _slideSpeed;
        [SerializeField] private float _slideTimer;
        [SerializeField] private float _slideCooldown;
        [SerializeField] private float _ceillingCheckDistance;

        [Header("Ledge info")]
        [SerializeField] private Vector2 _offset1;
        [SerializeField] private Vector2 _offset2;

        [Header("Knockback info")]
        [SerializeField] private Vector2 _knockackDir;
        private bool _canBeKnocked = true;
        private bool _canClimb;

        private bool _canDoubleJump;
        private bool _canGrabLedge = true;
        private bool _ceillingDetected;

        private Vector2 _climbBegunPosition;
        private Vector2 _climbOverPosition;

        private bool _isDead;

        private bool _isGrounded;
        private bool _isKnocked;
        private bool _isRunning;
        private bool _isSliding;
        private bool _isWall;
        private bool _ledgeDetected;
        private float _slideCooldownCounter;
        private float _slideTimerCounter;

        #endregion

        #region Unity lifecycle

        private void Update()
        {
            _slideTimerCounter -= Time.deltaTime;
            _slideCooldownCounter -= Time.deltaTime;

            Animation();
            

            if (Time.timeScale == 0)
            {
                return;
            }

            if (!GameService.instance.CanRun)
            {
                return;
            }

            if (_isDead)
            {
                return;
            }

            if (_isKnocked)
            {
                return;
            }

            Moving();

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

            CheckForLedge();
            CheckForSlide();
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawLine(transform.position,
                new Vector2(transform.position.x, transform.position.y - _groundCheckDistance));
            Gizmos.DrawLine(transform.position,
                new Vector2(transform.position.x, transform.position.y + _ceillingCheckDistance));
            Gizmos.DrawWireCube(_wallCheck.position, _wallCheckSize);
        }

        #endregion

        #region Public methods

        public void SetLedge(bool value)
        {
            _ledgeDetected = value;
        }

        #endregion

        #region Private methods

        private void AllowLedgeGrab()
        {
            _canGrabLedge = true;
        }

        private void Animation()
        {
            _playerAnimation.SetFloatRun(_rb.velocity.x);
            _playerAnimation.SetFloatJump(_rb.velocity.y);
            _playerAnimation.SetBoolJump(GroundCheck());
            _playerAnimation.SetBoolSlide(_isSliding);
            _playerAnimation.SetBoolLedge(_canClimb);
            _playerAnimation.SetBoolKnocked(_isKnocked);

            if (_rb.velocity.y < -5)
            {
                _playerAnimation.SetBoolRoll(true);
            }
        }

        private void CancelKnockback()
        {
            _isKnocked = false;
        }

        private void CheckForLedge()
        {
            if (_ledgeDetected && _canGrabLedge)
            {
                _canGrabLedge = false;

                Vector2 ledgePosition = GetComponentInChildren<LedgeDetection>().transform.position;
                _climbBegunPosition = ledgePosition + _offset1;
                _climbOverPosition = ledgePosition + _offset2;

                _canClimb = true;
            }

            if (_canClimb)
            {
                transform.position = _climbBegunPosition;
            }
        }

        private void CheckForSlide()
        {
            if (_slideTimerCounter < 0 && !SilingCheck())
            {
                _isSliding = false;
            }
        }

        public void StartDie()
        {
            StartCoroutine(Die());
        }

        public void DieInSpace()
        {
            _camera.Follow = null;
            Destroy(_camera.gameObject);
            _isDead = true;
            _canBeKnocked = false;
            _rb.velocity = new Vector2(0, 0);
            GameService.instance.CallEndUi();
        }
        
        private IEnumerator Die()
        {
            AudioService.Instance.PlaySFX(3);
            _isDead = true;
            _canBeKnocked = false;
            _rb.velocity = new Vector2(0, 0);
            _playerAnimation.SetBoolDead(true);
            yield return new WaitForSeconds(1f);
            GameService.instance.CallEndUi();
        }

        private bool GroundCheck()
        {
            return Physics2D.Raycast(transform.position, Vector2.down, _groundCheckDistance, _whatIsGround);
        }

        private IEnumerator Invicibility()
        {
            Color originalColor = _sr.color;
            Color darkenColor = new Color(_sr.color.r, _sr.color.g, _sr.color.b, .5f);
            _canBeKnocked = false;
            _sr.color = darkenColor;
            yield return new WaitForSeconds(2.5f);
            _sr.color = originalColor;
            _canBeKnocked = true;
        }

        private void Jump()
        {
            if (SilingCheck())
            {
                return;
            }

            if (GroundCheck())
            {
                _canDoubleJump = true;
                _rb.velocity = new Vector2(_moveSpeed, _jumpForce);
                AudioService.Instance.PlaySFX(Random.Range(1,2));
            }
            else if (_canDoubleJump)
            {
                _canDoubleJump = false;
                _rb.velocity = new Vector2(_moveSpeed, _jumpForce);
                AudioService.Instance.PlaySFX(Random.Range(1,2));
            }
        }

        public void Knockback()
        {
            if (!_canBeKnocked)
            {
                return;
            }

            StartCoroutine(Invicibility());
            _isKnocked = true;
            _rb.velocity = _knockackDir;
        }

        private void LedgeClimbOver()
        {
            _canClimb = false;

            transform.position = _climbOverPosition;
            Invoke("AllowLedgeGrab", .1f);
        }

        private void Moving()
        {
            if (WallCheck())
            {
                return;
            }

            if (_isSliding)
            {
                _rb.velocity = new Vector2(_slideSpeed, _rb.velocity.y);
            }
            else
            {
                _rb.velocity = new Vector2(_moveSpeed, _rb.velocity.y);
            }
        }

        private void RollAnimFinished()
        {
            _playerAnimation.SetBoolRoll(false);
        }

        private bool SilingCheck()
        {
            return Physics2D.Raycast(transform.position, Vector2.up, _ceillingCheckDistance, _whatIsGround);
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