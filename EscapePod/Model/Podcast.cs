using System;

namespace EscapePod.Model
{
    [Serializable]
    public class Podcast
    {
        public string Url { get; set; }

        public string Title { get; set; }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((Podcast) obj);
        }

        protected bool Equals(Podcast other)
        {
            return string.Equals(Url, other.Url) && string.Equals(Title, other.Title);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((Url != null ? Url.GetHashCode() : 0) * 397) ^ (Title != null ? Title.GetHashCode() : 0);
            }
        }
    }
}
