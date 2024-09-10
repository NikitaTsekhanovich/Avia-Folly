using UnityEngine;

namespace PlayerData
{
    public static class PlayerScore
    {
        private static string _keyCountAirplanes = "CountAirplanes";
        public static int CurrentLandingAirplanes => PlayerPrefs.GetInt(_keyCountAirplanes);

        public static void IncreaseNumberAirplanes(int airplanes)
        {
            PlayerPrefs.SetInt(_keyCountAirplanes, CurrentLandingAirplanes + airplanes);
        }

        public static void SetCurrentAttempt(string levelName, int numberAttempt, int airplanes)
        {
            Debug.Log($"Set {levelName}{numberAttempt}");
            PlayerPrefs.SetInt($"{levelName}{numberAttempt}", airplanes);
        }

        public static int GetResultAttempt(string levelName, int numberAttempt)
        {
            Debug.Log($"Get {levelName}{numberAttempt}");
            return PlayerPrefs.GetInt($"{levelName}{numberAttempt}");;
        }
    }
}

