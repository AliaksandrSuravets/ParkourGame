using UnityEngine;

namespace ParkourGame.PlayerFolder
{
    public class PlayerAnimation : MonoBehaviour
    {

        [SerializeField] private Animator _animator;
        private readonly int _isRunning = Animator.StringToHash("isRunning");
        private readonly int _yVelocity = Animator.StringToHash("yVelocity");
        private readonly int _xVelocity = Animator.StringToHash("xVelocity");
        private readonly int _isGrounded = Animator.StringToHash("isGrounded");
        private readonly int _isSliding = Animator.StringToHash("isSliding");
        private readonly int _canClimb = Animator.StringToHash("canClimb");
        private readonly int _canRoll = Animator.StringToHash("canRoll");
        private readonly int _isKnocked = Animator.StringToHash("isKnocked");
        private readonly int _isDead = Animator.StringToHash("isDead");
        public void SetBoolRun(bool value)
        {
            _animator.SetBool(_isRunning, value);
        }

        public void SetFloatJump(float value)
        {
            _animator.SetFloat(_yVelocity, value);
        }

        public void SetFloatRun(float value)
        {
            _animator.SetFloat(_xVelocity, value);
        }

        
        public void SetBoolJump(bool value)
        {
            _animator.SetBool(_isGrounded, value);
        }

        public void SetBoolSlide(bool value)
        {
            _animator.SetBool(_isSliding, value);
        }
        
        public void SetBoolLedge(bool value)
        {
            _animator.SetBool(_canClimb, value);
        }
        
        public void SetBoolRoll(bool value)
        {
            _animator.SetBool(_canRoll, value);
        }
        
        public void SetBoolKnocked(bool value)
        {
            _animator.SetBool(_isKnocked, value);
        }

        public void SetBoolDead(bool value)
        {
            _animator.SetBool(_isDead, value);
        }
        
    }
}
