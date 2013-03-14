namespace StudioDonder.OAuth2.Mobile.Tests.Helpers
{
    using System.Collections.Generic;

    using RestSharp;

    internal class ParameterEqualityComparer : IEqualityComparer<Parameter>
    {
        public bool Equals(Parameter x, Parameter y)
        {
            if (x == null && y == null)
            {
                return true;
            }

            if (x == null || y == null)
            {
                return false;
            }

            return string.Equals(x.Name, y.Name) && x.Type == y.Type && object.Equals(x.Value, y.Value);
        }

        public int GetHashCode(Parameter obj)
        {
            unchecked
            {
                var hash = 17;
                hash = hash * 23 + obj.Name.GetHashCode();
                hash = hash * 23 + obj.Type.GetHashCode();
                hash = hash * 23 + obj.Value.GetHashCode();

                return hash;
            }
        }
    }
}