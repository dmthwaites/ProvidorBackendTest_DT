using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProvidorBackendTest.Application.Queries;

namespace ProvidorBackendTest.Api.Controllers
{
    [Controller]
    [Route("api/[controller]")]
    public class MetersController
    {
        private readonly IMediator _mediator;

        public MetersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IEnumerable<GetAllMeters.Result>> GetAllMeters()
        {
            return await _mediator.Send(new GetAllMeters.Query());
        }

        [HttpGet("{meterType}")]
        public async Task<GetMeter.Result> GetMeter(string meterType)
        {
            return await _mediator.Send(new GetMeter.Query(meterType));
        }
    }
}
