namespace StudioDonder.OAuth2.Mobile.IntegrationTests
{
    using System;
    using System.Threading;

    using Xunit;

    public class AccessTokenClientTests
    {
        private const string ClientId = "demo-client-1";
        private const string ClientSecret = "demo-client-secret-1";
        private const string ClientScope = "demo-scope-client-1";
        private const string Username = "demo-user-1";
        private const string Password = "demo-user-password-1";
        private const string UserScope = "demo-scope-1";

        private static readonly Uri TokensUrl = new Uri("/tokens", UriKind.Relative);
        private static readonly Uri BaseUrl = new Uri("https://oauth2demo.azurewebsites.net");
        private static readonly OAuthServerConfiguration ServerConfiguration = new OAuthServerConfiguration(BaseUrl, TokensUrl, ClientId, ClientSecret);
        
        [Fact]
        public void GetUserAccessTokenWithValidCredentialsReturnsAccessToken()
        {
            // Arrange
            var accessTokenClient = new AccessTokenClient(ServerConfiguration);
            
            // Act
            var task = accessTokenClient.GetUserAccessToken(Username, Password, UserScope);
            task.Wait();

            // Assert
            Assert.NotNull(task.Result);
        }

        [Fact]
        public void GetUserAccessTokenCancellationTokenOverloadWithValidCredentialsReturnsAccessToken()
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
            var task = accessTokenClient.GetUserAccessToken(Username, Password, "invalid user scope");
            
            // Assert
            Assert.Throws<AggregateException>(() => task.Wait());
        }

        [Fact]
        public void GetUserAccessTokenCancellationTokenOverloadWithValidCredentialsButInvalidScopeThrowsException()
        {
            // Arrange
            var accessTokenClient = new AccessTokenClient(ServerConfiguration);

            // Act
            var task = accessTokenClient.GetUserAccessToken(Username, Password, "invalid user scope", CancellationToken.None);

            // Assert
            Assert.Throws<AggregateException>(() => task.Wait());
        }

        [Fact]
        public void GetUserAccessTokenWithNullScopeDoesNotThrowException()
        {
            // Arrange
            var accessTokenClient = new AccessTokenClient(ServerConfiguration);
            string nullScope = null;

            // Act

            // Assert
            Assert.DoesNotThrow(() => accessTokenClient.GetUserAccessToken(Username, Password, nullScope));
        }

        [Fact]
        public void GetUserAccessTokenCancellationTokenOverloadWithNullScopeDoesNotThrowException()
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
            var task = accessTokenClient.GetClientAccessToken(ClientScope);
            task.Wait();

            // Assert
            Assert.NotNull(task.Result);
        }

        [Fact]
        public void GetClientAccessTokenCancellationTokenOverloadReturnsAccessToken()
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
            var task = accessTokenClient.GetClientAccessToken("invalid client scope");

            // Assert
            Assert.Throws<AggregateException>(() => task.Wait());
        }

        [Fact]
        public void GetClientAccessTokenCancellationTokenOverloadWithInvalidScopeReturnsAccessToken()
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
            Assert.DoesNotThrow(() => accessTokenClient.GetClientAccessToken(nullScope));
        }

        [Fact]
        public void GetClientAccessTokenCancellationTokenOverloadWithNullScopeDoesNotThrowException()
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
            var task = accessTokenClient.GetUserAccessToken(Username, Password, UserScope);
            task.Wait();

            var refreshTokenTask = accessTokenClient.RefreshToken(task.Result.RefreshToken);
            refreshTokenTask.Wait();

            // Assert
            Assert.NotNull(refreshTokenTask.Result);
        }

        [Fact]
        public void RefreshTokenCancellationTokenOverloadReturnsNewAccessToken()
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