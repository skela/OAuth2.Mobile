# OAuth2.Mobile

## Introduction

A library built to easily interact with OAuth 2 ([official spec](http://tools.ietf.org/html/rfc6749)) servers using two-legged authentication on the [Xamarin.iOS](http://xamarin.com/ios) and [Xamarin.Android](http://xamarin.com/monoforandroid) platforms. For these platforms the [Xamarin.Auth](https://github.com/xamarin/Xamarin.Auth) library offers support for three-legged authentication. Our library is built on top of Xamarin.Auth to also add two-legged authentication support. This library is built to integrate seamlessly with the [Task Parallel Library](http://msdn.microsoft.com/en-us/library/dd460717.aspx), so most methods return `Task<T>` instances.

# Usage

Using this library starts with instantiating an instance of the `AccessTokenClient` class, which constructor requires an `OAuthServerConfiguration` instance as its parameter:

	var oauthServerConfiguration = new OAuthServerConfiguration(new Uri("https://my-oauthserver.com"), new Uri("/tokens", UriKind.Relative), "my client id", "my client secret");
    var accessTokenClient = new AccessTokenClient(oauthServerConfiguration);

We see that the `OAuthServerConfiguration` constructor requires four parameters to be passed:

1. The base url of the OAuth server.
2. The url at which token requests will be made. Note: this url is relative to the base url.
3. The client id.
4. The client secret.

Now that we have created a `AccessTokenClient` instance, we are ready to start requesting access tokens. There are three two-legged authentication workflows, each of which is implemented as a separate method in the `AccessTokenClient` class. As we have built our library to be compatible with the TPL, each method will return a `Task<AccessToken>` instance. We also have an overload for each method that takes a cancellation token, to allow cancellation of requests. Let's move on to the three workflows.

## Workflow 1: client credentials grant workflow
This first workflow ([official spec](http://tools.ietf.org/html/rfc6749#section-4.4)) is for clients only, which means that there is no user involved. Instead, this workflow depends on the combination of client id and client secret for its authentication (which were provided in the `OAuthServerConfiguration` argument of the `AccessTokenClient`). You can request a client access token using the `GetClientAccessToken` method:

	var clientAccessTokenTask = accessTokenClient.GetClientAccessToken("my client scope");
	var clientAccessToken = clientAccessTokenTask.Result;

If the requested scope is valid for the specified client id, an `AccessToken` is returned as the result of the task. The `AccessToken` class contains the actual access token, token type, authorized scope and an expiration date.

## Workflow 2: resource owner password credentials grant workflow
The second workflow ([official spec](http://tools.ietf.org/html/rfc6749#section-4.3)) deals with users requesting a token to access a protected resource. This workflow requires a user to submit his username and password, as well as the requested scope. The `GetUserAccessToken` implements this workflow:

	var userAccessTokenTask = accessTokenClient.GetUserAccessToken("my-username", "my password", "my user scope");
    var userAccessToken = userAccessTokenTask.Result;

The returned instance is once again a `Task<AccessToken>` instance, from which we can retrieve the access token. The one difference with the access token retrieved from the client is that the access token will have its  `RefreshToken` set. This token can be used to easily refresh an (expired) user access token. This leads us to the third and last workflow.

## Workflow 3: refresh access token workflow
This workflow ([official spec](http://tools.ietf.org/html/rfc6749#section-6)) deals with refreshing an expired access token. Of course when an access token expires, one could just go through the same workflow again. However, refreshing an access token requested through workflow 2 can be simplified even more. All you have to do is to call the `RefreshToken` method with the `RefreshToken` property of the access token returned by the `GetUserAccessToken` method:

	var refreshTokenTask = accessTokenClient.RefreshToken(userAccessToken.RefreshToken);
    var refreshedUserAccessToken = refreshTokenTask.Result;

The result is a new brand new `AccessToken` instance that will not be expired. Using this workflow to refresh access tokens has the advantage that you don't have to bother with resending the username/password/scope combination again.

## Libraries

This library is built on top of these three fantastic libraries:

1. [RestSharp](http://restsharp.org/): we use this library to do our (asynchronous) web-requests.
2. [Xamarin.Auth](https://github.com/xamarin/Xamarin.Auth): we use the secure stores defined in this library to store our tokens in.
3. [Validation](https://github.com/AArnott/Validation): this library is used to do all our sanity checks (such as _NULL_ argument checks).

## Building

To use the library in your application, you need to build it yourself on the platform of your choice. 

1. The first step is to get the source on your machine, which you can do using:

git clone https://github.com/ErikSchierboom/OAuth2.Mobile.git

2. The second step is to open the solution for the platform you need to build the library for. This can be either Xamarin.iOS (OAuth2.Mobile.iOS.sln) or Xamarin.Android (OAuth2.Mobile.Android.sln). 

3. The third step is to build the solution (use the **Release** build). Then you will have four files in your bin directory:
	* OAuth2.Mobile.(iOS|Android).dll
    * RestSharp.dll
    * Validation.dll
    * Xamarin.Auth.dll

4. The last step is to add the four DLL's just mentioned as references to your application. You are now ready to use the OAuth.Mobile library.

## License
[Apache License 2.0](LICENSE.md)