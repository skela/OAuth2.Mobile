namespace StudioDonder.OAuth2.Mobile.Tests
{
    using System;
    using System.Threading;

    using Xunit;

    public class AccessTokenClientTests
    {
        private const string ClientId = "demo-client-1";
        private const string ClientSecret = "demo-client-secret-1";
        private const string Username = "demo-user-1";
        private const string Password = "demo-user-password-1";
        private const string UserScope = "demo-scope-1";

        private static readonly Uri TokensUrl = new Uri("/tokens", UriKind.Relative);
        private static readonly Uri BaseUrl = new Uri("https://oauth2demo.azurewebsites.net");

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