using UnityEngine;
using UnityEngine.SceneManagement;

namespace Levels
{
    public class LevelDataCarrier : MonoBehaviour
    {
        private LevelData _levelData;
        public static LevelDataCarrier instance;

        private void Awake() 
        {             
            if (instance == null) 
            { 
                instance = this; 
                DontDestroyOnLoad(gameObject);
            } 
            else 
            { 
                Destroy(this);  
            } 
        }

        private void OnEnable()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        private void OnDisable()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }
        
        public void SetLevel(LevelData levelData)
        {
            _levelData = levelData;
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
        {
            if (scene.name == "Game")
            {
                var levelLoader = GameObject.FindWithTag("GameControllers").GetComponent<LevelLoader>();
                levelLoader.SetLevelData(_levelData);
            }
            if (scene.name == "Menu" && LoadingScreenController.instance != null)
            {
                LoadingScreenController.instance.EndAnimationFade();
            }
        }
    }
}

