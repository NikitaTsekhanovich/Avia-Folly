using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Levels;
using PlayerData;
using TMPro;

namespace Menu
{
    public class MenuHandler : MonoBehaviour
    {
        [SerializeField] private Image _nameCurrentLevel;
        [SerializeField] private Button _prevLevelButton;
        [SerializeField] private Button _nextLevelButton;
        [SerializeField] private List<LevelData> _levelsData = new();
        [SerializeField] private Image _backgroundLevel;
        [SerializeField] private Image _easyDifficultyImage;
        [SerializeField] private Image _mediumDifficultyImage;
        [SerializeField] private Image _hardDifficultyImage;
        [SerializeField] private TMP_Text _blockPlayButtonText;
        [SerializeField] private Button _playButton;
        [SerializeField] private Image _playButtonBackground;
        private int _indexLevel = 0;
        private Image _imagePrevLevel;
        private Image _imageNextLevel;

        private void Awake()
        {
            Screen.orientation = ScreenOrientation.LandscapeLeft;
        }

        private void Start()
        {
            _imagePrevLevel = _prevLevelButton.GetComponent<Image>();
            _imageNextLevel = _nextLevelButton.GetComponent<Image>();
            ChangeData();
        }

        public void ChooseEasyDifficulty()
        {
            _levelsData[_indexLevel].CurrentDifficulty = 0;
            _easyDifficultyImage.color = new Color(1f, 1f, 1f);
            _mediumDifficultyImage.color = new Color(0.6f, 0.6f, 0.6f);
            _hardDifficultyImage.color = new Color(0.6f, 0.6f, 0.6f);
        }

        public void ChooseMediumDifficulty()
        {
            _levelsData[_indexLevel].CurrentDifficulty = 1;
            _easyDifficultyImage.color = new Color(0.6f, 0.6f, 0.6f);
            _mediumDifficultyImage.color = new Color(1f, 1f, 1f);
            _hardDifficultyImage.color = new Color(0.6f, 0.6f, 0.6f);
        }

        public void ChooseHardDifficulty()
        {
            _levelsData[_indexLevel].CurrentDifficulty = 2;
            _easyDifficultyImage.color = new Color(0.6f, 0.6f, 0.6f);
            _mediumDifficultyImage.color = new Color(0.6f, 0.6f, 0.6f);
            _hardDifficultyImage.color = new Color(1f, 1f, 1f);
        }

        public void GetPrevLevel()
        {
            if (_indexLevel > -1)
            {
                if (_indexLevel > 0)
                {
                    _indexLevel--;
                    _imageNextLevel.color = new Color (1, 1, 1);

                    if (_indexLevel <= 0)
                        _imagePrevLevel.color = new Color (0.4f, 0.4f, 0.4f);

                    CheckAccessLevel();
                }

                ChangeData();
            }
        }

        public void GetNextLevel()
        {
            if (_indexLevel < _levelsData.Count)
            {
                if (_indexLevel < _levelsData.Count - 1)
                {
                    _indexLevel++;
                    _imagePrevLevel.color = new Color (1, 1, 1);

                    if (_indexLevel >= _levelsData.Count - 1)
                        _imageNextLevel.color = new Color (0.4f, 0.4f, 0.4f);

                    CheckAccessLevel();
                }                           

                ChangeData();
            }
        }

        private void CheckAccessLevel()
        {
            if (_levelsData[_indexLevel].LevelOpen == (int)LevelTypes.isClosed && 
            _levelsData[_indexLevel].AircraftToOpen > PlayerScore.CurrentLandingAirplanes)
            {
                _blockPlayButtonText.gameObject.SetActive(true);
                _blockPlayButtonText.text = 
                $"{PlayerScore.CurrentLandingAirplanes}/{_levelsData[_indexLevel].AircraftToOpen} aircraft landed to unlock";
                _playButton.enabled = false;
                _playButtonBackground.color = new Color (0.4f, 0.4f, 0.4f);
            }
            else
            {
                _blockPlayButtonText.gameObject.SetActive(false);
                _playButton.enabled = true;
                _playButtonBackground.color = new Color (1f, 1f, 1f);
            }
        }

        private void ChangeData()
        {
            _nameCurrentLevel.sprite = _levelsData[_indexLevel].NameLevelImage;
            _backgroundLevel.sprite = _levelsData[_indexLevel].Background;

            if (_levelsData[_indexLevel].CurrentDifficulty == 0)
                ChooseEasyDifficulty();
            else if (_levelsData[_indexLevel].CurrentDifficulty == 1)
                ChooseMediumDifficulty();
            else 
                ChooseHardDifficulty();
        }

        public void PlayLevel()
        {
            LoadingScreenController.instance.StartAnimationFade();
            LevelDataCarrier.instance.SetLevel(_levelsData[_indexLevel]);
            SceneManager.LoadScene("Game");
        }
    }
}

