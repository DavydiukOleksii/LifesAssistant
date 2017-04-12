using System;

namespace DataModel.Water
{
    public class OnceDrink
    {
        public double Capasity { get; set; }
        public DateTime time { get; set; }

        public override bool Equals(object obj)
        {
            // If parameter is null return false.
            if (obj == null)
            {
                return false;
            }

            // If parameter cannot be cast to Point return false.
            OnceDrink t = obj as OnceDrink;
            if ((Object)t == null)
            {
                return false;
            }

            // Return true if the fields match:
            return (Capasity == t.Capasity) && (time == t.time);
        }
    }
}
