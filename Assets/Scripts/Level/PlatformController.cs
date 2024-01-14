using System;
using ParkourGame.Service;
using UnityEngine;

namespace ParkourGame.Level
{
    public class PlatformController : MonoBehaviour
    {

        [SerializeField] private SpriteRenderer _sr;
        [SerializeField] private SpriteRenderer _srHeader;

        private void Start()
        {
            _srHeader.transform.parent = transform.parent;
            _srHeader.transform.localScale = new Vector2(_sr.bounds.size.x, .2f);
            _srHeader.transform.position = new Vector2(transform.position.x, _sr.bounds.max.y);
            _srHeader.color = _sr.color;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            _srHeader.color = GameService.instance.GetColor();
        }
    }
}
