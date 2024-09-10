using DG.Tweening;
using Airplanes;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Platforms
{
    public class Platform : MonoBehaviour
    {
        [SerializeField] private TypesAirplaneColors _colorType;
        [SerializeField] private AircraftTypes _aircraftType;
        [SerializeField] private Transform _landingPoint;
        [SerializeField] private SpriteRenderer _landingLight;
        private HashSet<Aircraft> _registrationAircrafts = new();

        public TypesAirplaneColors ColorType => _colorType;
        public AircraftTypes AircraftType => _aircraftType;

        private void Start()
        {
            DOTween.Sequence()
                .Append(_landingLight.DOFade(1f, 0f))
                .AppendInterval(0.5f)
                .Append(_landingLight.DOFade(0f, 1f))
                .SetLoops(-1);
        }

        private void OnTriggerEnter2D(Collider2D col)
        {           
            CheckLand(col);
        }

        private void CheckLand(Collider2D col)
        {
            if (col.CompareTag("Airplane") && 
                col.TryGetComponent(out Aircraft aircraft) && 
                aircraft.ColorType == _colorType &&
                aircraft.AircraftType == _aircraftType)
            {
                _registrationAircrafts.Add(aircraft);
                StartCoroutine(RegistrationAircrafts());
            }
        }

        private void OnTriggerExit2D(Collider2D col)
        {     
            if (col.CompareTag("Airplane") && 
                col.TryGetComponent(out Aircraft aircraft))  
            {
                if (_registrationAircrafts.Contains(aircraft))
                    _registrationAircrafts.Remove(aircraft);
            }    
        }

        private IEnumerator RegistrationAircrafts()
        {
            while (_registrationAircrafts.Count > 0)
            {
                foreach (var aircraft in _registrationAircrafts.ToList())
                {
                    if (aircraft.IsReadyToLand)
                    {
                        aircraft.DoLand(_landingPoint);
                        _registrationAircrafts.Remove(aircraft);
                    }
                }
                
                yield return null;
            }
        }
    }
}

