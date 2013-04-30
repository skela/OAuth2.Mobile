# OAuth2.Mobile

## Introduction

A library built to easily access OAuth 2 servers using two-legged authentication on the [Xamarin.iOS](http://xamarin.com/ios) and [Xamarin.Android](http://xamarin.com/monoforandroid) platforms. For these platforms the [Xamarin.Auth](https://github.com/xamarin/Xamarin.Auth) library offers support for three-legged authentication. Our library is built on top of Xamarin.Auth to also add two-legged authentication support. This library is built to integrate seamlessly with the [Task Parallel Library](http://msdn.microsoft.com/en-us/library/dd460717.aspx), so most methods return `Task<T>` instances.

# Usage

Using this library starts with instantiating an instance of the `AccessTokenClient` class, which constructor requires an `OAuthServerConfiguration` instance as its parameter:

	var oauthServerConfiguration = new OAuthServerConfiguration(new Uri("https://my-oauthserver.com"), new Uri("/tokens", UriKind.Relative), "my-client-id", "my-client-secret");
    var accessTokenClient = new AccessTokenClient(oauthServerConfiguration);

We see that the `OAuthServerConfiguration` constructor requires four parameters to be passed:

1. The base url of the OAuth server.
2. The url at which token requests will be made. Note: this url is relative to the base url.
3. The client id.
4. The client secret.

With this information, we have enough information to request a client access token using the `GetClientAccessToken` method:

	var clientAccessTokenTask = accessTokenClient.GetClientAccessToken("my-client-scope", CancellationToken.None);
	var clientAccessToken = clientAccessTokenTask.Result;

One can see that this parameter takes the scope requested as a parameter, as well as a `CancellationToken` instance (which can be used to cancel the request).
If the requested scope is valid for the specified client id, an `AccessToken` is returned. This class contain the actual access token, as well as the token type, authorized scope and an expiration date.

Requesting a user access token is similar:

	var userAccessTokenTask = accessTokenClient.GetUserAccessToken(Username, Password, UserScope, CancellationToken.None);
    var userAccessToken = userAccessTokenTask.Result;

The returned instance is once again a `Task<AccessToken>` instance, from which we can retrieve the access token. The one difference with the access token retrieved from the client is that a refresh token is also returned. This token can be used to easily refresh an (expired) user access token. You do this with the `RefreshToken` method:

	var refreshTokenTask = accessTokenClient.RefreshToken(userAccessTokenTask.Result.RefreshToken, CancellationToken.None);
    var refreshedUserAccessToken = refreshTokenTask.Result;

## Libraries

This library is built on top of these three fantastic libraries:

1. [RestSharp](http://restsharp.org/): we use this library to do our (asynchronous) web-requests.
2. [Xamarin.Auth](https://github.com/xamarin/Xamarin.Auth): we use the secure stores defined in this library to store our tokens in.
3. [Validation](https://github.com/AArnott/Validation): this library is used to do all our sanity checks (such as _NULL_ argument checks).

## Building

To use the library in your application, you need to build it yourself on the platform of your choice. 

1. The first step is to get the source on your machine, which you can do using:

git clone https://github.com/ErikSchierboom/OAuth2.Mobile.git

2. The second step is to open the solution for the platform you need to build the library for. This can be either Xamarin.iOS ([OAuth2.Mobile.iOS.sln](OAuth2.Mobile.iOS.sln) solution) or Xamarin.Android ([OAuth2.Mobile.Android.sln](OAuth2.Mobile.Android.sln)). 

3. The third step is to build the solution (use the **Release** build). Then you will have four files in your bin directory:
	* OAuth2.Mobile.(iOS|Android).dll
    * RestSharp.dll
    * Validation.dll
    * Xamarin.Auth.dll

4. The last step is to add the four DLL's just mentioned as references to your application. You are now ready to use the OAuth.Mobile library.

## License
[Apache License 2.0](LICENSE.md)