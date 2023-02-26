using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using AkuvoxAPI.Models;
using Flurl.Http;
using Flurl.Serialization.TextJson;

namespace AkuvoxAPI;

[SuppressMessage("Design", "CA1001:Types that own disposable fields should be disposable")]
public sealed class AkuvoxClient
{
	private readonly string _token;
	private readonly IFlurlClient _httpClient;

	internal AkuvoxClient(string baseUrl, string token)
	{
		_token = token;
		_httpClient = new FlurlClient(baseUrl);
		_httpClient.Configure(settings => settings.WithTextJsonSerializer());
	}

	public async Task OpenDoor(int relay, string macAddress)
	{
		var response = await _httpClient.Request("opendoor")
			.SetQueryParam("token", _token)
			.WithHeader("api-version", "4.3")
			.WithHeader("x-auth-token", _token)
			.PostUrlEncodedAsync(
				new
				{
					relay,
					mac = macAddress
				}
			)
			.ReceiveJson<AkuvoxResponse<object[]>>();

		response.ValidateResponse();
	}

	public async Task<UserConfiguration> GetUserConfiguration()
	{
		var response = await _httpClient.Request("userconf")
			.SetQueryParam("token", _token)
			.WithHeader("api-version", "6.5")
			.WithHeader("x-auth-token", _token)
			.GetJsonAsync<AkuvoxResponse<UserConfiguration>>();

		response.ValidateResponse();

		return response.Data;
	}
}
