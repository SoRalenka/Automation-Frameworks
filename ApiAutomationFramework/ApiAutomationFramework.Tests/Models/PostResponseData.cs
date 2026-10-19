using System.Text.Json.Serialization;

namespace ApiAutomationFramework.Tests.Models;

public class PostResponseData
{
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("role")]
    public string Role { get; set; } = string.Empty;
}