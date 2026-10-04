using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ezyvoyagerAPI.Repositories.Interfaces;
using ezyvoyagerAPI.Data;
using ezyvoyagerAPI.Models.Domain;
using ezyvoyagerAPI.Models.DTOs;
using System.Threading.Tasks;

namespace ezyvoyagerAPI.Repositories.Implementation
{
        public class CompanyRepository : ICompanyRepository
    {
        private readonly VoyagerDbContext _context;

        public CompanyRepository()
        {
            _context = new VoyagerDbContext();
        }
        public CompanyRepository(VoyagerDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CompanyDTO>> GetAllAsync()
        {
            return _context.Companies
                           .Where(x => !x.IsDeleted)
                           .Select(x => new CompanyDTO
                           {
                               Id = x.Id,
                               Name = x.Name,
                               Address = x.Address,
                               City = x.City,
                               Pincode = x.Pincode,
                               Country = x.Country,
                               Phone1 = x.Phone1,
                               Phone2 = x.Phone2,
                               FaxNumber = x.FaxNumber,
                               WebSite = x.WebSite,
                               Email = x.Email,
                               ServiceTaxNumber = x.ServiceTaxNumber,
                               ATOLNumber = x.ATOLNumber,
                               PANNumber = x.PANNumber,
                               TANNumber = x.TANNumber,
                               ABTANumber = x.ABTANumber,
                               CompanyLogo = x.CompanyLogo,
                               XHostLogo = x.XHostLogo,
                               IsAddressAndLogoUsedForDocs = x.IsAddressAndLogoUsedForDocs,
                               IsBranchAddressAndLogoUsedForDocs = x.IsBranchAddressAndLogoUsedForDocs,
                               IsSAddressAndLogoUsedForB2bDocs = x.IsSAddressAndLogoUsedForB2bDocs,
                               CRS_Id = x.CRS_Id,
                               CTnC_Id = x.CTnC_Id,
                               IsActive = x.IsActive
                           })
                           .ToList();
        }

        public async Task<CompanyDTO> GetByIdAsync(long id)
        {
            return _context.Companies
                           .Where(c => c.Id == id && !c.IsDeleted)
                           .Select(c => new CompanyDTO
                           {
                               Id = c.Id,
                               Name = c.Name,
                               Address = c.Address,
                               City = c.City,
                               Pincode = c.Pincode,
                               Country = c.Country,
                               Phone1 = c.Phone1,
                               Phone2 = c.Phone2,
                               FaxNumber = c.FaxNumber,
                               WebSite = c.WebSite,
                               Email = c.Email,
                               ServiceTaxNumber = c.ServiceTaxNumber,
                               ATOLNumber = c.ATOLNumber,
                               PANNumber = c.PANNumber,
                               TANNumber = c.TANNumber,
                               ABTANumber = c.ABTANumber,
                               CompanyLogo = c.CompanyLogo,
                               XHostLogo = c.XHostLogo,
                               IsAddressAndLogoUsedForDocs = c.IsAddressAndLogoUsedForDocs,
                               IsBranchAddressAndLogoUsedForDocs = c.IsBranchAddressAndLogoUsedForDocs,
                               IsSAddressAndLogoUsedForB2bDocs = c.IsSAddressAndLogoUsedForB2bDocs,
                               CRS_Id = c.CRS_Id,
                               CTnC_Id = c.CTnC_Id,
                               IsActive = c.IsActive
                           })
                           .FirstOrDefault();
        }

        public async Task<CompanyDTO> AddAsync(CompanyDTO companyDto)
        {
            var company = MapToEntity(companyDto);

            _context.Companies.Add(company);
            _context.SaveChanges();

            companyDto.Id = company.Id;

            return companyDto;
        }

        public async Task<CompanyDTO> UpdateAsync(CompanyDTO companyDto)
        {
            var company = _context.Companies.FirstOrDefault(c => c.Id == companyDto.Id);

            if (company == null)
                return null;

            MapToEntity(company, companyDto);

            _context.SaveChanges();

            return companyDto;
        }
        public async Task<bool> DeleteAsync(Int64 id)
        {
            var company = _context.Companies.Find(id);

            if (company == null)
                return false;

            company.IsDeleted = true;
            company.IsActive = false;

            _context.SaveChanges();

            return true;
        }

        private Company MapToEntity(CompanyDTO dto)
        {
            return new Company
            {
                Id = dto.Id,
                Name = dto.Name,
                Address = dto.Address,
                City = dto.City,
                Pincode = dto.Pincode,
                Country = dto.Country,
                Phone1 = dto.Phone1,
                Phone2 = dto.Phone2,
                FaxNumber = dto.FaxNumber,
                WebSite = dto.WebSite,
                Email = dto.Email,
                ServiceTaxNumber = dto.ServiceTaxNumber,
                ATOLNumber = dto.ATOLNumber,
                PANNumber = dto.PANNumber,
                TANNumber = dto.TANNumber,
                ABTANumber = dto.ABTANumber,
                CompanyLogo = dto.CompanyLogo,
                XHostLogo = dto.XHostLogo,
                IsAddressAndLogoUsedForDocs = dto.IsAddressAndLogoUsedForDocs,
                IsBranchAddressAndLogoUsedForDocs = dto.IsBranchAddressAndLogoUsedForDocs,
                IsSAddressAndLogoUsedForB2bDocs = dto.IsSAddressAndLogoUsedForB2bDocs,
                CRS_Id = dto.CRS_Id,
                CTnC_Id = dto.CTnC_Id,
                IsActive = dto.IsActive,
                IsDeleted = dto.IsDeleted
            };
            }

        private void MapToEntity(Company entity, CompanyDTO dto)
        {
            entity.Name = dto.Name;
            entity.Address = dto.Address;
            entity.City = dto.City;
            entity.Pincode = dto.Pincode;
            entity.Country = dto.Country;
            entity.Phone1 = dto.Phone1;
            entity.Phone2 = dto.Phone2;
            entity.FaxNumber = dto.FaxNumber;
            entity.WebSite = dto.WebSite;
            entity.Email = dto.Email;
            entity.ServiceTaxNumber = dto.ServiceTaxNumber;
            entity.ATOLNumber = dto.ATOLNumber;
            entity.PANNumber = dto.PANNumber;
            entity.TANNumber = dto.TANNumber;
            entity.ABTANumber = dto.ABTANumber;
            entity.CompanyLogo = dto.CompanyLogo;
            entity.XHostLogo = dto.XHostLogo;
            entity.IsAddressAndLogoUsedForDocs = dto.IsAddressAndLogoUsedForDocs;
            entity.IsBranchAddressAndLogoUsedForDocs = dto.IsBranchAddressAndLogoUsedForDocs;
            entity.IsSAddressAndLogoUsedForB2bDocs = dto.IsSAddressAndLogoUsedForB2bDocs;
            entity.CRS_Id = dto.CRS_Id;
            entity.CTnC_Id = dto.CTnC_Id;
            entity.IsActive = dto.IsActive;
        }

        public async Task<TemplateDTO> getCompanyTemplate(string xApi)
        {
            var cmp = _context.Companies.Where(c => c.xAPIKey == xApi).FirstOrDefault();
            var tmpl = _context.Templates.Where(t => t.TemplateID == cmp.TemplateId).FirstOrDefault();
            return new TemplateDTO
            {
                TemplateID = tmpl.TemplateID,
                CompanyID = cmp.Id,
                TemplateName = tmpl.TemplateName,
                TemplateType = tmpl.TemplateType

            };

        }

        


    }
}