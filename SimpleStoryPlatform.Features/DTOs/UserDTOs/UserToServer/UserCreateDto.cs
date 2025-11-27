using SimpleStoryPlatform.Application.DTOs.UserDTOs.UserToServer.Validators.RulesCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleStoryPlatform.Application.DTOs.UserDTOs.UserToServer
{
    public class UserCreateDto : IUserPassValidatorRule, IEmailValidatorRule, IFirstNameAndLastNameValidatorRule
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public string PasswordTwo { get; set; }
    }
}
