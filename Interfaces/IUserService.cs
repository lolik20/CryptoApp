using CryptoExchange.Models;

namespace CryptoExchange.Interfaces
{
    public interface IUserService
    {
        Guid AddUser(AddUserRequest request);
    }
}
