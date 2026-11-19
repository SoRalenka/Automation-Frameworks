namespace ApiAutomationFramework.Tests.Models.Authentication;

public sealed record BearerAuthentication(string Token) : ApiAuthentication;