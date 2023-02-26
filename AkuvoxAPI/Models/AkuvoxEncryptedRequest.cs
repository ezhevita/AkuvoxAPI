using System.Text.Json.Serialization;

namespace AkuvoxAPI.Models;

public record AkuvoxEncryptedRequest : AkuvoxBaseResponse<string>
{
	[JsonPropertyName("user")]
	public string User { get; set; } = null!;
}
