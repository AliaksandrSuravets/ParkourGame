using System;
using ParkourGame.Ui;
using UnityEngine;
using YG;

namespace ParkourGame.Service
{
    public class GameService : MonoBehaviour
    {
        #region Variables

        public static GameService instance;

        [SerializeField] private Color _color;
        [SerializeField] private UiMain _uiMain;
        [SerializeField] private GameObject _endUI;

        #endregion

        #region Events

        public event Action OnChangeScore;

        #endregion

        #region Properties

        public int BestScore { get; set; }
        public bool CanRun { get; set; }
        public int Coin { get; set; }

        #endregion

        #region Unity lifecycle

        private void Awake()
        {
            Time.timeScale = 1;
            BestScore = YandexGame.savesData.BestScore;
            instance = this;
            CanRun = false;
        }

        #endregion

        #region Public methods

        public void AddCoin()
        {
            Coin++;
            OnChangeScore?.Invoke();
        }

        public void CallEndUi()
        {
            _uiMain.SwitchMenuTo(_endUI);
        }

        public Color GetColor()
        {
            return _color;
        }

        public void SetBestScore()
        {
            if (Coin <= BestScore)
            {
                return;
            }

            Debug.Log(Coin);
            BestScore = Coin;
            YandexGame.savesData.BestScore = BestScore;
            YandexGame.SaveProgress();
        }

        #endregion
    }
}