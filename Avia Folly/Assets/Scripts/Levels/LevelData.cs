using UnityEngine;
using System.Collections.Generic;

namespace Levels
{
    [CreateAssetMenu(fileName = "LevelData", menuName = "Levels data/ Level")]
    public class LevelData : ScriptableObject
    {
        [SerializeField] private string _name;
        [SerializeField] private Sprite _background;
        [SerializeField] private Sprite _nameLevelImage;
        [SerializeField] private List<GameObject> _platforms;
        [SerializeField] private GameObject _coordPlatforms;
        [SerializeField] private List<GameObject> _airplanes; 
        [SerializeField] private int _aircraftToOpen;
        [SerializeField] private int _aircraftToOpenNextLevel;
        [SerializeField] private int _levelOpen => PlayerPrefs.GetInt(_name);
        [SerializeField] private string _nextLevelName;
        private int _countLandedAircrafts => PlayerPrefs.GetInt($"{_name}Score");

        [Header("Level difficulty data")]
        [SerializeField] private float _delaySpawnEasy;
        [SerializeField] private float _delaySpawnMedium;
        [SerializeField] private float _delaySpawnHard;
        public int CurrentDifficulty;
        public int CurrentAttempt;
        
        public Sprite Background => _background;
        public Sprite NameLevelImage => _nameLevelImage;
        public List<GameObject> Platforms => _platforms;
        public GameObject CoordPlatforms => _coordPlatforms;
        public List<GameObject> Airplanes => _airplanes;
        public int AircraftToOpen => _aircraftToOpen;
        public int AircraftToOpenNextLevel => _aircraftToOpenNextLevel;
        public int LevelOpen => _levelOpen;
        public string NextLevelName => _nextLevelName;
        public int CountLandedAircrafts => _countLandedAircrafts;

        public float DelaySpawnEasy => _delaySpawnEasy;
        public float DelaySpawnMedium => _delaySpawnMedium;
        public float DelaySpawnHard => _delaySpawnHard;
    }
}
