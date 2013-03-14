namespace StudioDonder.OAuth2.Mobile.Tests
{
    using System;
    using System.Net;
    using System.Threading;

    using Xunit;

    public class AccessTokenClientTests
    {
        //private const string ClientId = "demo-oauth-client";
        //private const string ClientSecret = "foobar";
        private const string ClientId = "iphone";
        private const string ClientSecret = "test";
        private const string Username = "erikschierboom";
        private const string Password = "erikschierboom";
        private const string ClientScope = "register_user";
        private const string UserScope = "movie list user comment";

        //private static readonly Uri TokensUrl = new Uri("https://frko.surfnetlabs.nl/workshop/php-oauth/token.php");
        private static readonly Uri BaseUrl = new Uri("https://erik.mono.sdn.io/icmapi");
        private static readonly Uri TokensUrl = new Uri("/tokens", UriKind.Relative);

        public AccessTokenClientTests()
        {
            ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;
        }

        private static OAuthServerConfiguration ServerConfiguration
        {
            get
            {
                return new OAuthServerConfiguration(BaseUrl, TokensUrl, ClientId, ClientSecret);
            }
        }

        [Fact]
        public void ConstructorCreatesRestClient()
        {
            // Arrange
            var accessTokenClient = new AccessTokenClient(ServerConfiguration);

            // Act

            // Assert
            Assert.NotNull(accessTokenClient.RestClient);
        }

        [Fact]
        public void ConstructorCreatesRestClientWithCorrectBaseUrl()
        {
            // Arrange
            var accessTokenClient = new AccessTokenClient(ServerConfiguration);

            // Act

            // Assert
            Assert.Equal(ServerConfiguration.BaseUrl.ToString(), accessTokenClient.RestClient.BaseUrl);
        }

        [Fact]
        public void GetUserAccessTokenWithValidCredentialsReturnsAccessToken()
        {
            // Arrange
            var accessTokenClient = new AccessTokenClient(ServerConfiguration);
            
            // Act
            var task = accessTokenClient.GetUserAccessToken(Username, Password, UserScope, CancellationToken.None);
            task.Wait();

            // Assert
            Assert.NotNull(task.Result);
        }

        [Fact]
        public void GetClientAccessTokenReturnsAccessToken()
        {
            // Arrange
            var accessTokenClient = new AccessTokenClient(ServerConfiguration);

            // Act
            var task = accessTokenClient.GetClientAccessToken(ClientScope, CancellationToken.None);
            task.Wait();

            // Assert
            Assert.NotNull(task.Result);
        }

        [Fact]
        public void RefreshTokenReturnsNewAccessToken()
        {
            // Arrange
            var accessTokenClient = new AccessTokenClient(ServerConfiguration);

            // Act
            var task = accessTokenClient.GetUserAccessToken(Username, Password, UserScope, CancellationToken.None);
            task.Wait();

            var refreshTokenTask = accessTokenClient.RefreshToken(task.Result.RefreshToken, CancellationToken.None);
            refreshTokenTask.Wait();

            // Assert
            Assert.NotNull(refreshTokenTask.Result);
        }
    }
}