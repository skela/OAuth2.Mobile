namespace StudioDonder.OAuth2.Mobile.Tests
{
    using System;
    using System.Net;
    using System.Threading;

    using Xunit;

    public class AccessTokenClientTests
    {
        private const string ClientId = "demo-identifier";
        private const string ClientSecret = "demo-secret";
        private const string Username = "demo-username";
        private const string Password = "demo-password";
        private const string ClientScope = "demo-scope-client";
        private const string UserScope = "demo-scope-user";

        private static readonly Uri TokensUrl = new Uri("/tokens", UriKind.Relative);
        private static readonly Uri BaseUrl = new Uri("https://oauth2demo.azurewebsites.net");

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
            Assert.Equal(ServerConfiguration.BaseUrl.OriginalString, accessTokenClient.RestClient.BaseUrl);
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
        public void GetUserAccessTokenWithValidCredentialsButInvalidScopeThrowsException()
        {
            // Arrange
            var accessTokenClient = new AccessTokenClient(ServerConfiguration);

            // Act
            var task = accessTokenClient.GetUserAccessToken(Username, Password, "invalid user scope", CancellationToken.None);
            
            // Assert
            Assert.Throws<AggregateException>(() => task.Wait());
        }

        [Fact]
        public void GetUserAccessTokenWithNullUsernameThrowsArgumentNullException()
        {
            // Arrange
            var accessTokenClient = new AccessTokenClient(ServerConfiguration);
            string nullUsername = null;

            // Act
            
            // Assert
            Assert.Throws<ArgumentNullException>(() => accessTokenClient.GetUserAccessToken(nullUsername, Password, UserScope, CancellationToken.None));
        }

        [Fact]
        public void GetUserAccessTokenWithEmptyUsernameThrowsArgumentException()
        {
            // Arrange
            var accessTokenClient = new AccessTokenClient(ServerConfiguration);
            var emptyUsername = string.Empty;

            // Act

            // Assert
            Assert.Throws<ArgumentException>(() => accessTokenClient.GetUserAccessToken(emptyUsername, Password, UserScope, CancellationToken.None));
        }

        [Fact]
        public void GetUserAccessTokenWithNullPasswordThrowsArgumentNullException()
        {
            // Arrange
            var accessTokenClient = new AccessTokenClient(ServerConfiguration);
            string nullPassword = null;

            // Act

            // Assert
            Assert.Throws<ArgumentNullException>(() => accessTokenClient.GetUserAccessToken(Username, nullPassword, UserScope, CancellationToken.None));
        }

        [Fact]
        public void GetUserAccessTokenWithEmptyPasswordThrowsArgumentException()
        {
            // Arrange
            var accessTokenClient = new AccessTokenClient(ServerConfiguration);
            var emptyPassword = string.Empty;

            // Act

            // Assert
            Assert.Throws<ArgumentException>(() => accessTokenClient.GetUserAccessToken(Username, emptyPassword, UserScope, CancellationToken.None));
        }

        [Fact]
        public void GetUserAccessTokenWithNullScopeDoesNotThrowException()
        {
            // Arrange
            var accessTokenClient = new AccessTokenClient(ServerConfiguration);
            string nullScope = null;

            // Act

            // Assert
            Assert.DoesNotThrow(() => accessTokenClient.GetUserAccessToken(Username, Password, nullScope, CancellationToken.None));
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
        public void GetClientAccessTokenWithInvalidScopeReturnsAccessToken()
        {
            // Arrange
            var accessTokenClient = new AccessTokenClient(ServerConfiguration);

            // Act
            var task = accessTokenClient.GetClientAccessToken("invalid client scope", CancellationToken.None);

            // Assert
            Assert.Throws<AggregateException>(() => task.Wait());
        }

        [Fact]
        public void GetClientAccessTokenWithNullScopeDoesNotThrowException()
        {
            // Arrange
            var accessTokenClient = new AccessTokenClient(ServerConfiguration);
            string nullScope = null;

            // Act

            // Assert
            Assert.DoesNotThrow(() => accessTokenClient.GetClientAccessToken(nullScope, CancellationToken.None));
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

        [Fact]
        public void RefreshTokenWithNullRefreshTokenThrowsArgumentNullException()
        {
            // Arrange
            var accessTokenClient = new AccessTokenClient(ServerConfiguration);
            string nullRefreshToken = null;

            // Act

            // Assert
            Assert.Throws<ArgumentNullException>(() => accessTokenClient.RefreshToken(nullRefreshToken, CancellationToken.None));
        }

        [Fact]
        public void RefreshTokenWithEmptyRefreshTokenThrowsArgumentException()
        {
            // Arrange
            var accessTokenClient = new AccessTokenClient(ServerConfiguration);
            var emptyRefreshToken = string.Empty;

            // Act

            // Assert
            Assert.Throws<ArgumentException>(() => accessTokenClient.RefreshToken(emptyRefreshToken, CancellationToken.None));
        }
    }
}