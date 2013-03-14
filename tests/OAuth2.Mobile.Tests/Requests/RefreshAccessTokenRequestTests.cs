namespace StudioDonder.OAuth2.Mobile.Tests.Requests
{
    using System;
    using System.Collections.Generic;

    using RestSharp;

    using StudioDonder.OAuth2.Mobile.Requests;
    using StudioDonder.OAuth2.Mobile.Tests.Helpers;

    using Xunit;

    public class RefreshAccessTokenRequestTests
    {
        private const string RefreshToken = "test refresh token";
        private const string ClientId = "test client id";
        private const string ClientSecret = "test client secret";

        [Fact]
        public void ConstructorWithNullRefreshTokenThrowsArgumentNullException()
        {
            // Arrange
            string nullRefreshToken = null;

            // Act

            // Assert
            Assert.Throws<ArgumentNullException>(() => new RefreshAccessTokenRequest(nullRefreshToken, ClientId, ClientSecret));
        }

        [Fact]
        public void ConstructorWithEmptyRefreshTokenThrowsArgumentException()
        {
            // Arrange
            var emptyRefreshToken = string.Empty;

            // Act

            // Assert
            Assert.Throws<ArgumentException>(() => new RefreshAccessTokenRequest(emptyRefreshToken, ClientId, ClientSecret));
        }

        [Fact]
        public void ConstructorWithNullClientIdThrowsArgumentNullException()
        {
            // Arrange
            string nullClientId = null;

            // Act

            // Assert
            Assert.Throws<ArgumentNullException>(() => new RefreshAccessTokenRequest(RefreshToken, nullClientId, ClientSecret));
        }

        [Fact]
        public void ConstructorWithEmptyClientIdThrowsArgumentException()
        {
            // Arrange
            var emptyClientId = string.Empty;

            // Act

            // Assert
            Assert.Throws<ArgumentException>(() => new RefreshAccessTokenRequest(RefreshToken, emptyClientId, ClientSecret));
        }

        [Fact]
        public void ConstructorWithNullClientSecretDoesNotThrowsArgumentNullException()
        {
            // Arrange
            string nullClientSecret = null;

            // Act

            // Assert
            Assert.Throws<ArgumentNullException>(() => new RefreshAccessTokenRequest(RefreshToken, ClientId, nullClientSecret));
        }

        [Fact]
        public void ConstructorWithEmptyClientSecretDoesNotThrowsArgumentException()
        {
            // Arrange
            var emptyClientSecret = string.Empty;

            // Act

            // Assert
            Assert.Throws<ArgumentException>(() => new RefreshAccessTokenRequest(RefreshToken, ClientId, emptyClientSecret));
        }

        [Fact]
        public void ToRestRequestWithNullTokensUriThrowsArgumentNullException()
        {
            // Arrange
            Uri tokensUri = null;
            var refreshAccessTokenRequest = new RefreshAccessTokenRequest(RefreshToken, ClientId, ClientSecret);

            // Act

            // Assert
            Assert.Throws<ArgumentNullException>(() => refreshAccessTokenRequest.ToRestRequest(tokensUri));
        }

        [Fact]
        public void ToRestRequestReturnsRestRequestWithSpecifiedTokensUri()
        {
            // Arrange
            var tokensUri = new Uri("/tokens", UriKind.Relative);
            var refreshAccessTokenRequest = new RefreshAccessTokenRequest(RefreshToken, ClientId, ClientSecret);

            // Act
            var restRequest = refreshAccessTokenRequest.ToRestRequest(tokensUri);

            // Assert
            Assert.Equal(tokensUri.ToString(), restRequest.Resource);
        }

        [Fact]
        public void ToRestRequestReturnsRestRequestWithMethodIsPost()
        {
            // Arrange
            var tokensUri = new Uri("/tokens", UriKind.Relative);
            var refreshAccessTokenRequest = new RefreshAccessTokenRequest(RefreshToken, ClientId, ClientSecret);

            // Act
            var restRequest = refreshAccessTokenRequest.ToRestRequest(tokensUri);

            // Assert
            Assert.Equal(Method.POST, restRequest.Method);
        }

        [Fact]
        public void ToRestRequestReturnsRestRequestWithCorrectParameters()
        {
            // Arrange
            var tokensUri = new Uri("/tokens", UriKind.Relative);
            var refreshAccessTokenRequest = new RefreshAccessTokenRequest(RefreshToken, ClientId, ClientSecret);

            // Act
            var restRequest = refreshAccessTokenRequest.ToRestRequest(tokensUri);

            // Assert
            var expectedParameters = new List<Parameter>
                       {
                           new Parameter { Name = "grant_type", Value = "refresh_token", Type = ParameterType.GetOrPost },
                           new Parameter { Name = "client_id", Value = ClientId, Type = ParameterType.GetOrPost },
                           new Parameter { Name = "client_secret", Value = ClientSecret, Type = ParameterType.GetOrPost },
                           new Parameter { Name = "refresh_token", Value = RefreshToken, Type = ParameterType.GetOrPost }
                       };

            Assert.Equal(expectedParameters, restRequest.Parameters, new ParameterEqualityComparer());
        }
    }
}