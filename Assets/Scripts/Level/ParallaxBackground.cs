using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ParkourGame
{
    public class ParallaxBackground : MonoBehaviour
    {
        #region Variables

        [SerializeField] private GameObject _camera;
        [SerializeField] private float _parallaxEffect;
        [SerializeField] private SpriteRenderer _sr;

        private float _length;
        private float _xPosition;

        #endregion

        #region Unity lifecycle

        // Start is called before the first frame update
        private void Start()
        {
            _length = _sr.bounds.size.x;
            _xPosition = transform.position.x;
        }

        // Update is called once per frame
        private void Update()
        {
            float distanceMoved = _camera.transform.position.x * (1 - _parallaxEffect);
            float distanceToMove = _camera.transform.position.x * _parallaxEffect;
            transform.position = new Vector3(_xPosition + distanceToMove, transform.position.y);

            if (distanceMoved > _xPosition + _length)
            {
                _xPosition = _xPosition + _length;
            }
        }

        #endregion
    }
}