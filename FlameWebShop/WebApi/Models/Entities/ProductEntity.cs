using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Models.Entities
{
    public class ProductEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; } = null!;

        [Required]
        public string Description { get; set; } = null!;

        [Required]
        [Column ( TypeName = "decimal (18, 2)")]
        public decimal Price { get; set; }

        [Range(0, 5)]
        public int StarRating { get; set; } = 0;

        public string? ImageUrl { get; set; }

        public int CategoryId { get; set; }

        //public CategoryEntity Category { get; set; } = null!;
        public string? Tag {  get; set; }    
    }
    //Klassen ProductEntity används för att representera en produkt i en databas.
    //Attributen som [Key], [Required], [Column] och [Range] används för att definiera
    //hur egenskaperna mappas till databastabellen och vilka begränsningar som gäller för dem

    //[Column(TypeName = "decimal(16, 2)")] specificerar att priset ska lagras som ett
    //decimaltal med 16 siffror i totalt och 2 decimaler i databasen.

    //public string? Tag { get; set; }:
    //Tag är en annan nollbar egenskap som kan användas för att lagra ytterligare
    //information om produkten, till exempel en tagg eller etikett
}
