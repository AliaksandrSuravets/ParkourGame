using ParkourGame.Level;
using UnityEngine;
using Random = UnityEngine.Random;

namespace ParkourGame.Service
{
    public class CoinGenerator : MonoBehaviour
    {
        #region Variables

        [SerializeField] private Coin _coin;
        [SerializeField] private int _minCoins;
        [SerializeField] private int _maxCoins;
        private int _amountOfCoins;

        #endregion

        #region Unity lifecycle

        private void Start()
        {
            _amountOfCoins = Random.Range(_minCoins, _maxCoins);
            int additionalOffset = _amountOfCoins / 2;

            for (int i = 0; i < _amountOfCoins; i++)
            {
                Vector3 offse = new Vector2(i - additionalOffset, 0);
                Instantiate(_coin, transform.position + offse, Quaternion.identity, transform);
            }
        }

        #endregion
    }
}