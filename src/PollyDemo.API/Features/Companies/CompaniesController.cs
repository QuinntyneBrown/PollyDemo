using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace PollyDemo.API.Features.Companies
{
    [ApiController]
    [Route("api/companies")]
    public class CompaniesController
    {
        private readonly IMediator _mediator;
        private static readonly Random random = new Random();
        private static readonly object syncLock = new object();

        public CompaniesController(IMediator mediator) => _mediator = mediator;

        [HttpGet]
        public async Task<ActionResult<GetCompaniesQuery.Response>> Get() {

            var number = RandomNumber();

            if (number > 14) throw new Exception();

            return await _mediator.Send(new GetCompaniesQuery.Request());
        }


        public static int RandomNumber(int min = 0, int max = 30)
        {
            lock (syncLock)
                return random.Next(min, max);
        }
    }
}
