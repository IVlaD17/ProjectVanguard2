using System;

namespace ProjectVanguard.Models.Entities
{
    public class Time
    {
        private int secondsInAMinute = 60;
        private int secondsInAnHour = 3600;
        private int secondsInADay = 86400;
        private int secondsInAMonth = 2628000;
        private int secondsInAYear = 31536000;

        public float Seconds { get; private set; }

        public Time(float seconds)
        {
            if (seconds > 0)
                Seconds = seconds;
            else
                throw new ArgumentOutOfRangeException();
        }

        /// <summary>
        /// Takes only a positive value. Increases the amount of seconds by the value passed.
        /// </summary>
        /// <param name="value"></param>
        public void IncreaseTime(float value)
        {
            if (value >= 0)
                Seconds += value;
        }
        /// <summary>
        /// Takes only a positive value. Decreases the amount of seconds by the value passed.
        /// </summary>
        /// <param name="value"></param>
        public void DecreaseTime(float value)
        {
            if (value >= 0)
                Seconds -= value;
        }

        public override string ToString()
        {
            string toString;
            // If the stored seconds amount to more than a year
            if (Seconds >= secondsInAYear)
            {
                string years = ((int)(Seconds / secondsInAYear)).ToString();
                if (years.Length == 1)
                    years = $"0{years}";

                string months = ((int)(Seconds % secondsInAYear / secondsInAMonth)).ToString();
                if (months.Length == 1)
                    months = $"0{months}";

                string days = ((int)(Seconds % secondsInAMonth / secondsInADay)).ToString();
                if (days.Length == 1)
                    days = $"0{days}";

                string hours = ((int)(Seconds % secondsInADay / secondsInAnHour)).ToString();
                if (hours.Length == 1)
                    hours = $"0{hours}";

                string minutes = ((int)(Seconds % secondsInAnHour / secondsInAMinute)).ToString();
                if (minutes.Length == 1)
                    minutes = $"0{minutes}";

                string seconds = ((int)(Seconds % secondsInAMinute)).ToString();
                if (seconds.Length == 1)
                    seconds = $"0{seconds}";

                toString = $"{years}/{months}/{days} - {hours}:{minutes}:{seconds}";
            }
            // If the stored seconds amount to more than a month
            else if (Seconds >= secondsInAMonth)
            {
                string months = ((int)(Seconds % secondsInAYear / secondsInAMonth)).ToString();
                if (months.Length == 1)
                    months = $"0{months}";

                string days = ((int)(Seconds % secondsInAMonth / secondsInADay)).ToString();
                if (days.Length == 1)
                    days = $"0{days}";

                string hours = ((int)(Seconds % secondsInADay / secondsInAnHour)).ToString();
                if (hours.Length == 1)
                    hours = $"0{hours}";

                string minutes = ((int)(Seconds % secondsInAnHour / secondsInAMinute)).ToString();
                if (minutes.Length == 1)
                    minutes = $"0{minutes}";

                string seconds = ((int)(Seconds % secondsInAMinute)).ToString();
                if (seconds.Length == 1)
                    seconds = $"0{seconds}";

                toString = $"{months}/{days} - {hours}:{minutes}:{seconds}";
            }
            // If the stored seconds amount to more than a day
            else if (Seconds >= secondsInADay)
            {
                string days = ((int)(Seconds % secondsInAMonth / secondsInADay)).ToString();
                if (days.Length == 1)
                    days = $"0{days}";

                string hours = ((int)(Seconds % secondsInADay / secondsInAnHour)).ToString();
                if (hours.Length == 1)
                    hours = $"0{hours}";

                string minutes = ((int)(Seconds % secondsInAnHour / secondsInAMinute)).ToString();
                if (minutes.Length == 1)
                    minutes = $"0{minutes}";

                string seconds = ((int)(Seconds % secondsInAMinute)).ToString();
                if (seconds.Length == 1)
                    seconds = $"0{seconds}";

                toString = $"{days} - {hours}:{minutes}:{seconds}";
            }
            // If the stored seconds amount to more than an hour
            else if (Seconds >= secondsInAnHour)
            {
                string hours = ((int)(Seconds % secondsInADay / secondsInAnHour)).ToString();
                if (hours.Length == 1)
                    hours = $"0{hours}";

                string minutes = ((int)(Seconds % secondsInAnHour / secondsInAMinute)).ToString();
                if (minutes.Length == 1)
                    minutes = $"0{minutes}";

                string seconds = ((int)(Seconds % secondsInAMinute)).ToString();
                if (seconds.Length == 1)
                    seconds = $"0{seconds}";

                toString = $"{hours}:{minutes}:{seconds}";
            }
            // If the stored seconds amount to more than a minute
            else if (Seconds >= secondsInAMinute)
            {
                string minutes = ((int)(Seconds % secondsInAnHour / secondsInAMinute)).ToString();
                if (minutes.Length == 1)
                    minutes = $"0{minutes}";

                string seconds = ((int)(Seconds % secondsInAMinute)).ToString();
                if (seconds.Length == 1)
                    seconds = $"0{seconds}";

                toString = $"{minutes}:{seconds}";
            }
            else
            {
                string seconds = ((int)(Seconds % secondsInAMinute)).ToString();
                if (seconds.Length == 1)
                    seconds = $"0{seconds}";

                toString = seconds;
            }

            return toString;
        }
    }
}
