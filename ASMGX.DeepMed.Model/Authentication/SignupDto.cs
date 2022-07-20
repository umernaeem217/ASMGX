﻿using System.ComponentModel.DataAnnotations;

namespace ASMGX.DeepMed.Model.Authentication
{
    public class SignupDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        [Required]
        [MinLength(6)]
        public string Password { get; set; } = string.Empty;
        [Required]
        public string FullName { get; set; } = string.Empty;
    }
}
