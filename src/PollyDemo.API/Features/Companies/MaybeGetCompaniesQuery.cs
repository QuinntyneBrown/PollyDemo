using MediatR;
using Microsoft.EntityFrameworkCore;
using PollyDemo.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PollyDemo.API.Features.Companies
{
    public class MaybeGetCompaniesQuery
    {
        public class Request : IRequest<Response> { }

        public class Response
        {
            public IEnumerable<CompanyDto> Companies { get; set; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IAppDbContext _context;
            private static readonly Random random = new Random();
            private static readonly object syncLock = new object();
            public Handler(IAppDbContext context) => _context = context;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {

                var number = RandomNumber();

                if (number > 14) throw new Exception();

                return new Response()
                {
                    Companies = await _context.Companies.Select(x => CompanyDto.FromCompany(x)).ToListAsync()
                };
            }
            public static int RandomNumber(int min = 0, int max = 30)
            {
                lock (syncLock)
                    return random.Next(min, max);
            }
        }
    }
}
