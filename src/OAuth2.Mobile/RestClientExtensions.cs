namespace StudioDonder.OAuth2.Mobile
{
    using System;
    using System.Net;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Web;

    using RestSharp;

    public static class RestClientExtensions
    {
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
                        else if (response.StatusCode == HttpStatusCode.OK)
                        {
                            taskCompletionSource.TrySetResult(response.Data);
                        }
                        else
                        {
                            taskCompletionSource.TrySetException(new HttpException((int)response.StatusCode, response.StatusDescription));
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

        public static Task ExecuteAsync(this IRestClient client, IRestRequest request, CancellationToken token)
        {
            return client.ExecuteAsync<IRestResponse>(request, token);
        }
    }
}