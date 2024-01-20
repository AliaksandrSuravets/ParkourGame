using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace ParkourGame.Service
{
    public class AudioService : MonoBehaviour
    {
        #region Variables

        public static AudioService Instance;

        [SerializeField] private AudioSource[] _sfx;
        [SerializeField] private AudioSource[] _bgm;

        private int _bgmIndex;
        #endregion

        #region Unity lifecycle

        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            PlayBGM(Random.Range(0, _bgm.Length));
        }

        public void PlayRandomBGM()
        {
            _bgmIndex = Random.Range(0, _bgm.Length);
            PlayBGM(_bgmIndex);
        }

        private void Update()
        {
            if (!_bgm[_bgmIndex].isPlaying)
            {
                PlayRandomBGM();
            }
        }

        #endregion

        #region Public methods

        public void PlayBGM(int index)
        {
            foreach (AudioSource audioSource in _bgm)
            {
                audioSource.Stop();
            }

            _bgm[index].Play();
        }

        public void PlaySFX(int i)
        {
            if (i < _sfx.Length)
            {
                _sfx[i].pitch = Random.Range(0.85f, 1.15f);
                _sfx[i].Play();
            }
        }

        public void StopBGM()
        {
            foreach (AudioSource audioSource in _bgm)
            {
                audioSource.Stop();
            }
        }

        public void StopSFX(int i)
        {
            _sfx[i].Stop();
        }

        #endregion
    }
}