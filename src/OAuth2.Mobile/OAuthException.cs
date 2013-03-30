namespace StudioDonder.OAuth2.Mobile
{
    using System;
    using System.Runtime.Serialization;

    /// <summary>
    /// An OAuth exception.
    /// </summary>
    [Serializable]
    public class OAuthException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OAuthException"/> class.
        /// </summary>
        public OAuthException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="OAuthException"/> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public OAuthException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="OAuthException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="inner">The inner.</param>
        public OAuthException(string message, Exception inner)
            : base(message, inner)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="OAuthException"/> class.
        /// </summary>
        /// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that holds the serialized object data about the exception being thrown.</param>
        /// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains contextual information about the source or destination.</param>
        protected OAuthException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}