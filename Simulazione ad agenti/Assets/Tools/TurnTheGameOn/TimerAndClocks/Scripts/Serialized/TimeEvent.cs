namespace TurnTheGameOn.Timer
{
    using UnityEngine;
    using UnityEngine.Events;
    using System;

    [Serializable]
    public class TimeEvent
    {
        [Tooltip("The name of the custom time event.")]
        public string eventName;
        [Tooltip("The time the event will be triggered.")]
        public double eventTime;
        [Tooltip("The event that's invoked when the event time is reached.")]
        public UnityEvent timeEvent;
        [HideInInspector]
        public bool wasTriggered;
    }
}