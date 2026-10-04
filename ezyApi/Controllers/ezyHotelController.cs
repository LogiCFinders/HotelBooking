using ezyApi.Models;
using ezyApi.Repository.Implementation;
using ezyApi.Repository.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ezyApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ezyHotelController : ControllerBase
    {
        private readonly IHotelAvailability _hrepo;

        public ezyHotelController(IHotelAvailability _urepo)
        {
            _hrepo = _urepo;
        }
        [HttpPost(Name = "getHotelAvailaibility")]
        public AvailResponse getHotelAvailaibility(AvailRequest _req)
        {
            return _hrepo.getHotelAvailaibility(_req);
           
        }
    }
}
