namespace StudioDonder.OAuth2.Mobile
{
    using System;

    using Validation;

    /// <summary>
    /// This class represents a serialized access token. We use this class to automatically deserialize the 
    /// result of an OAuth token response.
    /// </summary>
    internal class SerializedAccessToken
    {
        /// <summary>
        /// Gets or sets the access token.
        /// </summary>
        /// <value>
        /// The access token.
        /// </value>
        public string access_token { get; set; }

        /// <summary>
        /// Gets or sets the refresh token.
        /// </summary>
        /// <value>
        /// The refresh token.
        /// </value>
        public string refresh_token { get; set; }

        /// <summary>
        /// Gets or sets the token type.
        /// </summary>
        /// <value>
        /// The token type.
        /// </value>
        public string token_type { get; set; }

        /// <summary>
        /// Gets or sets the number of seconds the token has before it expires. If this value is <c>null</c>,
        /// the token will never expire.
        /// </summary>
        /// <value>
        /// The number of seconds the token has before it expires, or <c>null</c> if it never expires.
        /// </value>
        public int? expires_in { get; set; }

        /// <summary>
        /// Gets or sets the scope of the token.
        /// </summary>
        /// <value>
        /// The scope.
        /// </value>
        public string scope { get; set; }

        private DateTime? ExpirationDate
        {
            get
            {
                return this.expires_in == null ? (DateTime?)null : DateTime.Now.AddSeconds(Convert.ToInt32(this.expires_in));
            }
        }

        /// <summary>
        /// Convert this instance to a <see cref="AccessToken"/> instance.
        /// </summary>
        /// <returns>An <see cref="AccessToken"/> that represents this instance.</returns>
        public AccessToken ToAccessToken()
        {
            Verify.Operation(this.access_token != null, "The \"access_token\" property must not be null.");
            Verify.Operation(this.token_type != null, "The \"token_type\" property must not be null.");

            return new AccessToken(this.access_token, this.token_type, this.scope, this.ExpirationDate, this.refresh_token);
        }
    }
}