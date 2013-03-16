namespace StudioDonder.OAuth2.Mobile
{
    using System.Threading;
    using System.Threading.Tasks;

    using RestSharp;

    using StudioDonder.OAuth2.Mobile.Requests;

    using Validation;

    public class AccessTokenClient
    {
        private readonly OAuthServerConfiguration serverConfiguration;

        /// <summary>
        /// Initializes a new instance of the <see cref="AccessTokenClient"/> class.
        /// </summary>
        /// <param name="serverConfiguration">The client configuration.</param>
        /// <exception cref="System.ArgumentNullException">clientConfiguration</exception>
        public AccessTokenClient(OAuthServerConfiguration serverConfiguration)
        {
            Requires.NotNull(serverConfiguration, "clientConfiguration");

            this.serverConfiguration = serverConfiguration;
            this.RestClient = new RestClient(serverConfiguration.BaseUrl.ToString());
        }

        public RestClient RestClient { get; private set; }

        public Task<AccessToken> GetUserAccessToken(string username, string password, string scope, CancellationToken cancellationToken)
        {
            Requires.NotNullOrEmpty(username, "username");
            Requires.NotNullOrEmpty(password, "password");
            
            return this.ExecuteAccessTokenRequest(new ResourceOwnerPasswordCredentialsGrantTokenRequest(username, password, this.serverConfiguration.ClientId, scope), cancellationToken);
        }

        public Task<AccessToken> GetClientAccessToken(string scope, CancellationToken cancellationToken)
        {
            return this.ExecuteAccessTokenRequest(new ClientCredentialsGrantTokenRequest(this.serverConfiguration.ClientId, this.serverConfiguration.ClientSecret, scope), cancellationToken);
        }

        public Task<AccessToken> RefreshToken(string refreshToken, CancellationToken cancellationToken)
        {
            Requires.NotNullOrEmpty(refreshToken, "refreshToken");
            
            return this.ExecuteAccessTokenRequest(new RefreshAccessTokenRequest(refreshToken, this.serverConfiguration.ClientId, this.serverConfiguration.ClientSecret), cancellationToken);
        }

        private Task<AccessToken> ExecuteAccessTokenRequest(TokenRequest tokenRequest, CancellationToken cancellationToken)
        {
            var restRequest = tokenRequest.ToRestRequest(this.serverConfiguration.TokensUrl);

            return this.RestClient.ExecuteAsync<SerializedAccessToken>(restRequest, cancellationToken)
                       .ContinueWith(t => t.Result.ToAccessToken(), TaskContinuationOptions.OnlyOnRanToCompletion);
        }
    }
}