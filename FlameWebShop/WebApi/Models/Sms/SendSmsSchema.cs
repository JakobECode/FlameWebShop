﻿using System.ComponentModel.DataAnnotations;

namespace WebApi.Models.Sms
{
    public class SendSmsSchema
    {
        [Required]
        public string PhoneNumber { get; set; } = null!;
        [Required]
        public string Message { get; set; } = null!;
    }
}
