using ParkourGame.Service;
using TMPro;
using UnityEngine;

namespace ParkourGame.Ui
{
    public class UiInfoBar : MonoBehaviour
    {
        [SerializeField] private TMP_Text _bestScore;
        [SerializeField] private TMP_Text _currentScore;
        
        
        // Start is called before the first frame update
        void Start()
        {
            _bestScore.text = $"{_bestScore.text} {GameService.instance.BestScore}";
            GameService.instance.OnChangeScore += OnChangeScore;
        }
        
        private void OnDestroy()
        {
            GameService.instance.OnChangeScore -= OnChangeScore;
        }

        // Update is called once per frame
        private void OnChangeScore()
        {
            if (_currentScore != null)
            {
                _currentScore.text = $"Score: {GameService.instance.Coin}";
            }
        }
    }
}
