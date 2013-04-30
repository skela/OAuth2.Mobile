namespace StudioDonder.OAuth2.Mobile
{
    using System;
    using System.Net;
    using System.Web;

    using RestSharp;

    /// <summary>
    /// Extensions to the <see cref="ResponseStatus"/> class.
    /// </summary>
    public static class ResponseStatusExtensions
    {
        /// <summary>
        /// Convert the <see cref="ResponseStatus"/> to an <see cref="HttpException"/>.
        /// </summary>
        /// <param name="responseStatus">The response status.</param>
        /// <returns>The <see cref="HttpException"/> instance.</returns>
        /// <exception cref="System.ArgumentException">The Completed response status cannot be converted to an HTTP exception.</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">responseStatus</exception>
        public static HttpException ToHttpException(this ResponseStatus responseStatus)
        {
            switch (responseStatus)
            {
                case ResponseStatus.None:
                    return new HttpException((int)HttpStatusCode.InternalServerError, "The request could not be processed.");
                case ResponseStatus.Error:
                    return new HttpException((int)HttpStatusCode.InternalServerError, "An error occured while processing the request.");
                case ResponseStatus.TimedOut:
                    return new HttpException((int)HttpStatusCode.RequestTimeout, "The request timed-out.");
                case ResponseStatus.Aborted:
                    return new HttpException((int)HttpStatusCode.RequestTimeout, "The request was aborted.");
                case ResponseStatus.Completed:
                    throw new ArgumentException("The Completed response status cannot be converted to an HTTP exception.");
                default:
                    throw new ArgumentOutOfRangeException("responseStatus");
            }
        }
    }
}