namespace StudioDonder.OAuth2.Mobile.Tests.Requests
{
    using System;
    using System.Collections.Generic;

    using RestSharp;

    using StudioDonder.OAuth2.Mobile.Requests;
    using StudioDonder.OAuth2.Mobile.Tests.Helpers;

    using Xunit;

    public class ClientCredentialsGrantTokenRequestTests
    {
        private const string ClientId = "test client id";
        private const string ClientSecret = "test client secret";
        private const string Scope = "test scope";

        [Fact]
        public void ConstructorWithNullClientIdThrowsArgumentNullException()
        {
            // Arrange
            string nullClientId = null;

            // Act

            // Assert
            Assert.Throws<ArgumentNullException>(() => new ClientCredentialsGrantTokenRequest(nullClientId, ClientSecret, Scope));
        }

        [Fact]
        public void ConstructorWithEmptyClientIdThrowsArgumentException()
        {
            // Arrange
            var emptyClientId = string.Empty;

            // Act

            // Assert
            Assert.Throws<ArgumentException>(() => new ClientCredentialsGrantTokenRequest(emptyClientId, ClientSecret, Scope));
        }

        [Fact]
        public void ConstructorWithNullClientSecretThrowsArgumentNullException()
        {
            // Arrange
            string nullClientSecret = null;

            // Act

            // Assert
            Assert.Throws<ArgumentNullException>(() => new ClientCredentialsGrantTokenRequest(ClientId, nullClientSecret, Scope));
        }

        [Fact]
        public void ConstructorWithEmptyClientSecretThrowsArgumentException()
        {
            // Arrange
            var emptyClientSecret = string.Empty;

            // Act

            // Assert
            Assert.Throws<ArgumentException>(() => new ClientCredentialsGrantTokenRequest(ClientId, emptyClientSecret, Scope));
        }

        [Fact]
        public void ConstructorWithNullScopeDoesNotThrowException()
        {
            // Arrange
            string nullScope = null;

            // Act

            // Assert
            Assert.DoesNotThrow(() => new ClientCredentialsGrantTokenRequest(ClientId, ClientSecret, nullScope));
        }

        [Fact]
        public void ToRestRequestWithNullTokensUriThrowsArgumentNullException()
        {
            // Arrange
            Uri tokensUri = null;
            var clientCredentialsGrantTokenRequest = new ClientCredentialsGrantTokenRequest(ClientId, ClientSecret, Scope);

            // Act
            
            // Assert
            Assert.Throws<ArgumentNullException>(() => clientCredentialsGrantTokenRequest.ToRestRequest(tokensUri));
        }

        [Fact]
        public void ToRestRequestReturnsRestRequestWithSpecifiedTokensUri()
        {
            // Arrange
            var tokensUri = new Uri("/tokens", UriKind.Relative);
            var clientCredentialsGrantTokenRequest = new ClientCredentialsGrantTokenRequest(ClientId, ClientSecret, Scope);

            // Act
            var restRequest = clientCredentialsGrantTokenRequest.ToRestRequest(tokensUri);

            // Assert
            Assert.Equal(tokensUri.ToString(), restRequest.Resource);
        }

        [Fact]
        public void ToRestRequestReturnsRestRequestWithMethodIsPost()
        {
            // Arrange
            var tokensUri = new Uri("/tokens", UriKind.Relative);
            var clientCredentialsGrantTokenRequest = new ClientCredentialsGrantTokenRequest(ClientId, ClientSecret, Scope);

            // Act
            var restRequest = clientCredentialsGrantTokenRequest.ToRestRequest(tokensUri);

            // Assert
            Assert.Equal(Method.POST, restRequest.Method);
        }

        [Fact]
        public void ToRestRequestReturnsRestRequestWithCorrectParameters()
        {
            // Arrange
            var tokensUri = new Uri("/tokens", UriKind.Relative);
            var clientCredentialsGrantTokenRequest = new ClientCredentialsGrantTokenRequest(ClientId, ClientSecret, Scope);

            // Act
            var restRequest = clientCredentialsGrantTokenRequest.ToRestRequest(tokensUri);

            // Assert
            var expectedParameters = new List<Parameter>
                       {
                           new Parameter { Name = "grant_type", Value = "client_credentials", Type = ParameterType.GetOrPost },
                           new Parameter { Name = "client_id", Value = ClientId, Type = ParameterType.GetOrPost },
                           new Parameter { Name = "client_secret", Value = ClientSecret, Type = ParameterType.GetOrPost },
                           new Parameter { Name = "scope", Value = Scope, Type = ParameterType.GetOrPost }
                       };

            Assert.Equal(expectedParameters, restRequest.Parameters, new ParameterEqualityComparer());
        }
    }
}