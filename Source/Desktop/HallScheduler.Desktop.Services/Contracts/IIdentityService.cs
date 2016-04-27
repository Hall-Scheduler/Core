namespace HallScheduler.Desktop.Services.Contracts
{
    using Server.DataTransferObjects.Account;
    using System.Security;

    public interface IIdentityService
    {
        UserIdentity UserIdentity { get; }

        SecureString AuthToken { get; }

        void LoadIdentity(IHttpService httpService);
    }
}
