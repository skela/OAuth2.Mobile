namespace StudioDonder.OAuth2.Mobile.Tests.Requests
{
    using System;
    using System.Collections.Generic;

    using RestSharp;

    using StudioDonder.OAuth2.Mobile.Requests;
    using StudioDonder.OAuth2.Mobile.Tests.Helpers;

    using Xunit;

    public class ResourceOwnerPasswordCredentialsGrantTokenRequestTests
    {
        private const string Username = "test username";
        private const string Password = "test password";
        private const string ClientId = "test client id";
        private const string Scope = "test scope";

        [Fact]
        public void ConstructorWithNullUsernameThrowsArgumentNullException()
        {
            // Arrange
            string nullUsername = null;

            // Act

            // Assert
            Assert.Throws<ArgumentNullException>(() => new ResourceOwnerPasswordCredentialsGrantTokenRequest(nullUsername, Password, ClientId, Scope));
        }

        [Fact]
        public void ConstructorWithEmptyUsernameThrowsArgumentException()
        {
            // Arrange
            var emptyUsername = string.Empty;

            // Act

            // Assert
            Assert.Throws<ArgumentException>(() => new ResourceOwnerPasswordCredentialsGrantTokenRequest(emptyUsername, Password, ClientId, Scope));
        }

        [Fact]
        public void ConstructorWithNullPasswordThrowsArgumentNullException()
        {
            // Arrange
            string nullPassword = null;

            // Act

            // Assert
            Assert.Throws<ArgumentNullException>(() => new ResourceOwnerPasswordCredentialsGrantTokenRequest(Username, nullPassword, ClientId, Scope));
        }

        [Fact]
        public void ConstructorWithEmptyPasswordThrowsArgumentException()
        {
            // Arrange
            string emptyPassword = string.Empty;

            // Act

            // Assert
            Assert.Throws<ArgumentException>(() => new ResourceOwnerPasswordCredentialsGrantTokenRequest(Username, emptyPassword, ClientId, Scope));
        }

        [Fact]
        public void ConstructorWithNullClientIdThrowsArgumentNullException()
        {
            // Arrange
            string nullClientId = null;

            // Act

            // Assert
            Assert.Throws<ArgumentNullException>(() => new ResourceOwnerPasswordCredentialsGrantTokenRequest(Username, Password, nullClientId, Scope));
        }

        [Fact]
        public void ConstructorWithEmptyClientIdThrowsArgumentException()
        {
            // Arrange
            string emptyClientId = string.Empty;

            // Act

            // Assert
            Assert.Throws<ArgumentException>(() => new ResourceOwnerPasswordCredentialsGrantTokenRequest(Username, Password, emptyClientId, Scope));
        }

        [Fact]
        public void ConstructorWithNullScopeDoesNotThrowException()
        {
            // Arrange
            string nullScope = null;

            // Act

            // Assert
            Assert.DoesNotThrow(() => new ResourceOwnerPasswordCredentialsGrantTokenRequest(Username, Password, ClientId, nullScope));
        }

        [Fact]
        public void ToRestRequestWithNullTokensUriThrowsArgumentNullException()
        {
            // Arrange
            Uri tokensUri = null;
            var resourceOwnerPasswordCredentialsGrantTokenRequest = new ResourceOwnerPasswordCredentialsGrantTokenRequest(Username, Password, ClientId, Scope);

            // Act

            // Assert
            Assert.Throws<ArgumentNullException>(() => resourceOwnerPasswordCredentialsGrantTokenRequest.ToRestRequest(tokensUri));
        }

        [Fact]
        public void ToRestRequestReturnsRestRequestWithSpecifiedTokensUri()
        {
            // Arrange
            var tokensUri = new Uri("/tokens", UriKind.Relative);
            var resourceOwnerPasswordCredentialsGrantTokenRequest = new ResourceOwnerPasswordCredentialsGrantTokenRequest(Username, Password, ClientId, Scope);

            // Act
            var restRequest = resourceOwnerPasswordCredentialsGrantTokenRequest.ToRestRequest(tokensUri);

            // Assert
            Assert.Equal(tokensUri.ToString(), restRequest.Resource);
        }

        [Fact]
        public void ToRestRequestReturnsRestRequestWithMethodIsPost()
        {
            // Arrange
            var tokensUri = new Uri("/tokens", UriKind.Relative);
            var resourceOwnerPasswordCredentialsGrantTokenRequest = new ResourceOwnerPasswordCredentialsGrantTokenRequest(Username, Password, ClientId, Scope);

            // Act
            var restRequest = resourceOwnerPasswordCredentialsGrantTokenRequest.ToRestRequest(tokensUri);

            // Assert
            Assert.Equal(Method.POST, restRequest.Method);
        }

        [Fact]
        public void ToRestRequestReturnsRestRequestWithCorrectParameters()
        {
            // Arrange
            var tokensUri = new Uri("/tokens", UriKind.Relative);
            var resourceOwnerPasswordCredentialsGrantTokenRequest = new ResourceOwnerPasswordCredentialsGrantTokenRequest(Username, Password, ClientId, Scope);

            // Act
            var restRequest = resourceOwnerPasswordCredentialsGrantTokenRequest.ToRestRequest(tokensUri);

            // Assert
            var expectedParameters = new List<Parameter>
                       {
                           new Parameter { Name = "grant_type", Value = "password", Type = ParameterType.GetOrPost },
                           new Parameter { Name = "client_id", Value = ClientId, Type = ParameterType.GetOrPost },
                           new Parameter { Name = "username", Value = Username, Type = ParameterType.GetOrPost },
                           new Parameter { Name = "password", Value = Password, Type = ParameterType.GetOrPost },
                           new Parameter { Name = "scope", Value = Scope, Type = ParameterType.GetOrPost }
                       };

            Assert.Equal(expectedParameters, restRequest.Parameters, new ParameterEqualityComparer());
        }
    }
}