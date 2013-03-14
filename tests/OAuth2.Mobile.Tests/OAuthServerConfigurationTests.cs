namespace StudioDonder.OAuth2.Mobile.Tests
{
    using System;

    using Xunit;

    public class OAuthServerConfigurationTests
    {
        private const string ClientId = "test client id";
        private const string ClientSecret = "test client secret";

        private static readonly Uri BaseUrl = new Uri("https://oauth.net");
        private static readonly Uri TokensUrl = new Uri("/tokens", UriKind.Relative);

        [Fact]
        public void ConstructorWithNullBaseUrlThrowsArgumentNullException()
        {
            // Arrange
            Uri nullBaseUrl = null;

            // Act

            // Assert
            Assert.Throws<ArgumentNullException>(() => new OAuthServerConfiguration(nullBaseUrl, TokensUrl, ClientId, ClientSecret));
        }

        [Fact]
        public void ConstructorWithNullTokensUrlThrowsArgumentNullException()
        {
            // Arrange
            Uri nullTokensUrl = null;

            // Act

            // Assert
            Assert.Throws<ArgumentNullException>(() => new OAuthServerConfiguration(BaseUrl, nullTokensUrl, ClientId, ClientSecret));
        }
        
        [Fact]
        public void ConstructorWithNullClientIdThrowsArgumentNullException()
        {
            // Arrange
            string nullClientId = null;

            // Act

            // Assert
            Assert.Throws<ArgumentNullException>(() => new OAuthServerConfiguration(BaseUrl, TokensUrl, nullClientId, ClientSecret));
        }

        [Fact]
        public void ConstructorWithEmptyClientIdThrowsArgumentException()
        {
            // Arrange
            var emptyClientId = string.Empty;

            // Act

            // Assert
            Assert.Throws<ArgumentException>(() => new OAuthServerConfiguration(BaseUrl, TokensUrl, emptyClientId, ClientSecret));
        }

        [Fact]
        public void ConstructorWithNullClientSecretThrowsArgumentNullException()
        {
            // Arrange
            string nullClientSecret = null;

            // Act

            // Assert
            Assert.Throws<ArgumentNullException>(() => new OAuthServerConfiguration(BaseUrl, TokensUrl, ClientId, nullClientSecret));
        }

        [Fact]
        public void ConstructorWithEmptyClientSecretThrowsArgumentNullException()
        {
            // Arrange
            var emptyClientSecret = string.Empty;

            // Act

            // Assert
            Assert.Throws<ArgumentException>(() => new OAuthServerConfiguration(BaseUrl, TokensUrl, ClientId, emptyClientSecret));
        }
    }
}