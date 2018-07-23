using PollyDemo.Core.Models;

namespace PollyDemo.API.Features.Companies
{
    public class CompanyDto
    {        
        public int CompanyId { get; set; }
        public string Name { get; set; }

        public static CompanyDto FromCompany(Company company)
            => new CompanyDto
            {
                CompanyId = company.CompanyId,
                Name = company.Name
            };
    }
}
