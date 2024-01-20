using ParkourGame.PlayerFolder;
using UnityEngine;

namespace ParkourGame.Enemy
{
    public class Trap : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out PlayerMoving player))
            {
                player.Knockback();
            }
        }
    }
}
