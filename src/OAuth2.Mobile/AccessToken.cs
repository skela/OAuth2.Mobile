﻿namespace StudioDonder.OAuth2.Mobile
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Validation;

    /// <summary>
    /// An OAuth access token.
    /// </summary>
    public class AccessToken
    {
        private const string TokenKey = "Token";
        private const string RefreshTokenKey = "RefreshToken";
        private const string ScopeKey = "Scope";
        private const string TokenTypeKey = "TokenType";
        private const string ExpirationDateKey = "ExpirationDate";

        /// <summary>
        /// Initializes a new instance of the <see cref="AccessToken"/> class.
        /// </summary>
        /// <param name="token">The token.</param>
        /// <param name="tokenType">Type of the token.</param>
        /// <param name="scope">The scope.</param>
        /// <param name="expirationDate">The expiration date.</param>
        public AccessToken(string token, string tokenType, string scope, DateTime? expirationDate)
            : this(token, tokenType, scope, expirationDate, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AccessToken" /> class.
        /// </summary>
        /// <param name="token">The token.</param>
        /// <param name="tokenType">Type of the token.</param>
        /// <param name="scope">The scope.</param>
        /// <param name="expirationDate">The expiration date.</param>
        /// <param name="refreshToken">The refresh token.</param>
        /// <exception cref="System.ArgumentNullException">
        /// token
        /// or
        /// scope
        /// or
        /// tokenType
        /// </exception>
        public AccessToken(string token, string tokenType, string scope, DateTime? expirationDate, string refreshToken)
        {
            Requires.NotNullOrEmpty(token, "token");
            Requires.NotNullOrEmpty(tokenType, "tokenType");

            this.Token = token;
            this.Scope = scope;
            this.TokenType = tokenType;
            this.ExpirationDate = expirationDate;
            this.RefreshToken = refreshToken;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AccessToken"/> class.
        /// </summary>
        /// <param name="dictionary">The dictionary.</param>
        public AccessToken(IDictionary<string, string> dictionary)
        {
            Requires.NotNull(dictionary, "dictionary");
            Requires.That(dictionary.ContainsKey(TokenKey), "dictionary", string.Format("The dictionary does not contain a value for the \"{0}\" key.", TokenKey));
            Requires.That(dictionary.ContainsKey(TokenTypeKey), "dictionary", string.Format("The dictionary does not contain a value for the \"{0}\" key.", TokenTypeKey));
            Requires.That(!string.IsNullOrEmpty(dictionary[TokenKey]), "dictionary", string.Format("The value for the \"{0}\" key must not be empty.", TokenKey));
            Requires.That(!string.IsNullOrEmpty(dictionary[TokenTypeKey]), "dictionary", string.Format("The value for the \"{0}\" key must not be empty.", TokenTypeKey));

            this.Token = dictionary[TokenKey];
            this.TokenType = dictionary[TokenTypeKey];
            this.Scope = dictionary.ContainsKey(ScopeKey) ? dictionary[ScopeKey] : null;
            this.RefreshToken = dictionary.ContainsKey(RefreshTokenKey) ? dictionary[RefreshTokenKey] : null;
            this.ExpirationDate = dictionary.ContainsKey(ExpirationDateKey) ? DateTime.Parse(dictionary[ExpirationDateKey]) : (DateTime?)null;
        }

        public string Token { get; private set; }
        public string Scope { get; private set; }
        public string TokenType { get; private set; }
        public DateTime? ExpirationDate { get; private set; }
        public string RefreshToken { get; private set; }

        public bool ShouldBeRefreshed(TimeSpan refreshTokenExpirationWindow)
        {
            if (this.ExpirationDate == null)
            {
                return false;
            }

            return this.ExpirationDate <= DateTime.Now || this.ExpirationDate - DateTime.Now <= refreshTokenExpirationWindow;
        }

        /// <summary>
        /// Convert this instance to a dictionary.
        /// </summary>
        /// <returns>The dictionary representation of this instance.</returns>
        public IDictionary<string, string> ToDictionary()
        {
            var dictionary = new Dictionary<string, string>
                                 {
                                     { TokenKey, this.Token },
                                     { RefreshTokenKey, this.RefreshToken },
                                     { ScopeKey, this.Scope },
                                     { TokenTypeKey, this.TokenType },
                                     { ExpirationDateKey, this.ExpirationDate.HasValue ? this.ExpirationDate.Value.ToString("s") : string.Empty },
                                 };

            return dictionary.Where(kv => kv.Value != null).ToDictionary(kv => kv.Key, kv => kv.Value);
        }
    }
}