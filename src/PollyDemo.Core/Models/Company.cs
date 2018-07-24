using System;

namespace PollyDemo.Core.Models
{
    public class Company
    {
        public Company(string name) => Name = name;
        public Guid CompanyId { get; set; } = Guid.NewGuid();          
		public string Name { get; set; }        
    }
}
