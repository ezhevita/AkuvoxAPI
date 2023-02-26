using System.Threading.Tasks;
using AkuvoxAPI.Models;

namespace AkuvoxAPI;

public interface IAkuvoxGateClient
{
	Task<ServerList> Authenticate(string login, string password);
	Task AuthenticateUsingToken(string token);
	Task<RestServerList> GetRestServerList();
	AkuvoxClient GetAkuvoxClient();
	Task<ServerList> GetServerList(string login, string password);
}
