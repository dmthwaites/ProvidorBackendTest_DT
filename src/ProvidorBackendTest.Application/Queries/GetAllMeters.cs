using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using ProvidorBackendTest.Persistance.Repositories;

namespace ProvidorBackendTest.Application.Queries
{
    public class GetAllMeters
    {
        public class Query : IRequest<IEnumerable<Result>>
        {
        }

        public class Result
        {
            public string MeterType { get; }
            public string NumberOfRegisters { get; }
            public string OperatingMode { get; }

            public Result(string meterType, string numberOfRegisters, string operatingMode)
            {
                MeterType = meterType;
                NumberOfRegisters = numberOfRegisters;
                OperatingMode = operatingMode;
            }
        }

        public class Handler : IRequestHandler<Query, IEnumerable<Result>>
        {
            private readonly ILogger<Handler> _logger;
            private readonly IMeterRepository _meterRepository;

            public Handler(ILogger<Handler> logger, IMeterRepository meterRepository)
            {
                _logger = logger;
                _meterRepository = meterRepository;
            }

            public async Task<IEnumerable<Result>> Handle(Query query, CancellationToken cancellationToken)
            {
                _logger.LogDebug("Entering {handlerName}", nameof(Handler));

                return _meterRepository.GetAllMeters()
                                       .Select(_ => new Result(_.MeterType, _.NumberOfRegisters, _.OperatingMode))
                                       .AsEnumerable();
            }
        }
    }
}
