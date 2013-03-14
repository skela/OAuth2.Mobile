namespace StudioDonder.OAuth2.Mobile.Tests.Requests
{
    using System;
    using System.Collections.Generic;
    using System.Collections.Specialized;

    using RestSharp;

    using StudioDonder.OAuth2.Mobile.Requests;
    using StudioDonder.OAuth2.Mobile.Tests.Helpers;

    using Xunit;

    public class TokenRequestTests
    {
        [Fact]
        public void ToRestRequestReturnsRestRequestWithSpecifiedTokensUri()
        {
            // Arrange
            var tokensUri = new Uri("/tokens", UriKind.Relative);
            var testableTokenRequest = new TestableTokenRequest(CreateParameters());

            // Act
            var restRequest = testableTokenRequest.ToRestRequest(tokensUri);

            // Assert
            Assert.Equal(tokensUri.ToString(), restRequest.Resource);
        }

        [Fact]
        public void ToRestRequestReturnsRestRequestWithMethodIsPost()
        {
            // Arrange
            var tokensUri = new Uri("/tokens", UriKind.Relative);
            var testableTokenRequest = new TestableTokenRequest(CreateParameters());

            // Act
            var restRequest = testableTokenRequest.ToRestRequest(tokensUri);

            // Assert
            Assert.Equal(Method.POST, restRequest.Method);
        }

        [Fact]
        public void ToRestRequestReturnsRestRequestWithCorrectParameters()
        {
            // Arrange
            var tokensUri = new Uri("/tokens", UriKind.Relative);
            var testableTokenRequest = new TestableTokenRequest(CreateParameters());

            // Act
            var restRequest = testableTokenRequest.ToRestRequest(tokensUri);

            // Assert
            Assert.Equal(CreateParameters(), restRequest.Parameters, new ParameterEqualityComparer());
        }

        [Fact]
        public void ToRestRequestWithGetParametersReturnsNullThrowsInvalidOperationException()
        {
            // Arrange
            var tokensUri = new Uri("/tokens", UriKind.Relative);
            var testableTokenRequest = new TestableTokenRequest(CreateParameters());

            // Act
            testableTokenRequest.Parameters = null;

            // Assert
            Assert.Throws<InvalidOperationException>(() => testableTokenRequest.ToRestRequest(tokensUri));
        }

        private static IEnumerable<Parameter> CreateParameters()
        {
            return new List<Parameter>
                       {
                           new Parameter { Name = "key1", Value = "value1", Type = ParameterType.GetOrPost },
                           new Parameter { Name = "key2", Value = "value2", Type = ParameterType.GetOrPost },
                       };
        }

        private class TestableTokenRequest : TokenRequest
        {
            public TestableTokenRequest(IEnumerable<Parameter> parameters)
            {
                this.Parameters = new NameValueCollection();

                foreach (var parameter in parameters)
                {
                    this.Parameters.Add(parameter.Name, parameter.Value.ToString());
                }
            }

            public NameValueCollection Parameters { get; set; }

            protected override NameValueCollection GetParameters()
            {
                return this.Parameters;
            }
        }
    }
}