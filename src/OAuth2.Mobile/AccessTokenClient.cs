namespace StudioDonder.OAuth2.Mobile
{
    using System.Net;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Web;

    using RestSharp;
    using RestSharp.Deserializers;

    using StudioDonder.OAuth2.Mobile.Requests;

    using Validation;

    /// <summary>
    /// This client allows retrieval of access tokens.
    /// </summary>
    public class AccessTokenClient
    {
        private readonly OAuthServerConfiguration serverConfiguration;
        private readonly JsonDeserializer jsonDeserializer;

        /// <summary>
        /// Initializes a new instance of the <see cref="AccessTokenClient"/> class.
        /// </summary>
        /// <param name="serverConfiguration">The client configuration.</param>
        /// <exception cref="System.ArgumentNullException">clientConfiguration</exception>
        public AccessTokenClient(OAuthServerConfiguration serverConfiguration)
        {
            Requires.NotNull(serverConfiguration, "clientConfiguration");

            this.serverConfiguration = serverConfiguration;
            this.jsonDeserializer = new JsonDeserializer();
            this.RestClient = new RestClient(serverConfiguration.BaseUrl.ToString());
        }

        /// <summary>
        /// Gets the rest client used to make the requests.
        /// </summary>
        /// <value>
        /// The rest client.
        /// </value>
        public RestClient RestClient { get; private set; }

        /// <summary>
        /// Gets an access token for a user.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="password">The password.</param>
        /// <param name="scope">The scope.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The access token retrieval task.</returns>
        public Task<AccessToken> GetUserAccessToken(string username, string password, string scope, CancellationToken cancellationToken)
        {
            Requires.NotNullOrEmpty(username, "username");
            Requires.NotNullOrEmpty(password, "password");
            
            return this.ExecuteAccessTokenRequest(new ResourceOwnerPasswordCredentialsGrantTokenRequest(username, password, this.serverConfiguration.ClientId, scope), cancellationToken);
        }

        /// <summary>
        /// Gets an access token for a client.
        /// </summary>
        /// <param name="scope">The scope.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The access token retrieval task.</returns>
        public Task<AccessToken> GetClientAccessToken(string scope, CancellationToken cancellationToken)
        {
            return this.ExecuteAccessTokenRequest(new ClientCredentialsGrantTokenRequest(this.serverConfiguration.ClientId, this.serverConfiguration.ClientSecret, scope), cancellationToken);
        }

        /// <summary>
        /// Exchanged a refresh token for a new access token.
        /// </summary>
        /// <param name="refreshToken">The refresh token.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public Task<AccessToken> RefreshToken(string refreshToken, CancellationToken cancellationToken)
        {
            Requires.NotNullOrEmpty(refreshToken, "refreshToken");
            
            return this.ExecuteAccessTokenRequest(new RefreshAccessTokenRequest(refreshToken, this.serverConfiguration.ClientId, this.serverConfiguration.ClientSecret), cancellationToken);
        }

        private Task<AccessToken> ExecuteAccessTokenRequest(TokenRequest tokenRequest, CancellationToken cancellationToken)
        {
            var restRequest = tokenRequest.ToRestRequest(this.serverConfiguration.TokensUrl);
            restRequest.OnBeforeDeserialization += response =>
                {
                    if ((int)response.StatusCode >= 400)
                    {
                        return;
                    }

                    var accessTokenErrorResponse = this.jsonDeserializer.Deserialize<AccessTokenErrorResponse>(response);

                    if (accessTokenErrorResponse.IsEmpty)
                    {
                        return;
                    }

                    throw new HttpException((int)HttpStatusCode.BadRequest, accessTokenErrorResponse.error_description);
                };

            return this.RestClient.ExecuteAsync<AccessTokenResponse>(restRequest, cancellationToken).ContinueWith(t => t.Result.ToAccessToken(), TaskContinuationOptions.OnlyOnRanToCompletion);
        }
    }
}