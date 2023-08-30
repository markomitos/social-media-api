using SocialMediaAPI.DTOs.Users.POST.Requests;
using SocialMediaAPI.Models;

namespace SocialMediaAPI.Validators
{
    public static class Validator
    {
        public static bool IsValid(User user)
        {
            return user.Email is not null;
        }

        public static bool IsValid(UserLoginRequest user)
        {
            return user.Email is not null;
        }

        public static bool IsValid(CreateUserRequest user)
        {
            return user.Email is not null;
        }
    }
}
