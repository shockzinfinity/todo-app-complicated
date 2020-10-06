using System;
using System.Collections.Generic;
using System.Linq;
using todoCore3.Api.Models;

namespace todoCore3.Api.Services
{
  public interface IUserService
  {
    User Authenticate(string username, string password);

    IEnumerable<User> GetAll();

    User GetBy(int id);

    User Create(User user, string password);

    void Update(User user, string password = null);

    void Delete(int id);
  }

  public class UserService : IUserService
  {
    private TodoContext _context;

    public UserService(TodoContext context)
    {
      _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public User Authenticate(string username, string password)
    {
      if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
        return null;

      var user = _context.Users.SingleOrDefault(u => u.Username == username);

      if (user == null) return null;

      if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt)) return null;

      return user;
    }

    public User Create(User user, string password)
    {
      if (string.IsNullOrWhiteSpace(password)) throw new AppException("Password is required.");

      if (_context.Users.Any(u => u.Username == user.Username))
        throw new AppException($"Username: '${user.Username}' is taken.");

      byte[] passwordHash, passwordSalt;
      CreatePasswordHash(password, out passwordHash, out passwordSalt);

      user.PasswordHash = passwordHash;
      user.PasswordSalt = passwordSalt;

      _context.Users.Add(user);
      _context.SaveChanges();

      return user;
    }

    public void Delete(int id)
    {
      var user = _context.Users.Find(id);
      if (user != null)
      {
        _context.Users.Remove(user);
        _context.SaveChanges();
      }
    }

    public IEnumerable<User> GetAll()
    {
      return _context.Users;
    }

    public User GetBy(int id)
    {
      return _context.Users.Find(id);
    }

    public void Update(User user, string password = null)
    {
      var updateUser = _context.Users.Find(user.Id);

      if (updateUser == null) throw new AppException("User not found.");

      if (!string.IsNullOrWhiteSpace(user.Username) && user.Username != user.Username)
      {
        if (_context.Users.Any(u => u.Username == user.Username))
          throw new AppException($"Username: '${user.Username}'is taken.");

        updateUser.Username = user.Username;
      }

      if (!string.IsNullOrWhiteSpace(user.FirstName))
        updateUser.FirstName = user.FirstName;

      if (!string.IsNullOrWhiteSpace(user.LastName))
        updateUser.LastName = user.LastName;

      if (!string.IsNullOrWhiteSpace(password))
      {
        byte[] passwordHash, passwordSalt;
        CreatePasswordHash(password, out passwordHash, out passwordSalt);

        user.PasswordHash = passwordHash;
        user.PasswordSalt = passwordSalt;
      }

      _context.Update(updateUser);
      _context.SaveChanges();
    }

    private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
    {
      if (password == null) throw new ArgumentNullException(nameof(password));
      if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value could not be empty or whitespace.", nameof(password));

      using (var hmac = new System.Security.Cryptography.HMACSHA512())
      {
        passwordSalt = hmac.Key;
        passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
      }
    }

    private static bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
    {
      if (password == null) throw new ArgumentNullException(nameof(password));
      if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value could not be empty or whitespace.", nameof(password));
      if (storedHash.Length != 64) throw new ArgumentException("Invalid stored hash.", nameof(storedHash));
      if (storedSalt.Length != 128) throw new ArgumentException("Invalid stored salt.", nameof(storedSalt));

      using (var hmac = new System.Security.Cryptography.HMACSHA512(storedSalt))
      {
        var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));

        for (int i = 0; i < computedHash.Length; i++)
        {
          if (computedHash[i] != storedHash[i]) return false;
        }
      }

      return true;
    }
  }
}
