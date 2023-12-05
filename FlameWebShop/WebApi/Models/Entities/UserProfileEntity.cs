using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebApi.Models.Entities
{
    public class UserProfileEntity
    {
        [Key, ForeignKey(nameof(User))]
        public string UserId { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public IdentityUser User { get; set; } = null!;
    }
    /* 
      [Key] markerar att 'UserId' är den primära nyckeln i databastabellen.
      [ForeignKey(nameof(User))] definierar ett främmande nyckelförhållande med IdentityUser-tabellen.
      'UserId' är en unik identifierare som länkar användarprofilen med en specifik användaridentitet.
       
      En referens till en 'IdentityUser' instans.
      'IdentityUser' representerar användarens autentiseringsrelaterade information i systemet.
      
      UserProfileEntity representerar en användarprofil och är kopplad till en IdentityUser-instans via UserId.
      Detta gör det möjligt att hålla användarens profilinformation(som förnamn och efternamn) separat från
      autentiseringsdata som lagras i IdentityUser.
      Användningen av attributen[Key] och[ForeignKey] är viktig för att definiera hur denna entitet mappas och
      relaterar till databastabeller i ett ORM-system som Entity Framework. 
    */


}
