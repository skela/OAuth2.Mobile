namespace StudioDonder.OAuth2.Mobile
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    using RestSharp;

    using Validation;

    /// <summary>
    /// Extensions to the <see cref="IRestClient"/> interface.
    /// </summary>
    public static class RestClientExtensions
    {
        /// <summary>
        /// Executes a request asynchronously.
        /// </summary>
        /// <param name="client">The client.</param>
        /// <param name="request">The request.</param>
        /// <param name="token">The token.</param>
        /// <returns>The asynchronous request task.</returns>
        public static Task<IRestResponse> ExecuteAsync(this IRestClient client, IRestRequest request, CancellationToken token)
        {
            Requires.NotNull(client, "client");
            Requires.NotNull(request, "request");

            var taskCompletionSource = new TaskCompletionSource<IRestResponse>();

            try
            {
                var async = client.ExecuteAsync(request, (response, _) =>
                    {
                        if (token.IsCancellationRequested)
                        {
                            taskCompletionSource.TrySetCanceled();
                        }
                        else if (response.ErrorException != null)
                        {
                            taskCompletionSource.TrySetException(response.ErrorException);
                        }
                        else if (response.ResponseStatus != ResponseStatus.Completed)
                        {
                            taskCompletionSource.TrySetException(response.ResponseStatus.ToHttpException());
                        }
                        else
                        {
                            taskCompletionSource.TrySetResult(response);
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
    }
}