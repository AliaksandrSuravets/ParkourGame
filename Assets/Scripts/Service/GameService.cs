using System;
using UnityEngine;

namespace ParkourGame.Service
{
    public class GameService : MonoBehaviour
    {

        [SerializeField] private Color _color;
        
        public static GameService instance;
        public int Coin { get; private set; }
        private void Awake()
        {
            instance = this;
        }

        public void AddCoin()
        {
            Coin++;
        }

        public Color GetColor()
        {
            return _color;
        }
        
    }
}
