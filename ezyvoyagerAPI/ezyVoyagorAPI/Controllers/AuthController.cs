using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Threading.Tasks;
using ezyvoyagerAPI.Models.DTOs;
using ezyvoyagerAPI.Services.Implementation;
using ezyvoyagerAPI.Services.Interfaces;
using ezyvoyagerAPI.Repositories.Implementation;
using ezyvoyagerAPI.Repositories.Interfaces;
using ezyvoyagerAPI.common;
using ezyvoyagerAPI.Data;


namespace ezyvoyagerAPI.Controllers
{
   
    public class AuthController : ApiController
    {
        private readonly IVoyagerServices _voyagerService;
        public AuthController()
        {
            _voyagerService = new VoyagerServices(
                new VoyagerRepository(),
                new StaffRepository(),
                new LoginRepository()
            );
        }
        public AuthController(IVoyagerServices voyagerService)
        {
            _voyagerService = voyagerService;
        }

        // ✅ Registration
        [System.Web.Http.HttpPost]
        [Route("api/auth/register")]
        public async Task<IHttpActionResult> Register(RegisterDto dto)
        {
            if (dto == null)
                return BadRequest("Invalid registration data.");

            ServiceResult result = await _voyagerService.RegisterStaffAsync(dto);

            if (!result.IsSuccess)
                return BadRequest(result.Message);

            // Return both data and message for clarity
            return Ok(new
            {
                result.Data,
                result.Message
            });

           

            
        }

        // ✅ Login
        //[AllowAnonymous]
        [System.Web.Http.HttpPost]
        [Route("api/auth/login")]
        public async Task<IHttpActionResult> Login(LoginDto dto)
        {
            ServiceResult result = await _voyagerService.LoginAsync(dto);

            if (!result.IsSuccess)
                return BadRequest(result.Message);

            return Ok(result.Data);
        }

        // ✅ Forget Password
        [System.Web.Http.HttpPost]
        [Route("api/auth/forgetPassword")]
        public async Task<IHttpActionResult> ForgetPassword(ForgetPasswordDto dto)
        {
            ServiceResult result = await _voyagerService.ForgetPasswordAsync(dto);

            if (!result.IsSuccess)
                return BadRequest(result.Message);

            return Ok(result.Message);
        }

        [System.Web.Http.HttpPost]
        [Route("api/auth/echo")]
        public IHttpActionResult Echo(PersonDto dto)
        {
            if (dto == null)
                return BadRequest("Invalid input");

            // Just return the same data back
            return Ok(new
            {
                dto.Name,
                dto.Age
            });
        }
        public class PersonDto
        {
            public string Name { get; set; }
            public int Age { get; set; }
        }

    }
}
