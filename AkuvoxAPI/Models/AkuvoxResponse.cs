using System.Text.Json.Serialization;

namespace AkuvoxAPI.Models;

public record AkuvoxResponse<T> : AkuvoxBaseResponse<T>
{
	[JsonPropertyName("message")]
	public string Message { get; set; } = null!;

	[JsonPropertyName("result")]
	public int Result { get; set; }

	public bool Success => Result == 0;
}
