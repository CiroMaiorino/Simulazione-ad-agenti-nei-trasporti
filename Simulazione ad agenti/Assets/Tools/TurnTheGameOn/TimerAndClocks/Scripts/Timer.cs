namespace TurnTheGameOn.Timer
{
    using TMPro;
    using UnityEngine;
    using UnityEngine.UI;
    using UnityEngine.Events;
    using System;

    public class Timer : MonoBehaviour
    {
        #region Variables
        [Tooltip("Controls if the timer is Counting or Disabled.")]
        public TimerState timerState;
        [Tooltip("Controls if the timer will count-up, count-down, or count-up infinitely.")]
        public TimerType timerType;
        [Tooltip("Time the timer will start from.")]
        public double startTime;
        [Tooltip("Time the timer will stop at.")]
        public double finishTime;
        [Tooltip("Rate at which the timer adds or subtracts time.")]
        [Range(0, 10000)]
        public float timerSpeed = 1;
        [Tooltip("Timer will auto restart when complete.")]
        public bool loop;
        [Tooltip("Timer will run as a clock and output system time.")]
        public bool useSystemTime;
        [Tooltip("Create a list of custom events, the timer will trigger when the assigned time is reached.")]
        public TimeEvent[] timeEvents;
        [Tooltip("A UnityEvent that can trigger custom scripting functions when time is up.")]
        public UnityEvent timesUpEvent;
        [Tooltip("Sets Time.timescale to 0 when time is up.")]
        public bool setZeroTimescale;
        [Tooltip("Assign a default Unity UI Text or Text Mesh Pro UGUI component.")]
        public TimerTextType textType;
        [Tooltip("Assign a UI Text component to output a formatted string.")]
        public Text timerTextDefault;
        [Tooltip("Assign a Text Mesh Pro UGUI component to output a formatted string.")]
        public TextMeshProUGUI timerTextTMPUGUI;
        [Tooltip("Toggle specific time value fields in the output string.")]
        public DisplayOptions displayOptions;
        [Tooltip("Toggle the leading zero on specific time value fields in the output string.")]
        public LeadingZero leadingZero;
        
        public int day;
        public int hour;
        public int minute;
        public int second;
        public int millisecond;
        public string formattedString;
        private double gameTime;
        private string ms, s, m, d, h;
        #endregion

        #region Primary Timer Logic
        void Start()
        {
            ClampGameTime();
            gameTime = startTime;
            UpdateUIText();
        }

        void Update()
        {
            if (timerState == TimerState.Counting)
            {
                if (timerType == TimerType.CountUp)
                {
                    gameTime += 1 * (double)Time.deltaTime * (double)timerSpeed;
                    CountUpTimeEvents();
                    if (gameTime >= finishTime) TimeIsUpEvent();
                }
                else if (timerType == TimerType.CountDown)
                {
                    gameTime -= 1 * (double)Time.deltaTime * (double)timerSpeed;
                    CountDownTimeEvents();
                    if (gameTime <= 0) TimeIsUpEvent();
                }
                else if (timerType == TimerType.CountUpInfinite)
                {
                    if (useSystemTime) SetSystemTime();
                    else gameTime += 1 * (double)Time.deltaTime * (double)timerSpeed;
                    CountUpTimeEvents();
                }
            }
            UpdateUIText();
        }

        void SetSystemTime()
        {
            gameTime = ((double)DateTime.Now.Hour + ((double)DateTime.Now.Minute) + (double)DateTime.Now.Second);
            second = (int)DateTime.Now.Second;
            minute = (int)DateTime.Now.Minute;
            hour = (int)DateTime.Now.Hour;
        }

        void TimeIsUpEvent()
        {
            StopTimer();
            timesUpEvent.Invoke();
            if (loop)
            {
                RestartTimer();
            }
        }

        void CountUpTimeEvents()
        {
            for (int i = 0; i < timeEvents.Length; i++)
            {
                if (timeEvents[i].wasTriggered == false)
                {
                    if (gameTime >= timeEvents[i].eventTime)
                    {
                        timeEvents[i].wasTriggered = true;
                        timeEvents[i].timeEvent.Invoke();
                    }
                }
            }
        }

        void CountDownTimeEvents()
        {
            for (int i = 0; i < timeEvents.Length; i++)
            {
                if (timeEvents[i].wasTriggered == false)
                {
                    if (gameTime <= timeEvents[i].eventTime)
                    {
                        timeEvents[i].wasTriggered = true;
                        timeEvents[i].timeEvent.Invoke();
                    }
                }
            }
        }

        string FormatSeconds(double elapsed)
        {
            SetDisplayOptions();
            if (timerType == TimerType.CountUpInfinite)
            {
                if (useSystemTime)
                {
                    gameTime = ((double)DateTime.Now.Hour + ((double)DateTime.Now.Minute) + (double)DateTime.Now.Second);
                    millisecond = (int)DateTime.Now.Millisecond;
                    second = (int)DateTime.Now.Second;
                    minute = (int)DateTime.Now.Minute;
                    hour = (int)DateTime.Now.Hour;
                    day = (int)DateTime.Now.DayOfYear;
                    return String.Format(d + h + m + s + ms, day, hour, minute, second, millisecond);
                }
                else
                {
                    TimeSpan t = TimeSpan.FromSeconds(elapsed);
                    day = t.Days;
                    hour = t.Hours;
                    minute = t.Minutes;
                    second = t.Seconds;
                    millisecond = t.Milliseconds;
                    return String.Format(d + h + m + s + ms, t.Days, t.Hours, t.Minutes, t.Seconds, t.Milliseconds);
                }
            }
            else
            {
                TimeSpan t = TimeSpan.FromSeconds(elapsed);
                day = t.Days;
                hour = t.Hours;
                minute = t.Minutes;
                second = t.Seconds;
                millisecond = t.Milliseconds;
                if (!leadingZero.seconds && t.Seconds < 10d && s == "{3:D2}") s = "{3:D1}";
                if (!leadingZero.minutes && t.Minutes < 10d && m == "{2:D2}:") m = "{2:D1}:";
                if (!leadingZero.hours && t.Hours < 10d && h == "{1:D2}:") h = "{1:D1}:";
                if (!leadingZero.days && t.Days < 10d && (d == "{0:D3}:" || d == "{0:D2}:")) d = "{0:D1}:";
                else if (!leadingZero.days && t.Days < 100d && (d == "{0:D3}:" || d == "{0:D1}:")) d = "{0:D2}:";
                return String.Format(d + h + m + s + ms, t.Days, t.Hours, t.Minutes, t.Seconds, t.Milliseconds);
            }
        }

        void SetDisplayOptions()
        {
            ms = displayOptions.milliseconds ? ".{4:D3}" : "";
            s = displayOptions.seconds ? "{3:D2}" : "";
            m = displayOptions.minutes ? "{2:D2}:" : "";
            h = displayOptions.hours ? "{1:D2}:" : "";
            if (timerType == TimerType.CountUpInfinite) d = displayOptions.days ? "{0:D3}:" : "";
            else d = displayOptions.days ? "{0:D0}:" : "";
        }
        #endregion

        #region Public Timer Methods
        /// <summary>
        /// Adds the value to the timer value, use a negative value to subtract time.
        /// </summary>
        /// <param name="value"></param>
        public void AddTime(double value)
        {
            gameTime += value;
        }

        /// <summary>
        /// Clamps the gameTime value to ensure it does not exceed the max double value of 922337193600.
        /// </summary>
        public void ClampGameTime()
        {
            if (startTime > 922337193600)
            {
                startTime = 922337193600;
                Debug.LogWarning("startTime exceeds max allowed value, it has been clamped to max value");
            }
            if (finishTime > 922337193600)
            {
                finishTime = 922337193600;
                Debug.LogWarning("finishTime exceeds max allowed value, it has been clamped to max value");
            }
            if (gameTime > 922337193600)
            {
                gameTime = 922337193600;
                Debug.LogWarning("gameTime exceeds max allowed value, it has been clamped to max value");
            }
        }

        /// <summary>
        /// Returns the timer value as a double.
        /// </summary>
        /// <returns></returns>
        public double GetTimerValue()
        {
            return gameTime;
        }

        /// <summary>
        /// Updates the assigned text component to display the current formatted time string.
        /// </summary>
        public void UpdateUIText()
        {
            formattedString = FormatSeconds(gameTime);
            switch (textType)
            {
                case TimerTextType.DefaultText:
                    if (timerTextDefault != null) timerTextDefault.text = formattedString;
                    break;
                case TimerTextType.TextMeshProUGUI:
                    if (timerTextTMPUGUI != null) timerTextTMPUGUI.text = formattedString;
                    break;
            }
        }

        /// <summary>
        /// Set gameTime to the value.
        /// </summary>
        public void SetTimerValue(double _gameTime)
        {
            gameTime = _gameTime;
        }

        /// <summary>
        /// Sets the timer state to counting.
        /// </summary>
        public void StartTimer()
        {
            timerState = TimerState.Counting;
        }

        /// <summary>
        /// Stops the timer.
        /// </summary>
        public void StopTimer()
        {
            timerState = TimerState.Disabled;
            if (setZeroTimescale) Time.timeScale = 0;
            if (gameTime < 0.0f) gameTime = 0.0f;
            UpdateUIText();
        }

        /// <summary>
        /// Sets the timer value to start time.
        /// </summary>
        public void ResetTimer()
        {
            if (timerType == TimerType.CountDown) gameTime = startTime;
            else gameTime = startTime;
            UpdateUIText();
            for (int i = 0; i < timeEvents.Length; i++)
            {
                timeEvents[i].wasTriggered = false;
            }
        }

        /// <summary>
        /// Sets the timer value to start time, and starts the timer if it's not counting.
        /// </summary>
        public void RestartTimer()
        {
            ResetTimer();
            UpdateUIText();
            StartTimer();
        }
        #endregion
    }
}