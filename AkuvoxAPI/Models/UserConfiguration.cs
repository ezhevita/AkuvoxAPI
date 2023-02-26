using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace AkuvoxAPI.Models;

public record UserConfiguration
{
	[JsonPropertyName("app_conf")]
	public AppConfiguration ApplicationConfiguration { get; set; } = null!;

	[JsonPropertyName("dev_list")]
	public IReadOnlyCollection<Device> DevicesList { get; set; } = null!;

	[JsonPropertyName("third_party_dev_list")]
	public ThirdPartyDevicesConfiguration ThirdPartyDevicesConfiguration { get; set; } = null!;

	[JsonPropertyName("unread_msg")]
	public UnreadMessages UnreadMessages { get; set; } = null!;
}
