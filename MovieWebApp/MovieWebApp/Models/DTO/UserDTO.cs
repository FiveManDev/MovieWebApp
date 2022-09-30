﻿namespace MovieAPI.Models.DTO
{
    public class UserDTO
    {
        public Guid UserID { get; set; }
        public string UserName { get; set; }
        //Relationship
        public ProfileDTO Profile { get; set; }
        public AuthorizationDTO Authorization { get; set; }

    }
}
