using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using YG;

namespace ParkourGame
{
    public class UiVolumeSlider : MonoBehaviour
    {
        #region Variables

        [SerializeField] private Slider _slider;
        [SerializeField] private AudioMixer _audioMixer;
        [SerializeField] private string _audioParametr;
        [SerializeField] private float _multiplier = 25;

        #endregion

        #region Unity lifecycle

        private void OnDisable()
        {
            
            switch (_audioParametr)
            {
                case "sfx":
                    YandexGame.savesData.sfx = _slider.value;
                    break;
                case "bgm":
                    YandexGame.savesData.bgm = _slider.value;
                    break;
            }
            YandexGame.SaveProgress();
   
        }

        #endregion

        #region Public methods

        public void SetupSlider()
        {
         
            _slider.minValue = .001f;
            switch (_audioParametr)
            {
                case "sfx":
                    _slider.value = YandexGame.savesData.sfx;
                    break;
                case "bgm":
                    _slider.value = YandexGame.savesData.bgm;
                    break;
            }
            
            _slider.onValueChanged.AddListener(SliderValue);
            SliderValue(_slider.value);
        }

        #endregion

        #region Private methods

        private void SliderValue(float value)
        {
            _audioMixer.SetFloat(_audioParametr, (float) (Math.Log10(value) * _multiplier));
        }

        #endregion
    }
}