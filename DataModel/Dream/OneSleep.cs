using System;

namespace DataModel.Dream
{
    public class OneSleep
    {
        public DateTime Time { get; set; }
        public DateTime Duration { get; set; }

        public override bool Equals(object obj)
        {
            // If parameter is null return false.
            if (obj == null)
            {
                return false;
            }

            // If parameter cannot be cast to Point return false.
            OneSleep t = obj as OneSleep;
            if ((Object)t == null)
            {
                return false;
            }

            // Return true if the fields match:
            return (Time == t.Time) && (Duration == t.Duration);
        }

        public int GetDurationInSecond()
        {
            return Duration.Hour*3600 + Duration.Minute*60 + Duration.Second;
        }

        public double GetDurationInHour()
        {
            return GetDurationInSecond()/3600;
        }
    }
}
