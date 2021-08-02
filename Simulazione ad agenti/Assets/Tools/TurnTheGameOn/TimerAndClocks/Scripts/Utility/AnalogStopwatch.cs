namespace TurnTheGameOn.Timer
{
    using UnityEngine;

    public class AnalogStopwatch : MonoBehaviour
    {
        public Timer timer;
        public Transform secondHandPivot;
        public Transform minuteHandPivot;
        private float secondhandRotation;
        private float minuteRotation;
        private int second;
        private int minute;

        void Update()
        {
            if (timer)
            {
                second = (int)timer.second;
                minute = (int)timer.minute;
            }
            else
            {
                Debug.LogWarning("Please assign a timer to get values from.");
            }
            secondhandRotation = second * 6f;
            minuteRotation = minute * 6f;
            secondHandPivot.localRotation = Quaternion.Euler(0, 0, -secondhandRotation);
            if (minuteHandPivot != null) minuteHandPivot.localRotation = Quaternion.Euler(0, 0, -minuteRotation);
        }

    }
}