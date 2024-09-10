using GameLogic;
using LevelFeatures;
using Spawners;
using UnityEngine;
using UnityEngine.UI;

namespace Levels
{
    public class LevelLoader : MonoBehaviour
    {
        [SerializeField] private Image _background;
        [SerializeField] private SpawnerAirplanes _spawnerAirplanes;
        [SerializeField] private SpawnerPlatforms _spawnerPlatforms;
        [SerializeField] private ScoreHandler _scoreHandler;
        [SerializeField] private Education _education;
        private LevelData _levelData;

        private void OnEnable()
        {
            Education.OnEndEducation += StartSpawnerAirplanes;
        }

        private void OnDisable()
        {
            Education.OnEndEducation -= StartSpawnerAirplanes;
        }

        public void SetLevelData(LevelData levelData)
        {
            _levelData = levelData;

            LoadLevel();

            var startEducation = CheckLevelEducation(_levelData.name);

            if (!startEducation)
                StartSpawnerAirplanes();
        }

        private void LoadLevel()
        {
            _background.sprite = _levelData.Background;

            _spawnerPlatforms.SetPlatforms(_levelData.Platforms, _levelData.CoordPlatforms);

            _scoreHandler.SetScoreAirplanes(_levelData.AircraftToOpenNextLevel,
                                            _levelData.NextLevelName, 
                                            _levelData.name,
                                            _levelData.CurrentAttempt);

            _levelData.CurrentAttempt++;

            LoadingScreenController.instance.EndAnimationFade();
        }

        private void StartSpawnerAirplanes()
        {
            if (_levelData.CurrentDifficulty == 0)
                _spawnerAirplanes.SetAirplanes(_levelData.Airplanes, _levelData.DelaySpawnEasy);
            else if (_levelData.CurrentDifficulty == 1)
                _spawnerAirplanes.SetAirplanes(_levelData.Airplanes, _levelData.DelaySpawnMedium);
            else 
                _spawnerAirplanes.SetAirplanes(_levelData.Airplanes, _levelData.DelaySpawnHard);
        }

        private bool CheckLevelEducation(string nameLevel)
        {
            if (nameLevel == "Island")
            {
                _education.StartEducation();
                return true;
            }
            return false;
        }
    }
}

