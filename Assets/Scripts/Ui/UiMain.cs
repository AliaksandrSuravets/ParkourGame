using System.Collections;
using System.Collections.Generic;
using ParkourGame.Service;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ParkourGame
{
    public class UiMain : MonoBehaviour
    {
        #region Variables

        private bool _gamePaused;

        #endregion

        #region Public methods

        public void PauseGame()
        {
            if (_gamePaused)
            {
                Time.timeScale = 1;
                _gamePaused = false;
            }
            else
            {
                Time.timeScale = 0;
                _gamePaused = true;
            }
        }

        public void Restart()
        {
            GameService.instance.SetBestScore();
            SceneManager.LoadScene(0);
        }

        public void SetCanRun()
        {
            GameService.instance.Coin = 0;
            GameService.instance.CanRun = true;
        }

        public void SwitchMenuTo(GameObject uiMenu)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).gameObject.SetActive(false);
            }

            uiMenu.SetActive(true);
        }

        #endregion
    }
}