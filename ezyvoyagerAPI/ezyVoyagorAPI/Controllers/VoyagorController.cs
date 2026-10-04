using ezyvoyagerAPI.Models.DTOs;
using ezyvoyagerAPI.Services.Implementation;
using ezyvoyagerAPI.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace ezyvoyagerAPI.Controllers
{
    

   

    public class VoyagorController : ApiController
    {
        private readonly IVoyagerServices _voyagerService;

        public VoyagorController()
        {
            _voyagerService = new VoyagerServices();

        }
        public VoyagorController(IVoyagerServices voyagerServices) {
            _voyagerService = voyagerServices;

        }
        [Route("api/getVoyagerItenaryList")]
        [HttpGet]
        public async Task<VoyagerRESP> getVoyagerItenaryList()
        {
            
            VoyagerRESP product = await _voyagerService.GetVoyagerItenary();

            return product;
        }
        /// <summary>
        /// Add voyagerItenary
        /// </summary>
        /// <param name="voyagerItenaryAddREQ"></param>
        /// <returns></returns>
        [Route("api/AddVoyagerItenary")]
        [System.Web.Http.HttpPost]
        public async Task<VoyagerItenaryAddREQ> AddVoyagerItenary(VoyagerItenaryAddREQ voyagerItenaryAddREQ)
        {
            return await _voyagerService.AddVoyager(voyagerItenaryAddREQ);

        }

        /// <returns></returns>
        [Route("api/getVoyagerItenary/{voyagerItenaryId}")]
        [System.Web.Http.HttpPost]
        public async Task<VoyagerItenaryAddREQ> getVoyagerItenary(Int64 voyagerItenaryId)
        {
            return await _voyagerService.getVoyagerItenary(voyagerItenaryId);

        }

        [Route("api/Login")]
        [HttpPost]
        public async Task<LoginRESP> Login(LoginREQ loginREQ)
        {
            // Asynchronously retrieve product data
            //loginREQ.LoginReq.LoginName = "demo@admin.com";
            //loginREQ.LoginReq.LoginPassword = "admin@123";
            //var encrypted = await EncryptionHelper.EncryptAsync(loginREQ.LoginReq.LoginPassword);
            //loginREQ.LoginReq.LoginPassword = encrypted;
            VoyagerREQ voyagerREQ = new VoyagerREQ { TravelDate = DateTime.Now, Destination = "Test" };
            var loginData = await _voyagerService.Login(loginREQ);

            return loginData;
        }

        [Route("api/GetStaticData")]
        [System.Web.Http.HttpGet]
        public async Task<VoyagerStaticDataRESP> GetStaticData(VoyagerStaticDataREQ REQ)
        {
            return await _voyagerService.GetStaticData(REQ);

        }

        [Route("api/PostStaticData")]
        [System.Web.Http.HttpPost]
        public async Task<VoyagerStaticDataRESP> PostStaticData(VoyagerStaticDataREQ REQ)
        {
            return await _voyagerService.GetStaticData(REQ);

        }

        //company
        [Route("api/GetCompanyDetails/{companyid}")]
        [System.Web.Http.HttpGet]
        public async Task<CompanyDTO> GetCompanyDetails(Int64 companyid)
        {
            return await _voyagerService.getCompanyDetails(companyid);

        }
        [Route("api/UpdateCompanyDetails/{companyid}")]
        [System.Web.Http.HttpGet]
        public async Task<CompanyDTO> UpdateCompanyDetails(Int64 companyid)
        {
            return await _voyagerService.getCompanyDetails(companyid);

        }

        //company
        [Route("api/GetCompanyTemplate/{xpaikey}")]
        [System.Web.Http.HttpGet]
        public async Task<TemplateDTO> GetCompanyTemplate(string xpaikey)
        {
            return await _voyagerService.getCompanyTemplate(xpaikey);

        }

    }
}
