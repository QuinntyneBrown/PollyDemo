using MediatR;
using Microsoft.EntityFrameworkCore;
using PollyDemo.Core.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PollyDemo.API.Features.Companies
{
    public class GetCompaniesQuery
    {
        public class Request : IRequest<Response> { }

        public class Response
        {
            public IEnumerable<CompanyDto> Companies { get; set; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            public IAppDbContext _context { get; set; }
            
			public Handler(IAppDbContext context) => _context = context;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
                => new Response()
                {
                    Companies = await _context.Companies.Select(x => CompanyDto.FromCompany(x)).ToListAsync()
                };
        }
    }
}
