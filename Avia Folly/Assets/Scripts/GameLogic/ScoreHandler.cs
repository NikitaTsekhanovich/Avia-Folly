using Airplanes;
using GameMenu;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using PlayerData;
using Levels;

namespace GameLogic
{
    public class ScoreHandler : MonoBehaviour
    {
        [SerializeField] private Image _scoreFrame;
        [SerializeField] private TMP_Text _scoreText;
        [SerializeField] private TMP_Text _nextLevelIsOpenText;
        [SerializeField] private AudioSource _scoreIncreaseSound;
        [SerializeField] private AudioSource _levelOpenSound;
        private int _currentAirplanes;
        private int _maxAirplanes;
        private int _levelOpen;
        private string _nextLevelName;
        private string _levelName;
        private int _numberAttempt;

        private void OnEnable()
        {
            Aircraft.OnIncreasesScore += IncreasesScore;
            Destruction.OnDecreaseScore += DecreaseScore;
            PauseMenuHandler.OnRestartScore += RestartScore;
            PauseMenuHandler.OnSetAttempt += SetAttempt;
        }

        private void OnDisable()
        {
            Aircraft.OnIncreasesScore -= IncreasesScore;
            Destruction.OnDecreaseScore -= DecreaseScore;
            PauseMenuHandler.OnRestartScore -= RestartScore;
            PauseMenuHandler.OnSetAttempt -= SetAttempt;
        }

        public void SetScoreAirplanes(
            int maxAirplanes, 
            string nextLevelName, 
            string levelName, 
            int numberAttempt)
        {
            _nextLevelName = nextLevelName;
            _maxAirplanes = maxAirplanes;
            _levelName = levelName;
            _numberAttempt = numberAttempt;
            RestartScore();
        }

        private void RestartScore()
        {
            _scoreText.text = $"0/{_maxAirplanes}";
            _currentAirplanes = 0;
        }

        private void IncreasesScore(int airplanes)
        {
            _scoreIncreaseSound.Play();
            _currentAirplanes += airplanes;
            _scoreText.text = $"{_currentAirplanes}/{_maxAirplanes}";

            // var _currentLandedAircrafts = PlayerPrefs.GetInt($"{_levelName}Score");
            // PlayerPrefs.SetInt($"{_levelName}Score", _currentLandedAircrafts + airplanes);

            PlayerScore.IncreaseNumberAirplanes(airplanes);
        
            DOTween.Sequence()
                .Append(_scoreFrame.DOColor(Color.green, 0.3f))
                .AppendInterval(0.1f)
                .Append(_scoreFrame.DOColor(Color.white, 0.3f));

            CheckMaxScore();
        }

        private void CheckMaxScore()
        {
            if (_currentAirplanes == _maxAirplanes && 
                _nextLevelName != null)
            {
                _levelOpen = PlayerPrefs.GetInt(_nextLevelName);

                if (_levelOpen == (int)LevelTypes.isClosed)
                {
                    _levelOpenSound.Play();
                    _levelOpen = (int)LevelTypes.isOpen;
                    PlayerPrefs.SetInt(_nextLevelName, (int)LevelTypes.isOpen);

                    _nextLevelIsOpenText.gameObject.SetActive(true);

                    DOTween.Sequence()
                        .Append(_nextLevelIsOpenText.gameObject.transform.DOScale(new Vector3(1f, 1f, 1f), 1f))
                        .AppendInterval(1f)
                        .Append(_nextLevelIsOpenText.gameObject.transform.DOScale(new Vector3(0f, 0f, 0f), 1f));
                }
            }
        }

        private void DecreaseScore(int numberDestroyedAirplanes)
        {
            _currentAirplanes -= numberDestroyedAirplanes;

            if (_currentAirplanes < 0)
                _currentAirplanes = 0;

            _scoreText.text = $"{_currentAirplanes}/{_maxAirplanes}";

            DOTween.Sequence()
                .Append(_scoreFrame.DOColor(Color.red, 0.3f))
                .AppendInterval(0.1f)
                .Append(_scoreFrame.DOColor(Color.white, 0.3f));
        }

        private void SetAttempt()
        {
            PlayerScore.SetCurrentAttempt(_levelName, _numberAttempt, _currentAirplanes);
        }
    }
}

