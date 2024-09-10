using System;
using Airplanes;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameMenu
{
    public class PauseMenuHandler : MonoBehaviour
    {
        [SerializeField] private GameObject _airplanes;
        [SerializeField] private TMP_Text _currentSpeedText;
        private float _currentSpeedGame = 1f;

        public static Action OnRestartScore;
        public static Action OnSetAttempt;

        private void OnEnable()
        {
            CollisionWallsAirplanes.OnChangeTimeGame += ChangeTimeGame;
        }

        private void OnDisable()
        {
            CollisionWallsAirplanes.OnChangeTimeGame -= ChangeTimeGame;
        }

        public void PauseGame()
        {
            Time.timeScale = 0f;
        }

        public void ContinueGame()
        {
            Time.timeScale = _currentSpeedGame;
        }

        public void RestartGame()
        {
            foreach (var airplane in _airplanes.GetComponentsInChildren<Airplane>())
                Destroy(airplane.gameObject);

            OnRestartScore?.Invoke();
            Time.timeScale = _currentSpeedGame;
        }

        public void ReturnToMenu()
        {
            OnSetAttempt?.Invoke();
            Time.timeScale = 1f;
            LoadingScreenController.instance.StartAnimationFade();
            SceneManager.LoadScene("Menu");
        }

        public void SpeedUpGame()
        {
            _currentSpeedGame++;

            if (_currentSpeedGame > 3)
                _currentSpeedGame = 1;

            _currentSpeedText.text = $"x {_currentSpeedGame}";
            Time.timeScale = _currentSpeedGame;
        }

        public void ChangeTimeGame()
        {
            Time.timeScale = _currentSpeedGame;
        }
    }
}

