using System.Threading.Tasks;

namespace Auth0.ManagementAPI.WebApi.Services.Interfaces
{
    public interface IAuthService
    {
        string ClientID { get;  }
        string ClientSecret { get;  }
        string Domain { get;  }
        string CurrentAccessToken { get;  }

        Task<string> GetTokenAsync();
    }
}