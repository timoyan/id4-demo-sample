using System.Threading.Tasks;
using IdentityServer4.Models;
using IdentityServer4.Validation;

namespace id4_server {
    public class CustomUserValidator : IResourceOwnerPasswordValidator {
        public Task ValidateAsync (ResourceOwnerPasswordValidationContext context) {
            var existedUser = Config.GetUsers ().Find (q => q.Username == context.UserName && q.Password == context.Password);

            if (existedUser != null) {
                context.Result = new GrantValidationResult (existedUser.SubjectId, "password", existedUser.Claims);
                return Task.FromResult (context.Result);
            }

            context.Result = new GrantValidationResult (TokenRequestErrors.InvalidGrant, "The username and password do not match", null);
            return Task.FromResult (context.Result);
        }
    }
}