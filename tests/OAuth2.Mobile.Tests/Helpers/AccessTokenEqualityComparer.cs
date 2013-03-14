namespace StudioDonder.OAuth2.Mobile.Tests.Helpers
{
    using System.Collections.Generic;

    internal class AccessTokenEqualityComparer : IEqualityComparer<AccessToken>
    {
        public bool Equals(AccessToken x, AccessToken y)
        {
            if (x == null && y == null)
            {
                return true;
            }

            if (x == null || y == null)
            {
                return false;
            }

            return string.Equals(x.Token, y.Token) && string.Equals(x.TokenType, y.TokenType) &&
                   string.Equals(x.Scope, y.Scope) && string.Equals(x.RefreshToken, y.RefreshToken) && x.ExpirationDate == y.ExpirationDate;
        }

        public int GetHashCode(AccessToken obj)
        {
            unchecked
            {
                var hash = 17;
                hash = hash * 23 + obj.Token.GetHashCode();
                hash = hash * 23 + obj.TokenType.GetHashCode();

                if (obj.Scope != null)
                {
                    hash = hash * 23 + obj.Scope.GetHashCode();
                }

                if (obj.RefreshToken != null)
                {
                    hash = hash * 23 + obj.RefreshToken.GetHashCode();
                }

                if (obj.ExpirationDate != null)
                {
                    hash = hash * 23 + obj.ExpirationDate.GetHashCode();
                }

                return hash;
            }
        }
    }
}