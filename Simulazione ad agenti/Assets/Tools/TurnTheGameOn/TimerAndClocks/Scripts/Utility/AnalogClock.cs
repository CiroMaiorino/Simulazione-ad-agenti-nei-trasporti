namespace TurnTheGameOn.Timer
{
    using UnityEngine;
    using System;
    public class AnalogClock : MonoBehaviour
    {
        public bool useSystemTime = true;
        [Range(-24, 24)] public int systemTimeHourOffset;
        public Timer timer;
        public Transform secondHandPivot;
        public Transform minuteHandPivot;
        public Transform hourHandPivot;
        private float secondhandRotation;
        private float minuteRotation;
        private float hourRotation;
        private int second;
        private int minute;
        private int hour;
        private DateTime currentDateTime;

        void Update()
        {
            if (useSystemTime)
            {
                currentDateTime = DateTime.Now;
                currentDateTime = currentDateTime.AddHours(systemTimeHourOffset);
                second = currentDateTime.Second;
                minute = currentDateTime.Minute;
                hour = currentDateTime.Hour;
            }
            else
            {
                if (timer)
                {
                    second = timer.second;
                    minute = timer.minute;
                    hour = timer.hour;
                }
                else
                {
                    Debug.LogWarning("This clock is not set to use system time, please assign a timer to get values from.");
                }
            }
            secondhandRotation = second * 6f;
            minuteRotation = minute * 6f;
            hourRotation = (hour * 30) + (minuteRotation / 12);
            secondHandPivot.localRotation = Quaternion.Euler(0, 0, -secondhandRotation);
            minuteHandPivot.localRotation = Quaternion.Euler(0, 0, -minuteRotation);
            hourHandPivot.localRotation = Quaternion.Euler(0, 0, -hourRotation);
        }

    }
}