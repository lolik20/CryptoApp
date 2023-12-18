using CryptoExchange;
using CryptoExchange.Entities;
using CryptoExchange.Exceptions;
using CryptoExchange.Interfaces;
using CryptoExchange.Models;
using Isopoh.Cryptography.Argon2;

namespace CryptoExchange.Services
{
    public class UserService :IUserService
    {
       private readonly ApplicationContext _context;
        public UserService(ApplicationContext context)
        {
            _context = context;
        }
        public Guid AddUser(AddUserRequest request)
        {
            var existUser = _context.Users.FirstOrDefault(x => x.Name == request.Name);
            if (existUser != null)
            {
                throw new AlreadyExistException($"User already exist with name {existUser.Name}",System.Net.HttpStatusCode.Conflict);
            }
            var newUser = new User { Name = request.Name, PasswordHash = Argon2.Hash(request.Password) };
            var newUserEntity = _context.Users.Add(newUser).Entity;
            var newUserProfile = new UserProfile() { UserId = newUserEntity.Id };
            _context.Profiles.Add(newUserProfile);
            _context.SaveChanges();
            return newUser.Id;
        }
    }
}
