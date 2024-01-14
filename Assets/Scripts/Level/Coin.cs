using System;
using System.Collections;
using System.Collections.Generic;
using ParkourGame.Service;
using UnityEngine;

namespace ParkourGame
{
    public class Coin : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                GameService.instance.AddCoin();
                Destroy(gameObject);
            }
        }
    }
}
