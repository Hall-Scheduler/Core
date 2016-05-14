namespace HallScheduler.Desktop.Services.Contracts
{
    using Server.DataTransferObjects.Account;
    using System.Security;
    using System.Threading.Tasks;

    public interface IIdentityService
    {
        UserIdentity UserIdentity { get; }

        SecureString AuthToken { get; set; }

        Task<bool> LoadIdentity(IHttpService httpService);

        void ClearIdentity();
    }
}
