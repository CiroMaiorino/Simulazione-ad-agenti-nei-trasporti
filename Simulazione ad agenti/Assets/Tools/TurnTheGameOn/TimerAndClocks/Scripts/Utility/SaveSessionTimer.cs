namespace TurnTheGameOn.Timer
{
    using UnityEngine;
    public class SaveSessionTimer : MonoBehaviour
    {
        private Timer timer;
        private double lastSessionGameTimer;
        public string saveDataKey = "LastSessionGameTimer";
        public bool showDebugLogs = true;

        #region Get Data Triggers
        private void Awake()
        {
            timer = GetComponent<Timer>();
            if (timer.timerType == TimerType.CountDown)
            {
                lastSessionGameTimer = PlayerPrefsGetDouble(saveDataKey, (double)timer.startTime);
                timer.startTime = lastSessionGameTimer;
                if (showDebugLogs)
                {
                    Debug.Log("Setting Timer.gameTime from saved PlayerPrefs data: " + "(" + saveDataKey + ") " + lastSessionGameTimer);
                }
            }
            else if (timer.timerType == TimerType.CountUp || timer.timerType == TimerType.CountUpInfinite)
            {
                lastSessionGameTimer = PlayerPrefsGetDouble(saveDataKey, (double)timer.startTime);
                timer.startTime = lastSessionGameTimer;
                if (showDebugLogs)
                {
                    Debug.Log("Setting Timer.gameTime from saved PlayerPrefs data: " + "(" + saveDataKey + ") " + lastSessionGameTimer);
                }
            }
        }

        public static double PlayerPrefsGetDouble(string key, double defaultValue)
        {
            string defaultVal = DoubleToString(defaultValue);
            return StringToDouble(PlayerPrefs.GetString(key, defaultVal));
        }
        #endregion

        #region Save Data Triggers
        private void OnApplicationQuit()
        {
            if (Application.IsPlaying(timer))
            {
                PlayerPrefsSetDouble(saveDataKey, timer.GetTimerValue());
                if (showDebugLogs)
                {
                    Debug.Log("Saving Timer.gameTime in PlayerPrefs: " + "(" + saveDataKey + ")" + timer.GetTimerValue());
                }
            }
        }

        private void OnDestroy()
        {
            if (Application.IsPlaying(timer))
            {
                PlayerPrefsSetDouble(saveDataKey, timer.GetTimerValue());
                if (showDebugLogs)
                {
                    Debug.Log("Saving Timer.gameTime in PlayerPrefs: " + "(" + saveDataKey + ")" + timer.GetTimerValue());
                }
            }
        }

        public static void PlayerPrefsSetDouble(string key, double value)
        {
            PlayerPrefs.SetString(key, DoubleToString(value));
        }
        #endregion

        #region String-Double Conversion Helpers
        private static string DoubleToString(double target)
        {
            return target.ToString("R");
        }

        private static double StringToDouble(string target)
        {
            if (string.IsNullOrEmpty(target))
                return 0d;

            return double.Parse(target);
        }
        #endregion

        public void DeleteKeyValue()
        {
            PlayerPrefs.DeleteKey(saveDataKey);
        }
    }
}