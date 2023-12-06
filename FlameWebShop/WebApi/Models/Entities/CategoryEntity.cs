using System.ComponentModel.DataAnnotations;

namespace WebApi.Models.Entities
{
    public class CategoryEntity
    {
        [Key]
        public int Id { get; set; }
        public string CategoryName { get; set; } = null!;

        //public IEnumerable<ProductEntity> Products { get; set; } = new List<ProductEntity>();
    }
}
// Attributet [Key] indikerar att denna egenskap är den primära nyckeln i en databastabell.
// Detta är viktigt för ORM-ramverk för att kunna identifiera varje rad unikt i en tabell.

//public IEnumerable<ProductEntity> Products { get; set; } = new List<ProductEntity>();
//Denna egenskap representerar en samling av ProductEntity-objekt.
//IEnumerable<ProductEntity> är en samlingstyp som används för att lagra flera instanser av ProductEntity.
//new List<ProductEntity>() initierar denna egenskap med en tom lista.
//Detta är ofta använt i ORM-ramverk för att representera relationer mellan tabeller,
//i detta fall en "en-till-många"-relation mellan kategorier och produkter.