namespace StudioDonder.OAuth2.Mobile.Tests
{
    using System;

    using Xunit;

    public class SerializedAccessTokenTests
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
            var serializedAccessToken = CreateValidSerializedAccessToken();

            // Act
            serializedAccessToken.access_token = null;

            // Assert
            Assert.Throws<InvalidOperationException>(() => serializedAccessToken.ToAccessToken());
        }

        [Fact]
        public void ToAccessTokenWithNullTokenTypeThrowsInvalidOperationException()
        {
            // Arrange
            var serializedAccessToken = CreateValidSerializedAccessToken();

            // Act
            serializedAccessToken.token_type = null;

            // Assert
            Assert.Throws<InvalidOperationException>(() => serializedAccessToken.ToAccessToken());
        }

        [Fact]
        public void ToAccessTokenReturnsCorrectAccessToken()
        {
            // Arrange
            var serializedAccessToken = CreateValidSerializedAccessToken();

            // Act
            var accessToken = serializedAccessToken.ToAccessToken();

            // Assert
            Assert.Equal(Token, accessToken.Token);
            Assert.Equal(TokenType, accessToken.TokenType);
            Assert.Equal(Scope, accessToken.Scope);
            Assert.Equal(RefreshToken, accessToken.RefreshToken);
            Assert.True(DateTime.Now.Add(TimeSpan.FromSeconds(ExpiresIn)) - accessToken.ExpirationDate < TimeSpan.FromSeconds(DateTimeDifferenceInSecondsTolerance));
        }

        private static SerializedAccessToken CreateValidSerializedAccessToken()
        {
            return new SerializedAccessToken
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