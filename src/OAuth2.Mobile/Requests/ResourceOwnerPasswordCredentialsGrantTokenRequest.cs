namespace StudioDonder.OAuth2.Mobile.Requests
{
    using System.Collections.Specialized;

    using Validation;

    /// <summary>
    /// A request for a token based on a resource owner's password credentials. Implements: http://tools.ietf.org/html/rfc6749#section-4.3
    /// </summary>
    internal class ResourceOwnerPasswordCredentialsGrantTokenRequest : TokenRequest
    {
        private const string PasswordGrantType = "password";

        private readonly string username;
        private readonly string password;
        private readonly string clientId;
        private readonly string scope;

        /// <summary>
        /// Initializes a new instance of the <see cref="ResourceOwnerPasswordCredentialsGrantTokenRequest"/> class.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="password">The password.</param>
        /// <param name="clientId">The client id.</param>
        /// <param name="scope">The scope.</param>
        /// <exception cref="System.ArgumentNullException">
        /// username
        /// or
        /// password
        /// or
        /// clientId
        /// </exception>
        public ResourceOwnerPasswordCredentialsGrantTokenRequest(string username, string password, string clientId, string scope)
        {
            Requires.NotNullOrEmpty(username, "username");
            Requires.NotNullOrEmpty(password, "password");
            Requires.NotNullOrEmpty(clientId, "clientId");

            this.username = username;
            this.password = password;
            this.clientId = clientId;
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
                           { "grant_type", PasswordGrantType },
                           { "client_id", this.clientId },
                           { "username", this.username },
                           { "password", this.password },
                           { "scope", this.scope }
                       };
        }
    }
}