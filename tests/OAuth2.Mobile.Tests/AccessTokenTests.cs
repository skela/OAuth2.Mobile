namespace StudioDonder.OAuth2.Mobile.Tests
{
    using System;
    using System.Collections.Generic;

    using StudioDonder.OAuth2.Mobile.Tests.Helpers;

    using Xunit;

    public class AccessTokenTests
    {
        private const int RefreshTokenExpirationWindow = 5;
        private const string TokenType = "bearer";
        private const string Scope = "test scope";
        private const string Token = "abcedfgh87aaaj12";
        private const string RefreshToken = "xyzuio234pmnqwe";
        private const string ExpirationDate = "2013-03-25T02:00:12";

        private static readonly DateTime? ExpirationDateValue = DateTime.Parse(ExpirationDate);

        [Fact]
        public void ConstructorWithNullTokenThrowsArgumentNullException()
        {
            // Arrange
            string nullToken = null;

            // Act

            // Assert
            Assert.Throws<ArgumentNullException>(() => new AccessToken(nullToken, TokenType, Scope, ExpirationDateValue, RefreshToken));
        }

        [Fact]
        public void ConstructorWithEmptyTokenThrowsArgumentException()
        {
            // Arrange
            var emptyToken = string.Empty;

            // Act

            // Assert
            Assert.Throws<ArgumentException>(() => new AccessToken(emptyToken, TokenType, Scope, ExpirationDateValue, RefreshToken));
        }

        [Fact]
        public void ConstructorWithNullTokenTypeThrowsArgumentNullException()
        {
            // Arrange
            string nullTokenType = null;

            // Act

            // Assert
            Assert.Throws<ArgumentNullException>(() => new AccessToken(Token, nullTokenType, Scope, ExpirationDateValue, RefreshToken));
        }

        [Fact]
        public void ConstructorWithEmptyTokenTypeThrowsArgumentNullException()
        {
            // Arrange
            var emptyTokenType = string.Empty;

            // Act

            // Assert
            Assert.Throws<ArgumentException>(() => new AccessToken(Token, emptyTokenType, Scope, ExpirationDateValue, RefreshToken));
        }

        [Fact]
        public void ConstructorWithNullScopeDoesNotThrowArgumentNullException()
        {
            // Arrange
            string scope = null;

            // Act

            // Assert
            Assert.DoesNotThrow(() => new AccessToken(Token, TokenType, scope, ExpirationDateValue, RefreshToken));
        }

        [Fact]
        public void ConstructorWithNullExpirationDateDoesNotThrowArgumentNullException()
        {
            // Arrange
            DateTime? expirationDate = null;

            // Act

            // Assert
            Assert.DoesNotThrow(() => new AccessToken(Token, TokenType, Scope, expirationDate, RefreshToken));
        }

        [Fact]
        public void ConstructorWithNullRefreshTokenDoesNotThrowArgumentNullException()
        {
            // Arrange
            string refreshToken = null;

            // Act

            // Assert
            Assert.DoesNotThrow(() => new AccessToken(Token, TokenType, Scope, ExpirationDateValue, refreshToken));
        }

        [Fact]
        public void ConstructorWithNullDictionaryThrowsArgumentNullException()
        {
            // Arrange
            IDictionary<string, string> dictionary = null;

            // Act

            // Assert
            Assert.Throws<ArgumentNullException>(() => new AccessToken(dictionary));
        }

        [Fact]
        public void ConstructorWithDictionaryWithEmptyTokenThrowsArgumentException()
        {
            // Arrange
            var dictionary = CreateValidDictionary();

            // Act
            dictionary["Token"] = string.Empty;

            // Assert
            Assert.Throws<ArgumentException>(() => new AccessToken(dictionary));
        }

        [Fact]
        public void ConstructorWithDictionaryWithEmptyTokenTypeThrowsArgumentException()
        {
            // Arrange
            var dictionary = CreateValidDictionary();

            // Act
            dictionary["TokenType"] = string.Empty;

            // Assert
            Assert.Throws<ArgumentException>(() => new AccessToken(dictionary));
        }

        [Fact]
        public void ConstructorWithDictionaryWithoutTokenKeyThrowsArgumentException()
        {
            // Arrange
            var dictionary = CreateValidDictionary();

            // Act
            dictionary.Remove("Token");

            // Assert
            Assert.Throws<ArgumentException>(() => new AccessToken(dictionary));
        }

        [Fact]
        public void ConstructorWithDictionaryWithoutTokenTypeKeyThrowsArgumentException()
        {
            // Arrange
            var dictionary = CreateValidDictionary();

            // Act
            dictionary.Remove("TokenType");

            // Assert
            Assert.Throws<ArgumentException>(() => new AccessToken(dictionary));
        }

        [Fact]
        public void ConstructorWithDictionaryWithoutScopeKeyDoesNotThrowArgumentException()
        {
            // Arrange
            var dictionary = CreateValidDictionary();

            // Act
            dictionary.Remove("Scope");

            // Assert
            Assert.DoesNotThrow(() => new AccessToken(dictionary));
        }

        [Fact]
        public void ConstructorWithDictionaryWithoutRefreshTokenKeyDoesNotThrowArgumentException()
        {
            // Arrange
            var dictionary = CreateValidDictionary();

            // Act
            dictionary.Remove("RefreshTokenKey");

            // Assert
            Assert.DoesNotThrow(() => new AccessToken(dictionary));
        }

        [Fact]
        public void ConstructorWithDictionaryWithoutExpirationDateKeyDoesNotThrowArgumentException()
        {
            // Arrange
            var dictionary = CreateValidDictionary();

            // Act
            dictionary.Remove("ExpirationDate");

            // Assert
            Assert.DoesNotThrow(() => new AccessToken(dictionary));
        }

        [Fact]
        public void ConstructorWithDictionaryReturnsInitializedAccessToken()
        {
            // Arrange
            var dictionary = CreateValidDictionary();

            // Act
            var accessToken = new AccessToken(dictionary);

            // Assert
            Assert.Equal(dictionary, accessToken.ToDictionary(), new StringKeyValuePairEqualityComparer());
        }

        [Fact]
        public void ShouldBeRefreshedWithAccessTokenThatHasNoExpirationDateReturnsFalse()
        {
            // Arrange
            var accessToken = CreateAccessToken(null);

            // Act
            var tokenShouldBeRefreshed = accessToken.ShouldBeRefreshed(TimeSpan.FromMinutes(RefreshTokenExpirationWindow));

            // Assert
            Assert.False(tokenShouldBeRefreshed);
        }

        [Fact]
        public void ShouldBeRefreshedWithAccessTokenExpirationDateIsFutureDateNotWithinExpirationWindowReturnsFalse()
        {
            // Arrange
            var accessToken = CreateAccessToken(DateTime.Now.AddDays(2));

            // Act
            var tokenShouldBeRefreshed = accessToken.ShouldBeRefreshed(TimeSpan.FromMinutes(RefreshTokenExpirationWindow));

            // Assert
            Assert.False(tokenShouldBeRefreshed);
        }

        [Fact]
        public void ShouldBeRefreshedWithAccessTokenExpirationDateIsFutureDateWithinExpirationWindowReturnsTrue()
        {
            // Arrange
            var accessToken = CreateAccessToken(DateTime.Now.AddMinutes(2));

            // Act
            var tokenShouldBeRefreshed = accessToken.ShouldBeRefreshed(TimeSpan.FromMinutes(RefreshTokenExpirationWindow));

            // Assert
            Assert.True(tokenShouldBeRefreshed);
        }

        [Fact]
        public void ShouldBeRefreshedWithAccessTokenThatHasExpiredReturnsTrue()
        {
            // Arrange
            var accessToken = CreateAccessToken(DateTime.Now.AddDays(-2));

            // Act
            var tokenShouldBeRefreshed = accessToken.ShouldBeRefreshed(TimeSpan.FromMinutes(RefreshTokenExpirationWindow));

            // Assert
            Assert.True(tokenShouldBeRefreshed);
        }

        [Fact]
        public void ToDictionaryReturnsDictionaryWithCorrectValues()
        {
            // Arrange
            var dictionary = CreateValidDictionary();

            // Act
            var accessToken = new AccessToken(dictionary);

            // Assert
            Assert.Equal(dictionary, accessToken.ToDictionary(), new StringKeyValuePairEqualityComparer());
        }

        [Fact]
        public void ToDictionaryDoesNotReturnNullValues()
        {
            // Arrange
            var dictionary = CreateValidDictionary();
            dictionary.Remove("RefreshToken");
            dictionary.Remove("ExpirationDate");

            // Act
            var accessToken = new AccessToken(dictionary);
            var accessTokenDictionary = accessToken.ToDictionary();

            // Assert
            var expectedDictionary = new Dictionary<string, string>
                                  {
                                      { "Token", accessToken.Token }, 
                                      { "TokenType", accessToken.TokenType },
                                      { "Scope", accessToken.Scope },
                                  };

            Assert.Equal(expectedDictionary, accessTokenDictionary, new StringKeyValuePairEqualityComparer());
        }

        private static AccessToken CreateAccessToken(DateTime? expirationDate)
        {
            return new AccessToken(Token, TokenType, Scope, expirationDate);
        }

        private static Dictionary<string, string> CreateValidDictionary()
        {
            return new Dictionary<string, string>
                       {
                           { "Token", Token },
                           { "TokenType", TokenType },
                           { "Scope", Scope },
                           { "ExpirationDate", ExpirationDate },
                           { "RefreshToken", RefreshToken },
                       };
        }
    }
}