namespace DataModel.Credit
{
    public class OneCashTransaction
    {
        public int Money { get; set; }
        public string Article { get; set; }

        public override bool Equals(object obj)
        {
            // If parameter is null return false.
            if (obj == null)
            {
                return false;
            }

            // If parameter cannot be cast to Point return false.
            OneCashTransaction t = obj as OneCashTransaction;
            if ((System.Object)t == null)
            {
                return false;
            }

            // Return true if the fields match:
            return (Money == t.Money) && (Article == t.Article);
        }

        //public override int GetHashCode()
        //{
        //    return Money.GetHashCode() ^ Article.GetHashCode();
        //}
    }
}
