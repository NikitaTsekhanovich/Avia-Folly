using System;
using GameMenu;
using UnityEngine;

namespace Airplanes
{
    public class CollisionWallsAirplanes : MonoBehaviour
    {
        [SerializeField] private Aircraft _aircraft;
        [SerializeField] private SpriteRenderer _warningExplosion;
        [SerializeField] private AudioSource _alarmSound;
        [SerializeField] private bool _onGameField;
        private Transform _transformAirplane;
        private int _airplanesInRadius;

        public static Action OnChangeTimeGame;

        private void Start()
        {
            _transformAirplane = _aircraft.GetComponent<Transform>();
        }
        
        private void OnTriggerEnter2D(Collider2D col)
        {            
            if (col.CompareTag("HorizontalWall"))
                ChangeDirection(180);
            else if (col.CompareTag("VerticalWall"))
                ChangeDirection(360);
                
            if (col.CompareTag("Airplane"))
            {
                _alarmSound.Play();
                Time.timeScale = 0.3f;
                _warningExplosion.enabled = true;
                _airplanesInRadius++;
            }
        }

        private void OnTriggerExit2D(Collider2D col)
        {
            if (col.CompareTag("Airplane"))
            {
                _airplanesInRadius--;

                if (_airplanesInRadius <= 0)
                {
                    OnChangeTimeGame?.Invoke();
                    _warningExplosion.enabled = false;
                }
            }
        }

        private void ChangeDirection(float offset)
        {
            if (_onGameField)
                _transformAirplane.transform.rotation = Quaternion.Euler(0, 0, offset - _transformAirplane.localEulerAngles.z);
            else
                _onGameField = true;
        }
    }
}

