namespace HallScheduler.Desktop.Services
{
    using Contracts;
    using Data.Common.Enumerations;
    using Infrastructure.ExtensionMethods;
    using Models;
    using Server.DataTransferObjects.Account;
    using System.Security;

    public class IdentityService : IIdentityService
    {
        public UserIdentity UserIdentity { get; private set; }

        public SecureString AuthToken { get; set; }

        public bool IsInRole(IdentityRoleType role)
        {
            return this.UserIdentity.Roles.Contains(role);
        }

        public async void LoadIdentity(IHttpService httpService)
        {
            var url = "http://localhost:38013/api/Identity";
            var authToken = this.AuthToken.ConvertToUnsecureString();
            var response = await httpService.Get<ResponseResult<UserIdentity>>(url, authToken);
            var userIdentity = (response as ResponseResult<UserIdentity>).Data;

            this.UserIdentity = userIdentity;
        }
    }
}
