namespace StudioDonder.OAuth2.Mobile.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    using StudioDonder.OAuth2.Mobile.Tests.Helpers;

    using Xamarin.Auth;

    using Xunit;

    public class AccessTokenStoreTests
    {
        private const string Username = "test username";
        private const string ClientId = "test client id";
        private const string ServiceId = "test service id";

        private const string TokenType = "bearer";
        private const string Scope = "test scope";
        private const string Token = "abcedfgh87aaaj12";
        private const string RefreshToken = "xyzuio234pmnqwe";
        private const string ExpirationDate = "2013-03-25T02:00:12";

        [Fact]
        public void ConstructorWithNullAccountStoreThrowsArgumentNullException()
        {
            // Arrange
            AccountStore nullAccountStore = null;

            // Act

            // Assert
            Assert.Throws<ArgumentNullException>(() => new AccessTokenStore(nullAccountStore));
        }

        [Fact]
        public void GetUserAccessTokenWithNullUsernameThrowsArgumentNullException()
        {
            // Arrange
            string nullUsername = null;
            var accessTokenStore = CreateAccessTokenStore();

            // Act

            // Assert
            Assert.Throws<ArgumentNullException>(() => accessTokenStore.GetUserAccessToken(nullUsername, ServiceId, CancellationToken.None));
        }

        [Fact]
        public void GetUserAccessTokenWithEmptyUsernameThrowsArgumentException()
        {
            // Arrange
            var emptyUsername = string.Empty;
            var accessTokenStore = CreateAccessTokenStore();

            // Act

            // Assert
            Assert.Throws<ArgumentException>(() => accessTokenStore.GetUserAccessToken(emptyUsername, ServiceId, CancellationToken.None));
        }

        [Fact]
        public void GetUserAccessTokenWithNullServiceIdThrowsArgumentNullException()
        {
            // Arrange
            string nullServiceId = null;
            var accessTokenStore = CreateAccessTokenStore();

            // Act

            // Assert
            Assert.Throws<ArgumentNullException>(() => accessTokenStore.GetUserAccessToken(Username, nullServiceId, CancellationToken.None));
        }

        [Fact]
        public void GetUserAccessTokenWithEmptyServiceIdThrowsArgumentException()
        {
            // Arrange
            var emptyServiceId = string.Empty;
            var accessTokenStore = CreateAccessTokenStore();

            // Act

            // Assert
            Assert.Throws<ArgumentException>(() => accessTokenStore.GetUserAccessToken(Username, emptyServiceId, CancellationToken.None));
        }

        [Fact]
        public void GetUserAccessTokenReturnsAccessTokenForAccountBelongingToSpecifiedUsernameAndServiceIdCombination()
        {
            // Arrange
            var account = CreateUserAccount();
            var accessTokenStore = CreateAccessTokenStore(new[] { account });

            // Act
            var task = accessTokenStore.GetUserAccessToken(Username, ServiceId, CancellationToken.None);
            task.Wait();

            // Assert
            Assert.Equal(new AccessToken(account.Properties), task.Result, new AccessTokenEqualityComparer());
        }

        [Fact]
        public void GetUserAccessTokenMatchesUsernameCaseInsensitive()
        {
            // Arrange
            var accessTokenStore = CreateAccessTokenStore(new[] { CreateUserAccount() });

            // Act
            var task = accessTokenStore.GetUserAccessToken(Username.ToUpperInvariant(), ServiceId, CancellationToken.None);
            task.Wait();

            // Assert
            Assert.NotNull(task.Result);
        }

        [Fact]
        public void GetClientAccessTokenWithNullClientIdThrowsArgumentNullException()
        {
            // Arrange
            string nullClientId = null;
            var accessTokenStore = CreateAccessTokenStore();

            // Act

            // Assert
            Assert.Throws<ArgumentNullException>(() => accessTokenStore.GetClientAccessToken(nullClientId, ServiceId, CancellationToken.None));
        }

        [Fact]
        public void GetClientAccessTokenWithEmptyClientIdThrowsArgumentException()
        {
            // Arrange
            var emptyClientId = string.Empty;
            var accessTokenStore = CreateAccessTokenStore();

            // Act

            // Assert
            Assert.Throws<ArgumentException>(() => accessTokenStore.GetClientAccessToken(emptyClientId, ServiceId, CancellationToken.None));
        }

        [Fact]
        public void GetClientAccessTokenWithNullServiceIdThrowsArgumentNullException()
        {
            // Arrange
            string nullServiceId = null;
            var accessTokenStore = CreateAccessTokenStore();

            // Act

            // Assert
            Assert.Throws<ArgumentNullException>(() => accessTokenStore.GetClientAccessToken(ClientId, nullServiceId, CancellationToken.None));
        }

        [Fact]
        public void GetClientAccessTokenWithEmptyServiceIdThrowsArgumentException()
        {
            // Arrange
            var emptyServiceId = string.Empty;
            var accessTokenStore = CreateAccessTokenStore();

            // Act

            // Assert
            Assert.Throws<ArgumentException>(() => accessTokenStore.GetClientAccessToken(ClientId, emptyServiceId, CancellationToken.None));
        }

        [Fact]
        public void GetClientAccessTokenReturnsAccessTokenForAccountBelongingToSpecifiedClientIdAndServiceIdCombination()
        {
            // Arrange
            var account = CreateClientAccount();
            var accessTokenStore = CreateAccessTokenStore(clientAccounts: new[] { account });

            // Act
            var task = accessTokenStore.GetClientAccessToken(ClientId, ServiceId, CancellationToken.None);
            task.Wait();

            // Assert
            Assert.Equal(new AccessToken(account.Properties), task.Result, new AccessTokenEqualityComparer());
        }

        [Fact]
        public void GetClientAccessTokenMatchesClientIdCaseInsensitive()
        {
            // Arrange
            var accessTokenStore = CreateAccessTokenStore(clientAccounts: new[] { CreateClientAccount() });

            // Act
            var task = accessTokenStore.GetClientAccessToken(ClientId.ToUpperInvariant(), ServiceId, CancellationToken.None);
            task.Wait();

            // Assert
            Assert.NotNull(task.Result);
        }

        [Fact]
        public void SaveUserAccessTokenWithNullUsernameThrowsArgumentNullException()
        {
            // Arrange
            string nullUsername = null;
            var accessTokenStore = CreateAccessTokenStore();

            // Act

            // Assert
            Assert.Throws<ArgumentNullException>(() => accessTokenStore.SaveUserAccessToken(nullUsername, ServiceId, CreateAccessToken(), CancellationToken.None));
        }

        [Fact]
        public void SaveUserAccessTokenWithEmptyUsernameThrowsArgumentException()
        {
            // Arrange
            var emptyUsername = string.Empty;
            var accessTokenStore = CreateAccessTokenStore();

            // Act

            // Assert
            Assert.Throws<ArgumentException>(() => accessTokenStore.SaveUserAccessToken(emptyUsername, ServiceId, CreateAccessToken(), CancellationToken.None));
        }

        [Fact]
        public void SaveUserAccessTokenWithNullServiceIdThrowsArgumentNullException()
        {
            // Arrange
            string nullServiceId = null;
            var accessTokenStore = CreateAccessTokenStore();

            // Act

            // Assert
            Assert.Throws<ArgumentNullException>(() => accessTokenStore.SaveUserAccessToken(Username, nullServiceId, CreateAccessToken(), CancellationToken.None));
        }

        [Fact]
        public void SaveUserAccessTokenWithEmptyServiceIdThrowsArgumentException()
        {
            // Arrange
            var emptyServiceId = string.Empty;
            var accessTokenStore = CreateAccessTokenStore();

            // Act

            // Assert
            Assert.Throws<ArgumentException>(() => accessTokenStore.SaveUserAccessToken(Username, emptyServiceId, CreateAccessToken(), CancellationToken.None));
        }

        [Fact]
        public void SaveUserAccessTokenWithNullAccessTokenThrowsArgumentNullException()
        {
            // Arrange
            AccessToken nullAccessToken = null;
            var accessTokenStore = CreateAccessTokenStore();

            // Act

            // Assert
            Assert.Throws<ArgumentNullException>(() => accessTokenStore.SaveUserAccessToken(Username, ServiceId, nullAccessToken, CancellationToken.None));
        }

        [Fact]
        public void SaveUserAccessTokenStoresAccessTokenInStore()
        {
            // Arrange
            var accessTokenStore = CreateAccessTokenStore();
            var accessToken = CreateAccessToken();

            // Act
            var task = accessTokenStore.SaveUserAccessToken(Username, ServiceId, accessToken, CancellationToken.None);
            task.Wait();

            var getUserAccessTokenTask = accessTokenStore.GetUserAccessToken(Username, ServiceId, CancellationToken.None);
            getUserAccessTokenTask.Wait();

            // Assert
            Assert.Equal(accessToken, getUserAccessTokenTask.Result, new AccessTokenEqualityComparer());
        }

        [Fact]
        public void SaveClientAccessTokenWithNullClientIdThrowsArgumentNullException()
        {
            // Arrange
            string nullClientId = null;
            var accessTokenStore = CreateAccessTokenStore();

            // Act

            // Assert
            Assert.Throws<ArgumentNullException>(() => accessTokenStore.SaveClientAccessToken(nullClientId, ServiceId, CreateAccessToken(), CancellationToken.None));
        }

        [Fact]
        public void SaveClientAccessTokenWithEmptyClientIdThrowsArgumentException()
        {
            // Arrange
            var emptyClientId = string.Empty;
            var accessTokenStore = CreateAccessTokenStore();

            // Act

            // Assert
            Assert.Throws<ArgumentException>(() => accessTokenStore.SaveClientAccessToken(emptyClientId, ServiceId, CreateAccessToken(), CancellationToken.None));
        }

        [Fact]
        public void SaveClientAccessTokenWithNullServiceIdThrowsArgumentNullException()
        {
            // Arrange
            string nullServiceId = null;
            var accessTokenStore = CreateAccessTokenStore();

            // Act

            // Assert
            Assert.Throws<ArgumentNullException>(() => accessTokenStore.SaveClientAccessToken(ClientId, nullServiceId, CreateAccessToken(), CancellationToken.None));
        }

        [Fact]
        public void SaveClientAccessTokenWithEmptyServiceIdThrowsArgumentException()
        {
            // Arrange
            var emptyServiceId = string.Empty;
            var accessTokenStore = CreateAccessTokenStore();

            // Act

            // Assert
            Assert.Throws<ArgumentException>(() => accessTokenStore.SaveClientAccessToken(ClientId, emptyServiceId, CreateAccessToken(), CancellationToken.None));
        }

        [Fact]
        public void SaveClientAccessTokenWithNullAccessTokenThrowsArgumentNullException()
        {
            // Arrange
            AccessToken nullAccessToken = null;
            var accessTokenStore = CreateAccessTokenStore();

            // Act

            // Assert
            Assert.Throws<ArgumentNullException>(() => accessTokenStore.SaveClientAccessToken(ClientId, ServiceId, nullAccessToken, CancellationToken.None));
        }

        [Fact]
        public void SaveClientAccessTokenStoresAccessTokenInStore()
        {
            // Arrange
            var accessTokenStore = CreateAccessTokenStore();
            var accessToken = CreateAccessToken();

            // Act
            var task = accessTokenStore.SaveClientAccessToken(ClientId, ServiceId, accessToken, CancellationToken.None);
            task.Wait();

            var getUserAccessTokenTask = accessTokenStore.GetClientAccessToken(ClientId, ServiceId, CancellationToken.None);
            getUserAccessTokenTask.Wait();

            // Assert
            Assert.Equal(accessToken, getUserAccessTokenTask.Result, new AccessTokenEqualityComparer());
        }

        [Fact]
        public void UserAndClientWithSameNameCanBeStoredNextToEachOther()
        {
            // Arrange
            var accessTokenStore = CreateAccessTokenStore();
            var userAccessToken = CreateAccessToken();
            var clientAccessToken = CreateDifferentAccessToken();
            var sharedName = "shared name";

            // Act
            var saveUserAccessTokenTask = accessTokenStore.SaveUserAccessToken(sharedName, ServiceId, userAccessToken, CancellationToken.None);
            saveUserAccessTokenTask.Wait();

            var saveClientAccessTokenTask = accessTokenStore.SaveClientAccessToken(sharedName, ServiceId, clientAccessToken, CancellationToken.None);
            saveClientAccessTokenTask.Wait();

            var getClientAccessTokenTask = accessTokenStore.GetClientAccessToken(sharedName, ServiceId, CancellationToken.None);
            getClientAccessTokenTask.Wait();

            var getUserAccessTokenTask = accessTokenStore.GetUserAccessToken(sharedName, ServiceId, CancellationToken.None);
            getUserAccessTokenTask.Wait();

            // Assert
            Assert.Equal(userAccessToken, getUserAccessTokenTask.Result, new AccessTokenEqualityComparer());
            Assert.Equal(clientAccessToken, getClientAccessTokenTask.Result, new AccessTokenEqualityComparer());
        }

        private static Account CreateUserAccount()
        {
            return new Account(Username, CreatePropertiesDictionary());
        }

        private static Account CreateClientAccount()
        {
            return new Account(ClientId, CreatePropertiesDictionary());
        }

        private static AccessTokenStore CreateAccessTokenStore(IEnumerable<Account> userAccounts = null, IEnumerable<Account> clientAccounts = null)
        {
            var accessTokenStore = new AccessTokenStore(new InMemoryAccountStore());

            if (userAccounts != null)
            {
                var userTasks = userAccounts.Select(a => accessTokenStore.SaveUserAccessToken(a.Username, ServiceId, new AccessToken(a.Properties), CancellationToken.None)).ToArray();
                Task.WaitAll(userTasks);
            }

            if (clientAccounts != null)
            {
                var clientTasks = clientAccounts.Select(a => accessTokenStore.SaveClientAccessToken(a.Username, ServiceId, new AccessToken(a.Properties), CancellationToken.None)).ToArray();
                Task.WaitAll(clientTasks);
            }

            return accessTokenStore;
        }

        private static AccessToken CreateAccessToken()
        {
            return new AccessToken(CreatePropertiesDictionary());
        }

        private static AccessToken CreateDifferentAccessToken()
        {
            var propertiesDictionary = CreatePropertiesDictionary();
            propertiesDictionary["Token"] = "different token";
            propertiesDictionary["RefreshToken"] = "different refresh token";
            propertiesDictionary["Scope"] = "different scope";

            return new AccessToken(propertiesDictionary);
        }

        private static Dictionary<string, string> CreatePropertiesDictionary()
        {
            return new Dictionary<string, string>
                       {
                           { "Token", Token },
                           { "RefreshToken", RefreshToken },
                           { "Scope", Scope },
                           { "TokenType", TokenType },
                           { "ExpirationDate", ExpirationDate },
                       };
        }
    }
}