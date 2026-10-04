using ezyvoyagerAPI.Models.Domain;
using ezyVoyagorAPI.Models.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace ezyvoyagerAPI.Data
{
    public class VoyagerDbContext : DbContext
    {
        public VoyagerDbContext() : base("Name=EZVConstring")
        {


        }

        public DbSet<LoginInfo> LoginInfo { get; set; }
        public DbSet<TravelDate> TravelDate { get; set; }
        //public DbSet<Price> Price { get; set; }
        public DbSet<Image> Image { get; set; }
        public DbSet<InclusionsExclusion> InclusionsExclusion { get; set; }
        public DbSet<VoyagerInfo> Voyager { get; set; }
        public DbSet<Itinerary> Itinerary { get; set; }
        public DbSet<VoyagerFAQ> VoyagerFAQ { get; set; }

        //#StaticData
        public DbSet<GroupType> GroupType { get; set; }
        public DbSet<DurationUnit> DurationUnit { get; set; }
        public DbSet<Frequency> Frequency { get; set; }
        public DbSet<PriceType> PriceType { get; set; }
        public DbSet<ActivityType> ActivityType { get; set; }
        public DbSet<Grade> Grade { get; set; }
        public DbSet<Theme> Theme { get; set; }
        public DbSet<Currency> Currency { get; set; }
        public DbSet<CountryCode> CountryCode { get; set; }


        public DbSet<StaffMember> StaffMembers { get; set; }
        public DbSet<StaffLogin> StaffLogins
        {
            get; set;


        }
       
        public DbSet<RoleMaster> Roles { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Branch> Branches { get; set; }
        //public DbSet<Branch> Branches { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Templates> Templates { get; set; }

        ///Logging
        ///
        public DbSet<EZV_LOG> EZV_LOG { get; set; }
    }
}