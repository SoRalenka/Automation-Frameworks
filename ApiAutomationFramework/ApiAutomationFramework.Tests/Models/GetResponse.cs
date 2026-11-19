namespace ApiAutomationFramework.Tests.Models;

public class GetResponse
{
    public Dictionary<string, string> Args { get; set; } = new();
    public Dictionary<string, string> Headers { get; set; } = new();
    public string Origin { get; set; } = string.Empty;
    public string Url { get; set; } = string.Empty;
}