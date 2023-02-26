using System.Text.Json.Serialization;

namespace AkuvoxAPI.Models;

public record AkuvoxBaseResponse<T>
{
	[JsonPropertyName("datas")]
	public T Data { get; set; } = default!;
}
