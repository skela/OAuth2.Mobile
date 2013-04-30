namespace StudioDonder.OAuth2.Mobile.Tests
{
    using System;
    using System.Threading;

    using RestSharp;

    using Xunit;

    public class RestClientExtensionsTests
    {
        [Fact]
        public void ExecuteAsyncOnNullRestClientThrowsArgumentNullException()
        {
            // Arrange
            IRestClient nullRestClient = null;

            // Act

            // Assert
            Assert.Throws<ArgumentNullException>(() => nullRestClient.ExecuteAsync(CreateRestRequest(), CancellationToken.None));
        }

        [Fact]
        public void ExecuteAsyncWithNullRestRequestThrowsArgumentNullException()
        {
            // Arrange
            IRestRequest nullRestRequest = null;
            var restClient = new RestClient();

            // Act

            // Assert
            Assert.Throws<ArgumentNullException>(() => restClient.ExecuteAsync(nullRestRequest, CancellationToken.None));
        }

        private static RestRequest CreateRestRequest()
        {
            return new RestRequest(new Uri("/", UriKind.Relative));
        }
    }
}