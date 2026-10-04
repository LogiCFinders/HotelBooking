using ezyApi.infrastructure;
using ezyApi.Models;
using ezyApi.Repository.Interface;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.ConstrainedExecution;

namespace ezyApi.Repository.Implementation
{
    public class HotelAvailability : IHotelAvailability
    {
        private  eheDBContext veheDBContext;
       
        public HotelAvailability(eheDBContext ehe) {
            veheDBContext=ehe;
        }
        public  AvailResponse getHotelAvailaibility(AvailRequest availRequest)
        {
            AvailResponse rtnResp = new AvailResponse();
            var myParam = new SqlParameter("@my_lat", (SqlDbType.Float)).Value = availRequest.Longitude;
            var myParam1 = new SqlParameter("@my_lng", (SqlDbType.Float)).Value = availRequest.Latitude;
            var myParam2 = new SqlParameter("@dist", (SqlDbType.Int)).Value = 50;
            List<Hotelinfoary> rtnData= veheDBContext.Database.SqlQuery<Hotelinfoary>($"exec Sp_GetHGotelAvailData {myParam},{myParam1},{myParam2}").ToList();
            rtnResp.HotelInfoAry = rtnData.ToArray();
            return rtnResp;
        }
    }
}
