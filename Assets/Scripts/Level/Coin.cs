using ParkourGame.Service;
using UnityEngine;

namespace ParkourGame.Level
{
    public class Coin : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                GameService.instance.AddCoin();
                AudioService.Instance.PlaySFX(0);
                Destroy(gameObject);
            }
        }
    }
}
