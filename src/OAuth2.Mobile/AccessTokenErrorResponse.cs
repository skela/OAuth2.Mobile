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
        /// The errorn.
        /// </value>
        public string error { get; set; }

        /// <summary>
        /// Gets or sets the error description.
        /// </summary>
        /// <value>
        /// The error description.
        /// </value>
        public string error_description { get; set; }

        /// <summary>
        /// Gets a value indicating whether this instance is empty.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is empty; otherwise, <c>false</c>.
        /// </value>
        public bool IsEmpty
        {
            get
            {
                return string.IsNullOrWhiteSpace(this.error) && string.IsNullOrWhiteSpace(this.error_description);
            }
        }
    }
}