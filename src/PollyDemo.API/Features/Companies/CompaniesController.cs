using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace PollyDemo.API.Features.Companies
{
    [ApiController]
    [Route("api/companies")]
    public class CompaniesController
    {
        private readonly IMediator _mediator;

        public CompaniesController(IMediator mediator) => _mediator = mediator;

        [HttpGet]
        public async Task<ActionResult<MaybeGetCompaniesQuery.Response>> Get()
            => await _mediator.Send(new MaybeGetCompaniesQuery.Request())            
    }
}
