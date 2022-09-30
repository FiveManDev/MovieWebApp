﻿using System.ComponentModel.DataAnnotations;

namespace MovieAPI.Models.DTO
{
    public class CreateUserRequestDTO
    {
        [Required]
        [StringLength(maximumLength: 20, MinimumLength = 6)]
        public string UserName { get; set; }
        [Required]
        [StringLength(maximumLength: 20, MinimumLength = 6)]
        public string Password { get; set; }
        [Required]
        public string Email { get; set; }
    }
}
