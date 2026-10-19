using System.Text.Json;
using System.Text.Json.Serialization;

namespace ApiAutomationFramework.Tests.Models;

public class PostResponse
{
    [JsonPropertyName("json")]
    public PostResponseData? Json { get; set; }
}