﻿namespace SMS.Models.Users
{
    public class UserRegistrationFormModel
    {
        public string Username { get; init; }

        public string Email { get; init; }

        public string Password { get; init; }

        public string ConfirmPassword { get; init; }
    }
}