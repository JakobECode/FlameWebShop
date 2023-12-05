using WebApi.Models.Entities;

namespace WebApi.Models.Dtos
{
    public class Category
    {
		public int Id { get; set; }
		public string CategoryName { get; set; } = null!;


		public static implicit operator Category(CategoryEntity entity)
		{
			return new Category()
			{
				Id = entity.Id,
				CategoryName = entity.CategoryName,
			};
		}

		public static implicit operator CategoryEntity(Category category)
		{
			return new CategoryEntity()
			{
				Id = category.Id,
				CategoryName = category.CategoryName,
			};
		}
	}
}


/* public static implicit operator Category(CategoryEntity entity) 
   definierar en implicit omvandling från CategoryEntity till Category. 
   Detta betyder att du kan tilldela en CategoryEntity till en Category 
   variabel utan att behöva kalla en explicit omvandlingsmetod.
   Inuti metoden skapas en ny instans av Category och kopierar egenskaperna
   från CategoryEntity-instansen.

   CategoryEntity(Category category) är motsatsen till den föregående metoden; 
   den definierar en implicit omvandling från Category till CategoryEntity.
   Även här skapas en ny instans av CategoryEntity och kopierar egenskaperna från 
   Category-instansen.
*/


