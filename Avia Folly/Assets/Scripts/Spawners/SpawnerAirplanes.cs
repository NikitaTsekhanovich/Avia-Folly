using System.Collections;
using System.Collections.Generic;
using Airplanes;
using UnityEngine;

namespace Spawners
{
    public class SpawnerAirplanes : MonoBehaviour
    {
        [SerializeField] private Transform _parentComponent;
        [SerializeField] private List<Transform> _points = new();
        [SerializeField] private AudioSource _airplaneSpawnSound;
        [SerializeField] private AudioSource _helicopterSpawnSound;
        private List<GameObject> _airplanes = new();
        private float _delay = 5f; 
        private int _countAircrafts;

        public void SetAirplanes(List<GameObject> airplanes, float delay)
        {
            _delay = delay;
            _airplanes = airplanes;
            StartCoroutine(SpawnAirplanes());
        }

        private IEnumerator SpawnAirplanes()
        {
            while(true)
            {
                var randomPoint = Random.Range(0, _points.Count);
                var randomAirplane = Random.Range(0, _airplanes.Count);

                var aircraft = Instantiate(
                    _airplanes[randomAirplane], 
                    _points[randomPoint].transform.position,
                    _points[randomPoint].rotation,
                    _parentComponent);

                aircraft.name = $"{_countAircrafts}";

                if (aircraft.GetComponent<Airplane>() != null)
                    _airplaneSpawnSound.Play();
                else if (aircraft.GetComponent<Helicopter>() != null)
                    _helicopterSpawnSound.Play(); 

                _countAircrafts++;

                yield return new WaitForSeconds(_delay);
            }
        }
    }
}

