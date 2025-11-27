using SimpleStoryPlatform.Application.DTOs.UserDTOs.UserToServer.Validators.RulesCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleStoryPlatform.Application.DTOs.UserDTOs.UserToServer
{
    public class UserLoginDto : IUserPassValidatorRule
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
