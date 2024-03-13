using CryptoExchange;
using CryptoExchange.Entities;
using CryptoExchange.Exceptions;
using CryptoExchange.Interfaces;
using CryptoExchange.Models;
using Isopoh.Cryptography.Argon2;

namespace CryptoExchange.Services
{
    public class UserService : IUserService
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
                throw new AlreadyExistException($"User already exist with name {existUser.Name}");
            }
            //регистрация для тестов, нужно доработать
            var newUser = new User
            {
                Name = request.Name,
                PasswordHash = Argon2.Hash(request.Password),
                WebsiteUrl = request.WebsiteUrl
            };
            var newUserEntity = _context.Users.Add(newUser).Entity;
            _context.SaveChanges();
            return newUser.Id;
        }
    }
}
