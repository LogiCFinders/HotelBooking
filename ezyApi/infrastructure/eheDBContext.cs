using ezyApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Data;
//using System.Data.Entity;

namespace ezyApi.infrastructure
{
    public class eheDBContext : DbContext
    {

        public eheDBContext(DbContextOptions<eheDBContext> options) : base(options)
        {
        }
        public DbSet<AvailResponse> AvailabilityResponse
        {
            get;
            set;
        }

    }
   
}
