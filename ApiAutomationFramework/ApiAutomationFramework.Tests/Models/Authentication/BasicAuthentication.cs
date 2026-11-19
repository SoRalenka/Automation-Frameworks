namespace ApiAutomationFramework.Tests.Models.Authentication;

public sealed record BasicAuthentication(string Username, string Password) : ApiAuthentication;