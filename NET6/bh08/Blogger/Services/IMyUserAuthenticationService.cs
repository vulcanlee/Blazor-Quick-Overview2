﻿namespace Blogger.Services
{
    public interface IMyUserAuthenticationService
    {
        Task<bool> CheckUserAsync(string account, string password);
    }
}
