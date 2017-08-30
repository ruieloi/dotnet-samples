# auth0-managementAPI-webapi

API consuming Auth0 Management API

## Dependencies

Auth0.Management.API 4.4 - Nuget Package
Auth0.Authentication.API 4.2 - Nuget Package

## Before Getting started

- Auth0 account
- Create a Non Interactive Client for Auth0 Management API 
- Add all scopes to the client
- Go to tab "Test" in Auht0 Management API and copy the Token for the client you created.
- Update app.settings auth0 data:
	"Auth0": {
		"Domain": "",
		"ClientId": "",
		"ClientSecret": ""
	  }

## Start

- dotnet restore
- dotnet run
