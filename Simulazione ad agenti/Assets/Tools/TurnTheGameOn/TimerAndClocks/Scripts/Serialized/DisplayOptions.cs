namespace TurnTheGameOn.Timer
{
    using System;

    [Serializable]
    public class DisplayOptions
    {
        public bool milliseconds = true,
            seconds = true,
            minutes = true,
            hours = true,
            days = true;
    }
}