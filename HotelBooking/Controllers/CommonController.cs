using HotelBooking.Model;
using HotelBooking.Model.EditHotel;
using HotelBooking.Repository.Implementation;
using HotelBooking.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace HotelBooking.Controllers
{
    public class CommonController : ApiController
    {
        private readonly IBranch _Branch;
        public CommonController()
        {
            _Branch = new BranchRepository();
        }
        public CommonController(IBranch br) { 
            _Branch = br;
        }
        [HttpGet]
        [Route("api/Common/GetHotelTypes")]
        public IEnumerable<PropertyType> GetHotelTypes()
        {
            return _Branch.GetHotelTypes();
        }
        [HttpGet]
        [Route("api/Common/GetHotelCurrency")]
        public IEnumerable<Currency> GetHotelCurrency()
        {
            return _Branch.GetCurrency();
        }
    }
}
