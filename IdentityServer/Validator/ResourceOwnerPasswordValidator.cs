using DbAccessLayer.Entities;
using DbAccessLayer.Repository;
using IdentityServer4.Models;
using IdentityServer4.Validation;
using System.Security.Claims;
using System.Threading.Tasks;

namespace IdentityServer.Validator
{
    /// <summary>
    /// ResourceOwnerPasswordValidator
    /// </summary>
    public class ResourceOwnerPasswordValidator : IResourceOwnerPasswordValidator
    {
        // repository to get user from db
        private readonly IUserRepository _userRepository;

        public ResourceOwnerPasswordValidator(IUserRepository userRepository)
        {
            _userRepository = userRepository; //DI
        }

        // this is used to validate your user account with provided grant at /connect/token
        /// <summary>
        /// Validating user credentials, username and pasword 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            try
            {
                //get your user model from db 
                var user =  await _userRepository.GetUserByUsername(context.UserName);
                if (user != null)
                {
                    //check if password match 
                    if (user.Password == context.Password)
                    {
                        //set the result
                        context.Result = new GrantValidationResult(
                            subject: user.Id.ToString(),
                            authenticationMethod: "custom",
                            claims: GetUserClaims(user));

                        return;
                    }

                    context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant, "Incorrect password");
                    return;
                }
                context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant, "User does not exist.");
                return;
            }
            catch
            {
                context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant, "Invalid username or password");
            }
        }

        /// <summary>
        /// Gets the claims, by this claims access toke in generated
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public static Claim[] GetUserClaims(UserEntity user)
        {
            return new Claim[]
            {
                new Claim("user_id", user.Id.ToString()??""),
                new Claim("username", user.Username ?? ""),
                new Claim("email", user.Email?? ""),
            };
        }
    }
}