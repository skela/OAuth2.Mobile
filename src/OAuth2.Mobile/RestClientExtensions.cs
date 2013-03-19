namespace StudioDonder.OAuth2.Mobile
{
    using System;
    using System.Net;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Web;

    using RestSharp;

    /// <summary>
    /// Extensions to the <see cref="IRestClient"/> interface.
    /// </summary>
    public static class RestClientExtensions
    {
        /// <summary>
        /// Executes a request asynchronously and convert the response to a specific type.
        /// </summary>
        /// <typeparam name="T">The type to convert the response to.</typeparam>
        /// <param name="client">The client.</param>
        /// <param name="request">The request.</param>
        /// <param name="token">The token.</param>
        /// <returns>
        /// The asynchronous request task.
        /// </returns>
        public static Task<T> ExecuteAsync<T>(this IRestClient client, IRestRequest request, CancellationToken token)
        {
            var taskCompletionSource = new TaskCompletionSource<T>();
            
            try
            {
                var async = client.ExecuteAsync<T>(request, (response, _) =>
                    {
                        if (token.IsCancellationRequested || response == null)
                        {
                            return;
                        }

                        if (response.ErrorException != null)
                        {
                            taskCompletionSource.TrySetException(response.ErrorException);
                        }
                        else if (IsHttpException(response.StatusCode))
                        {
                            taskCompletionSource.TrySetException(new HttpException((int)response.StatusCode, response.StatusDescription));
                        }
                        else
                        {
                            taskCompletionSource.TrySetResult(response.Data);
                        }
                    });

                token.Register(() =>
                    {
                        async.Abort();
                        taskCompletionSource.TrySetCanceled();
                    });
            }
            catch (Exception ex)
            {
                taskCompletionSource.TrySetException(ex);
            }

            return taskCompletionSource.Task;
        }

        /// <summary>
        /// Executes a request asynchronously.
        /// </summary>
        /// <param name="client">The client.</param>
        /// <param name="request">The request.</param>
        /// <param name="token">The token.</param>
        /// <returns>The asynchronous request task.</returns>
        public static Task ExecuteAsync(this IRestClient client, IRestRequest request, CancellationToken token)
        {
            return client.ExecuteAsync<IRestResponse>(request, token);
        }

        private static bool IsHttpException(HttpStatusCode httpStatusCode)
        {
            return (int)httpStatusCode >= 400;
        }
    }
}