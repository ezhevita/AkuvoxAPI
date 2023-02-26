using System.Text.Json.Serialization;

namespace AkuvoxAPI.Models;

public record ServerListRequest
{
	[JsonPropertyName("passwd")]
	public string Password { get; set; } = null!;

	[JsonPropertyName("token")]
	public string Token { get; set; } = null!;

	[JsonPropertyName("auth_token")]
	public string AuthToken { get; set; } = "";
}
