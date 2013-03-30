namespace StudioDonder.OAuth2.Mobile
{
    /// <summary>
    /// This class represents a access token error response.
    /// </summary>
    internal class AccessTokenErrorResponse
    {
        /// <summary>
        /// Gets or sets the error.
        /// </summary>
        /// <value>
        /// The error.
        /// </value>
        public string error { get; set; }

        /// <summary>
        /// Gets or sets the error description.
        /// </summary>
        /// <value>
        /// The error description.
        /// </value>
        public string error_description { get; set; }
    }
}