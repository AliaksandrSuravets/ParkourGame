using ParkourGame.Level;
using UnityEngine;

namespace ParkourGame.PlayerFolder
{
    public class LedgeDetection : MonoBehaviour
    {
        #region Variables

        [SerializeField] private float _radius;
        [SerializeField] private LayerMask _whatIsGround;
        [SerializeField] private PlayerMoving _player;
        [SerializeField] private BoxCollider2D _boxCollider2D;
        
        private bool _canDetected = true;

        

        #endregion

        #region Unity lifecycle

        private void Update()
        {
            if (_canDetected)
            {
                _player.SetLedge(Physics2D.OverlapCircle(transform.position, _radius, _whatIsGround));
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(transform.position, _radius);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("Platform"))
            {
                _canDetected = false;
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {

            Collider2D[] colliders = Physics2D.OverlapBoxAll(_boxCollider2D.bounds.center, _boxCollider2D.size, 0);

            foreach (Collider2D hit in colliders)
            {
                if (hit.gameObject.GetComponent<PlatformController>() != null)
                {
                    return;
                }
            }
            
            if (other.gameObject.layer == LayerMask.NameToLayer("Platform"))
            {
                _canDetected = true;
            }
        }

        #endregion
    }
}