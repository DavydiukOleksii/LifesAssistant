using System;

namespace DataModel.Config
{
    public class LanguageItem: IEquatable<LanguageItem>
    {
        public string Title { get; set; }
        public string Value { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            LanguageItem objAsLI = obj as LanguageItem;
            if (objAsLI == null) return false;
            else return Equals(objAsLI);
        }

        public bool Equals(LanguageItem other)
        {
            if (other == null) return false;
            return this.Title == other.Title && this.Value == other.Value;
        }

        //public override int GetHashCode()
        //{
        //    return Title.GetHashCode() ^ Value.GetHashCode();
        //}
    }
}
