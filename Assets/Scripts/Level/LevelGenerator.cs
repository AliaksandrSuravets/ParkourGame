using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace ParkourGame.Level
{
    public class LevelGenerator : MonoBehaviour
    {
        #region Variables

        [SerializeField] private Transform[] _levelPart;
        [SerializeField] private Vector3 _nextPartPosition;
        [SerializeField] private float _distanceToSpawn;
        [SerializeField] private float _distanceToDelete;
        [SerializeField] private Transform _player;
        
        #endregion

        #region Unity lifecycle

        private void Update()
        {
            GeneratePlatform();
            DeletePlatform();
        }

        #endregion

        #region Private methods

        private void GeneratePlatform()
        {
            while (Vector2.Distance(_player.transform.position, _nextPartPosition) < _distanceToSpawn)
            {
                Transform part = _levelPart[Random.Range(0, _levelPart.Length)];
                Vector2 newPosition = new Vector2(_nextPartPosition.x - part.Find("StartPoint").position.x, 0);
                Transform newPart = Instantiate(part, newPosition, Quaternion.identity, transform);
                _nextPartPosition = newPart.Find("EndPoint").position;
            }
        }

        private void DeletePlatform()
        {
            if (transform.childCount <= 0)
            {
                return;
            }

            Transform partToDelete = transform.GetChild(0);

            if (Vector2.Distance(_player.transform.position, partToDelete.transform.position) > _distanceToDelete)
            {
                Destroy(partToDelete.gameObject);
            }
        }
        
        #endregion
    }
}