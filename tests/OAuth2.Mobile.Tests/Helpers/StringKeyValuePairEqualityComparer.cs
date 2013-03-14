namespace StudioDonder.OAuth2.Mobile.Tests.Helpers
{
    using System.Collections.Generic;

    internal class StringKeyValuePairEqualityComparer : IEqualityComparer<KeyValuePair<string, string>>
    {
        public bool Equals(KeyValuePair<string, string> x, KeyValuePair<string, string> y)
        {
            return string.Equals(x.Key, y.Key) && string.Equals(x.Value, y.Value);
        }

        public int GetHashCode(KeyValuePair<string, string> obj)
        {
            unchecked
            {
                var hash = 17;
                hash = hash * 23 + obj.Key.GetHashCode();
                hash = hash * 23 + obj.Value.GetHashCode();

                return hash;
            }
        }
    }
}