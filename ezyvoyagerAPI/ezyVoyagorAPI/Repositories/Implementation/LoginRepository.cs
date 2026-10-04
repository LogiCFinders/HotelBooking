using System;
using System.Data.Entity;
using System.Text;
using System.Threading.Tasks;
using ezyvoyagerAPI.Models;
using ezyvoyagerAPI.Models.DTOs;
using ezyvoyagerAPI.Models.Domain;
using ezyvoyagerAPI.Data;
using ezyvoyagerAPI.Repositories.Interfaces;
using ezyvoyagerAPI.Helper;

namespace ezyvoyagerAPI.Repositories.Implementation
{
    public class LoginRepository : ILoginRepository
    {
        private VoyagerDbContext _context;
        public LoginRepository()
        {
            _context = new VoyagerDbContext(); ;
        }
        public LoginRepository(VoyagerDbContext context)
        {
            _context = context;
        }

        public async Task<StaffLogin> CreateLoginAsync(RegisterDto dto, int staffId)
        {
            var login = new StaffLogin
            {
                LoginName = dto.LoginName,
                IsActive = true,
                StaffId = staffId,
                Password = PasswordHelper.HashPassword(dto.Password)
            };

            _context.StaffLogins.Add(login);
            await _context.SaveChangesAsync();
            return login;
        }

        public async Task<StaffLogin> GetLoginAsync(string loginName)
        {
            return await _context.StaffLogins.FirstOrDefaultAsync(l => l.LoginName == loginName && l.IsActive);
        }

        public bool VerifyPassword(string inputPassword, string storedHash)
        {
            return PasswordHelper.HashPassword(inputPassword) == storedHash;
        }

        public string GenerateTempPassword()
        {
            return Guid.NewGuid().ToString().Substring(0, 8);
        }

        public async Task UpdatePasswordAsync(int staffId, string newPassword)
        {
            var login = await _context.StaffLogins.FirstOrDefaultAsync(l => l.StaffId == staffId);
            if (login != null)
            {
                login.Password = HashPassword(newPassword);
                await _context.SaveChangesAsync();
            }
        }

        // 🔒 Simple Hash (replace with BCrypt/Identity in production)
        private string HashPassword(string password)
        {
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(password));
        }
    }
}
