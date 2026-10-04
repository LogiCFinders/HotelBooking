using feedbackApi.Data.DBContext;
using feedbackApi.Data.DTOs;
using feedbackApi.Models;
using feedbackApi.Services;
using Microsoft.Extensions.Configuration;
using System;
using System.Data.Entity;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Helpers;
using System.Web.Http;
using System.Web.Mvc;


namespace feedbackApi.Controllers
{


  
    public class AuthController : ApiController
    {
        private readonly FeedbackDbContext _db;
        public AuthController() : this(new FeedbackDbContext()) { }
        public AuthController(FeedbackDbContext db) => _db = db;

       

        // POST api/auth/register - create a company user (password is BCrypt-hashed)
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/auth/Register")]
        public async Task<IHttpActionResult> Register(RegisterDto dto)
        {
            if (!await _db.Companies.AnyAsync(c => c.CompanyId == dto.CompanyId && c.IsActive))
                return NotFound();

            if (await _db.Users.AnyAsync(u => u.Username == dto.Username))
                return ResponseMessage(Request.CreateResponse(
                    System.Net.HttpStatusCode.Conflict,
                    new { message = "This username is already taken." }));

            if (await _db.Users.AnyAsync(u => u.Email == dto.Email))
                return ResponseMessage(Request.CreateResponse(
                    System.Net.HttpStatusCode.Conflict,
                    new { message = "An account with this email already exists." }));
            // Generate email verification token
            var token = Guid.NewGuid().ToString("N");
            var user = new User
            {
                CompanyId = dto.CompanyId,
                Username = dto.Username,
                Email = dto.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password),
                FullName = dto.FullName,
                Role = "User",
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
                IsEmailVerified = false,
                EmailVerificationToken = token
            };

            _db.Users.Add(user);
            await _db.SaveChangesAsync();
            // Build verification link
            var verifyUrl = $"{Request.RequestUri.GetLeftPart(UriPartial.Authority)}/api/auth/verifyemail?token={token}";

            // Send email (simplified example)
            //IConfiguration _config = new Microsoft.Extensions.Configuration.ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            await EmailService.SendVerificationEmail(user.Email, verifyUrl);
            return Ok(new { user.UserId, message = "User created." });
        }

        // POST api/auth/login - accepts username OR email in the Username field
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/auth/Login")]
        // Web API route attribute
        public async Task<IHttpActionResult> Login(LoginDto dto)
        {
            var user = await _db.Users.Include(u => u.Company)
                .SingleOrDefaultAsync(u =>
                    (u.Username == dto.Username || u.Email == dto.Username) && u.IsActive);

            if (user == null || !BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash))
            {
                return ResponseMessage(Request.CreateResponse(
                    HttpStatusCode.Unauthorized,
                    new { message = "Invalid username or password." }));
            }

            user.LastLogin = DateTime.Now;
            await _db.SaveChangesAsync();

            return Ok(new
            {
                user.UserId,
                user.Username,
                user.FullName,
                user.Role,
                user.CompanyId,
                CompanyName = user.Company?.CompanyName
            });
        }

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/auth/verifyemail")]
        public async Task<IHttpActionResult> VerifyEmail(string token)
        {
            var user = await _db.Users.FirstOrDefaultAsync(u => u.EmailVerificationToken == token);

            if (user == null)
            {
                return ResponseMessage(Request.CreateResponse(
                    HttpStatusCode.BadRequest,
                    "Invalid token"));
            }

            user.IsEmailVerified = true;
            user.EmailVerificationToken = null; // clear token
            await _db.SaveChangesAsync();

            return Ok("Email verified successfully!");
        }
    }
}
