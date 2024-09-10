using System.Collections.Generic;
using Levels;
using PlayerData;
using UnityEngine;
using UnityEngine.UI;

namespace Menu
{
    public class Stats : MonoBehaviour
    {
        [SerializeField] private List<LevelData> _levelsData = new();
        [SerializeField] private LineRenderer _render;
        [SerializeField] private Transform _zeroPoint;
        [SerializeField] private List<float> _pointsX = new();
        [SerializeField] private List<Transform> _points = new();
        [SerializeField] private GameObject _aircraft;
        [SerializeField] private Transform _parentComponent;
        [SerializeField] private GameObject _previousButton;
        [SerializeField] private GameObject _nextButton;
        [SerializeField] private Image _title;

        private const float BottomLine = -3f;
        private const float UpperLine = 3f;

        private int _index;
        private GameObject _newAircraft;

        private void Start()
        {
            CreateGraph();
        }

        private void CreateGraph()
        {
            _render.positionCount++;
            _render.SetPosition(
                _render.positionCount - 1, 
                new Vector3(_zeroPoint.transform.position.x, _zeroPoint.transform.position.y, 0));

            var pointY = _zeroPoint.transform.position.y;
            
            var numberLastAttempt = _levelsData[_index].CurrentAttempt;
            Debug.Log(numberLastAttempt);

            int newI;
            if (numberLastAttempt > 10)
                newI = numberLastAttempt - 10;
            else
                newI = 0;
            
            for (var i = newI; i < numberLastAttempt; i++)
            {
                var resultAttempt = PlayerScore.GetResultAttempt(_levelsData[_index].name, i);

                pointY = resultAttempt * 0.05f + BottomLine;

                if (pointY >= UpperLine) pointY = UpperLine;

                _render.positionCount++;
                _render.SetPosition(
                    _render.positionCount - 1, new Vector2(_points[_render.positionCount - 2].transform.position.x,
                    pointY));
            }

            // foreach (var levelData in _levelsData)
            // {
            //     if (levelData.CountLandedAircrafts == 0) break;

            //     pointY = levelData.CountLandedAircrafts * 0.05f + BottomLine;

            //     if (pointY >= UpperLine) pointY = UpperLine;

            //     _render.positionCount++;
            //     index++;
            //     _render.SetPosition(
            //         _render.positionCount - 1, new Vector2(_pointsX[_render.positionCount - 2],
            //         pointY));
            // }

            var positionSpawn = new Vector3(_zeroPoint.transform.position.x, _zeroPoint.transform.position.y, 0);

            if (_render.positionCount - 2 >= 0) positionSpawn = new Vector3(_points[_render.positionCount - 2].transform.position.x, pointY, 0);

            _newAircraft = Instantiate(
                _aircraft, 
                positionSpawn, 
                Quaternion.identity, _parentComponent);

            _newAircraft.transform.localPosition = new Vector3(_newAircraft.transform.localPosition.x + 40f, _newAircraft.transform.localPosition.y + 40f, 0);
        }

        public void SwitchNextStats()
        {
            if (_index < _levelsData.Count - 1)
            {
                _index++;
                _previousButton.SetActive(true);

                if (_index >= _levelsData.Count - 1)
                    _nextButton.SetActive(false);
            }

            SwitchStats();
        }

        public void SwitchPreviousStats()
        {
            if (_index > 0)
            {
                _index--;
                _nextButton.SetActive(true);

                if (_index <= 0)
                    _previousButton.SetActive(false);
            }

            SwitchStats();
        }

        private void SwitchStats()
        {
            _title.sprite = _levelsData[_index].NameLevelImage;
            _render.positionCount = 0;
            Destroy(_newAircraft);
            CreateGraph();
        }
    }
}

