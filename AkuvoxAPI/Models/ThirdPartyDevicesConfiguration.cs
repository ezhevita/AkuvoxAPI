using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace AkuvoxAPI.Models;

public record ThirdPartyDevicesConfiguration
{
	[JsonPropertyName("lock_dev_list")]
	public IReadOnlyCollection<object> LockDevicesList { get; set; } = null!;

	[JsonPropertyName("camera_dev_list")]
	public IReadOnlyCollection<object> CameraDevicesList { get; set; } = null!;

	[JsonPropertyName("bsi_lock_list")]
	public IReadOnlyCollection<object> BsiLockList { get; set; } = null!;
}
