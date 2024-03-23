using WebApi.Models.Dtos;
using WebApi.Models.Schemas;

namespace WebApi.Models.Interfaces
{
    public interface IPaymentService
    {

        Task<IEnumerable<CreditCardDto>> GetUserCreditCardsAsync(string userName);
        Task<bool> RegisterCreditCardsAsync(RegisterCreditCardSchema schema, string userName);
        Task<bool> DeleteCreditCardsAsync(int id, string userName);
    }
}
