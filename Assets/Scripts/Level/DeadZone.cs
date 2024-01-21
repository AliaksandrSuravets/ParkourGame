using ParkourGame.PlayerFolder;
using UnityEngine;

namespace ParkourGame.Level
{
    public class DeadZone : MonoBehaviour
    {
        #region Unity lifecycle

        // Start is called before the first frame update
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out PlayerMoving player))
            {
                player.DieInSpace();
            }
        }

        #endregion
    }
}