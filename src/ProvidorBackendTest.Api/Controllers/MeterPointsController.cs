using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProvidorBackendTest.Application.Queries;

namespace ProvidorBackendTest.Api.Controllers
{
    [Controller]
    [Route("api/[controller]")]
    public class MeterPointsController
    {
        private readonly IMediator _mediator;

        public MeterPointsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IEnumerable<GetAllMeterPoints.Result>> GetAllMeterPoints()
        {
            return await _mediator.Send(new GetAllMeterPoints.Query());
        }

        [HttpGet("{mpan}")]
        public async Task<GetMeterPoint.Result> GetMeterPoint(string mpan)
        {
            return await _mediator.Send(new GetMeterPoint.Query(mpan));
        }
    }
}
