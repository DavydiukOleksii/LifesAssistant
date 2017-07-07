using System;

namespace DataModel.Calendar
{
    public class OneTask
    {
        public DateTime Time { get; set; }
        public string Descriptions { get; set; }

        public override bool Equals(object obj)
        {
            // If parameter is null return false.
            if (obj == null)
            {
                return false;
            }

            // If parameter cannot be cast to Point return false.
            OneTask t = obj as OneTask;
            if ((System.Object)t == null)
            {
                return false;
            }

            // Return true if the fields match:
            return (Time == t.Time) && (Descriptions == t.Descriptions);
        }

        public OneTask()
        {
            
        }

        //public override int GetHashCode()
        //{
        //    return Time?.GetHashCode() ^ Descriptions?.GetHashCode();
        //}
    }
}
