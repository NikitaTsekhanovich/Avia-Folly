using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

namespace Airplanes
{
    public abstract class Aircraft : MonoBehaviour
    {
        [SerializeField] private float _speed;
        [SerializeField] private int _coin;
        [SerializeField] private Transform _airplaneNose;
        [SerializeField] private Line _linePrefab;
        [SerializeField] private TypesAirplaneColors _colorType;
        [SerializeField] private AircraftTypes _aircraftType;
        private Queue<Vector2> _positionsInLine = new();
        private Queue<Vector2> _tempPositionsInLine = new();
        private Coroutine _currentCoroutine;
        private Line _line;
        private bool _isSpecifiedMovement;
        private bool _isReadyToLand;
        private DG.Tweening.Sequence _animLand;

        public bool IsReadyToLand => _isReadyToLand;
        public TypesAirplaneColors ColorType => _colorType;
        public AircraftTypes AircraftType => _aircraftType;

        public static Action<int> OnIncreasesScore;

        private void Update()
        {
            if (!_isSpecifiedMovement && !_isReadyToLand)
                transform.position = Vector2.Lerp(transform.position, _airplaneNose.transform.position, Time.deltaTime  * _speed);
        }

        public void ReadyToLand()
        {
            _isReadyToLand = true;
        }

        public void NotReadyToLand()
        {
            _isReadyToLand = false;
        }

        public void ChangeFlightPath(Vector3 mousePos)
        {
            if (_line != null)
            {
                _positionsInLine?.Clear();
                Destroy(_line.gameObject);
                StopCoroutine(_currentCoroutine);
            }
                
            _line = Instantiate(_linePrefab, mousePos, Quaternion.identity, transform);
        }

        public void DrawFlightPath(Vector3 mousePos)
        {
            if (_line != null)
            {
                _line.SetPosition(mousePos);
                _isSpecifiedMovement = true;
            } 
        }

        public void StopDrawFlightPath()
        {
            _positionsInLine = new Queue<Vector2>(_line.GetPointsLine());
            _isSpecifiedMovement = false;
            _tempPositionsInLine = new Queue<Vector2>(_positionsInLine);
            _currentCoroutine = StartCoroutine(FollowPath());
        }

        private IEnumerator FollowPath()
        {
            foreach (var point in _tempPositionsInLine)
            {
                while (Vector3.Distance(transform.position, point) > 0.5f)
                {
                    _airplaneNose.transform.position = point;
                    transform.position = Vector3.Lerp(transform.position, _airplaneNose.transform.position, Time.deltaTime * _speed);

                    var direction = _airplaneNose.position - transform.position;
                    var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                    transform.rotation = Quaternion.AngleAxis(angle - 90f, Vector3.forward);

                    yield return null;
                }
                _positionsInLine.Dequeue();
                _line.RemovePosition();
            }
            Destroy(_line?.gameObject);
        }

        public void DoLand(Transform landingPoint)
        {
            if (_currentCoroutine != null) StopCoroutine(_currentCoroutine);
            if (_line != null) Destroy(_line.gameObject);
            if (transform == null) return;

            transform.DOMove(landingPoint.position, 1f);
            transform.rotation = Quaternion.Euler(0, 0, landingPoint.eulerAngles.z);

            _animLand = DOTween.Sequence()
                .Append(transform.DOScale(new Vector3(0, 0, 1f), 1f))
                .AppendCallback(Land);
        }

        private void Land()
        {
            _animLand?.Kill();
            DoDestroy();
            OnIncreasesScore?.Invoke(_coin);
        }

        public abstract void DoDestroy();
    }
}

