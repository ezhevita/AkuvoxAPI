using System.Text.Json.Serialization;

namespace AkuvoxAPI.Models;

public record RestServerList
{
	[JsonPropertyName("rest_server")]
	public string RestServer { get; set; } = null!;

	[JsonPropertyName("rest_server_ipv6")]
	public string RestServerIPv6 { get; set; } = null!;

	[JsonPropertyName("rest_server_https")]
	public string RestServerHttps { get; set; } = null!;

	[JsonPropertyName("rest_server_https_ipv6")]
	public string RestServerHttpsIPv6 { get; set; } = null!;
}
