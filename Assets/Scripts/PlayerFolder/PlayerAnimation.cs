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
        
    }
}
