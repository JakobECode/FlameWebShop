﻿using System.ComponentModel.DataAnnotations;
using WebApi.Models.Dtos;

namespace WebApi.Models.Entities
{
    public class CreditCardEntity
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        [MinLength(4)]
        public string NameOnCard { get; set; } = null!;

        [Required]
        [StringLength(16)]
        public string CardNo { get; set; } = null!;

        [Required]
        public DateTime Expires { get; set; }

        [Required]
        [StringLength(3)]
        public string CVV { get; set; } = null!;

        public ICollection<UserProfileCreditCardEntity> UserProfileCreditCards { get; set; } = new HashSet<UserProfileCreditCardEntity>();

        public static implicit operator CreditCardDto(CreditCardEntity entity)
        {
            return new CreditCardDto
            {
                Id = entity.Id,
                NameOnCard = entity.NameOnCard,
                CardNo = entity.CardNo,
                ExpireYear = entity.Expires.Year,
                ExpireMonth = entity.Expires.Month,
                CVV = entity.CVV,
            };
        }
    }
}
