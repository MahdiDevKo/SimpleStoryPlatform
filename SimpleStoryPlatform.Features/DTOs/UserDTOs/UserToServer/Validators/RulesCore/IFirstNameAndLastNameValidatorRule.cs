using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleStoryPlatform.Application.DTOs.UserDTOs.UserToServer.Validators.RulesCore
{
    public interface IFirstNameAndLastNameValidatorRule
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
