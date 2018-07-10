using PollyDemo.Core.Models;

namespace PollyDemo.API.Features.Companies
{
    public class CompanyApiModel
    {        
        public int CompanyId { get; set; }
        public string Name { get; set; }

        public static CompanyApiModel FromCompany(Company company)
            => new CompanyApiModel
            {
                CompanyId = company.CompanyId,
                Name = company.Name
            };
    }
}
