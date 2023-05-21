using System.IdentityModel.Tokens.Jwt;
using System.Security.Authentication;
using System.Security.Claims;
using System.Text;
using Domain.Entities;
using Infrastructure.Exceptions;
using Infrastructure.Persistance.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.Services;

public class JwtAuthService
{
    private readonly IConfiguration _configuration;
    private readonly AppDbContext _db;

    public JwtAuthService(IConfiguration configuration, AppDbContext context)
    {
        _db = context;
        _configuration = configuration;
    }

    public async Task<string> SignInAsync(string login, string password)
    {
        var realUser = await _db.Users.FirstOrDefaultAsync(x => x.Login == login);
        
        if (realUser is not null && BCrypt.Net.BCrypt.Verify(password, realUser.Password))
        {
            var issuer = _configuration.GetSection("Jwt:Issuer").ToString();
            var audience = _configuration.GetSection("Jwt:Audience").ToString();
            var key = Encoding.ASCII.GetBytes
                (_configuration.GetSection("Jwt:Key").ToString());
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("Id", Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Sub, login)
                }),
                Expires = DateTime.UtcNow.AddMinutes(5),
                Issuer = issuer,
                Audience = audience,
                SigningCredentials = new SigningCredentials
                (new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha512Signature)
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var jwtToken = tokenHandler.WriteToken(token);
            var stringToken = tokenHandler.WriteToken(token);
            return stringToken;
        }

        throw new AuthException("Неправильный логин или пароль!");
    }

    public async Task SignUpAsync(User user)
    {
        if (_db.Users.Any(x => x.Login == user.Login))
        {
            throw new AuthException("Это имя пользователья уже занято!");
        }
        
        user.CreatedAt = DateTime.Now;
        user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
        
        await _db.Users.AddAsync(user);
        await _db.SaveChangesAsync();
    }
}