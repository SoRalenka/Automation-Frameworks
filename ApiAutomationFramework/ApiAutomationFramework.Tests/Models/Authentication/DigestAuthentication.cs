namespace ApiAutomationFramework.Tests.Models.Authentication;

public sealed record DigestAuthentication(string Username,string Password): ApiAuthentication;