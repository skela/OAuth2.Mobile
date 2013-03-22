namespace StudioDonder.OAuth2.Mobile.Tests
{
    using System;

    using Xunit;

    public class AccessTokenResponseTests
    {
        private const string TokenType = "bearer";
        private const string Scope = "test scope";
        private const string Token = "abcedfgh87aaaj12";
        private const string RefreshToken = "xyzuio234pmnqwe";
        private const int ExpiresIn = 3600;
        private const int DateTimeDifferenceInSecondsTolerance = 5;

        [Fact]
        public void ToAccessTokenWithNullAccessTokenThrowsInvalidOperationException()
        {
            // Arrange
            var accessTokenResponse = CreateValidAccessTokenResponse();

            // Act
            accessTokenResponse.access_token = null;

            // Assert
            Assert.Throws<InvalidOperationException>(() => accessTokenResponse.ToAccessToken());
        }

        [Fact]
        public void ToAccessTokenWithNullTokenTypeThrowsInvalidOperationException()
        {
            // Arrange
            var accessTokenResponse = CreateValidAccessTokenResponse();

            // Act
            accessTokenResponse.token_type = null;

            // Assert
            Assert.Throws<InvalidOperationException>(() => accessTokenResponse.ToAccessToken());
        }

        [Fact]
        public void ToAccessTokenReturnsCorrectAccessToken()
        {
            // Arrange
            var accessTokenResponse = CreateValidAccessTokenResponse();

            // Act
            var accessToken = accessTokenResponse.ToAccessToken();

            // Assert
            Assert.Equal(Token, accessToken.Token);
            Assert.Equal(TokenType, accessToken.TokenType);
            Assert.Equal(Scope, accessToken.Scope);
            Assert.Equal(RefreshToken, accessToken.RefreshToken);
            Assert.True(DateTime.Now.Add(TimeSpan.FromSeconds(ExpiresIn)) - accessToken.ExpirationDate < TimeSpan.FromSeconds(DateTimeDifferenceInSecondsTolerance));
        }

        private static AccessTokenResponse CreateValidAccessTokenResponse()
        {
            return new AccessTokenResponse
                       {
                           access_token = Token,
                           refresh_token = RefreshToken,
                           scope = Scope,
                           token_type = TokenType,
                           expires_in = ExpiresIn
                       };
        }
    }
}