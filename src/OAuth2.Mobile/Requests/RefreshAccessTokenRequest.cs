namespace StudioDonder.OAuth2.Mobile.Requests
{
    using System.Collections.Specialized;

    using Validation;

    /// <summary>
    /// A request to refresh an issued access token. Implements: http://tools.ietf.org/html/rfc6749#section-6
    /// </summary>
    internal class RefreshAccessTokenRequest : TokenRequest
    {
        private const string RefreshTokenGrantType = "refresh_token";

        private readonly string refreshToken;
        private readonly string clientId;
        private readonly string clientSecret;

        /// <summary>
        /// Initializes a new instance of the <see cref="RefreshAccessTokenRequest"/> class.
        /// </summary>
        /// <param name="refreshToken">The refresh token.</param>
        /// <param name="clientId">The client id.</param>
        /// <param name="clientSecret">The client secret.</param>
        /// <exception cref="System.ArgumentNullException">
        /// refreshToken
        /// or
        /// clientId
        /// or
        /// clientSecret
        /// </exception>
        public RefreshAccessTokenRequest(string refreshToken, string clientId, string clientSecret)
        {
            Requires.NotNullOrEmpty(refreshToken, "refreshToken");
            Requires.NotNullOrEmpty(clientId, "clientId");
            Requires.NotNullOrEmpty(clientSecret, "clientSecret");

            this.refreshToken = refreshToken;
            this.clientId = clientId;
            this.clientSecret = clientSecret;
        }

        /// <summary>
        /// Gets the parameters representing the request.
        /// </summary>
        /// <returns>
        /// The parameters.
        /// </returns>
        protected override NameValueCollection GetParameters()
        {
            return new NameValueCollection
                       {
                           { "grant_type", RefreshTokenGrantType },
                           { "client_id", this.clientId },
                           { "client_secret", this.clientSecret },
                           { "refresh_token", this.refreshToken }
                       };
        }
    }
}