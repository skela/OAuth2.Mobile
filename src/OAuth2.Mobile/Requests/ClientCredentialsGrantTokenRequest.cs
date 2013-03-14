namespace StudioDonder.OAuth2.Mobile.Requests
{
    using System.Collections.Specialized;

    using Validation;

    /// <summary>
    /// A request for a token based on a client credentials grant. Implements: http://tools.ietf.org/html/rfc6749#section-4.4
    /// </summary>
    internal class ClientCredentialsGrantTokenRequest : TokenRequest
    {
        private const string ClientCredentialsGrantType = "client_credentials";

        private readonly string clientId;
        private readonly string clientSecret;
        private readonly string scope;

        /// <summary>
        /// Initializes a new instance of the <see cref="ClientCredentialsGrantTokenRequest"/> class.
        /// </summary>
        /// <param name="clientId">The client id.</param>
        /// <param name="clientSecret">The client secret.</param>
        /// <param name="scope">The scope.</param>
        /// <exception cref="System.ArgumentNullException">
        /// clientId
        /// or
        /// clientSecret
        /// </exception>
        public ClientCredentialsGrantTokenRequest(string clientId, string clientSecret, string scope)
        {
            Requires.NotNullOrEmpty(clientId, "clientId");
            Requires.NotNullOrEmpty(clientSecret, "clientSecret");
            
            this.clientId = clientId;
            this.clientSecret = clientSecret;
            this.scope = scope;
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
                           { "grant_type", ClientCredentialsGrantType },
                           { "client_id", this.clientId },
                           { "client_secret", this.clientSecret },
                           { "scope", this.scope }
                       };
        }
    }
}