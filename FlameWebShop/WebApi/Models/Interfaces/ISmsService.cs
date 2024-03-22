﻿namespace WebApi.Models.Interfaces
{
    public interface ISmsService
    {
        Task<bool> SendSmsAsync(string phoneNo, string smsBody);
    }
}
