using System;
using System.Collections;
using Airplanes;
using DG.Tweening;
using UnityEngine;

namespace LevelFeatures
{
    public class Education : MonoBehaviour
    {
        [SerializeField] private Transform _pointSpawn;
        [SerializeField] private GameObject _airplane;
        [SerializeField] private Transform _parentComponent;
        [SerializeField] private Transform _iconClick;
        [SerializeField] private Transform _iconPoint;
        [SerializeField] private GameObject _mainEducation;

        public static Action OnEndEducation;

        private void OnEnable()
        {
            MainEducation.OnStartEndEducation += SpawnAirplane;
        }

        private void OnDisable()
        {
            MainEducation.OnStartEndEducation -= SpawnAirplane;
        }

        public void StartEducation()
        {
            var statusEducation = PlayerPrefs.GetString("Education");
            if (statusEducation == "true")
                SpawnAirplane();
            else
                _mainEducation.SetActive(true);
        }

        private GameObject GetSpawnAirplane()
        {
            return Instantiate(
                _airplane, 
                _pointSpawn.transform.position, 
                _pointSpawn.rotation,
                _parentComponent);
        }

        private void ShowLand(GameObject airplane)
        {
            airplane.GetComponent<Airplane>().ReadyToLand();
            _iconClick.gameObject.SetActive(true);

            var animIcon = DOTween.Sequence()
                .Append(_iconClick.DOMove(_iconPoint.position, 1f))
                .AppendInterval(0.4f)
                .SetLoops(-1);

            StartCoroutine(CheckStateAirplane(airplane, animIcon));
        }      

        private IEnumerator CheckStateAirplane(GameObject airplane, Tween animIcon)
        {
            while (airplane != null)
            {
                yield return null;
            }
            _iconClick.gameObject.SetActive(false);
            animIcon.Kill();
            EndEducation();
        } 

        public void SpawnAirplane()
        {
            var airplane = GetSpawnAirplane();
            ShowLand(airplane);
        }

        private void EndEducation()
        {
            OnEndEducation?.Invoke();
        }
    }
}

