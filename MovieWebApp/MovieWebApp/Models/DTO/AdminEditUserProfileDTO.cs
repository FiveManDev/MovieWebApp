using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieAPI.Models.DTO
{
    public class AdminEditUserProfileDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Subscription { get; set; }
        public string Right { get; set; }
    }
}