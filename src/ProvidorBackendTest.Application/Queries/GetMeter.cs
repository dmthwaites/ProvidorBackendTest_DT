using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using ProvidorBackendTest.Persistance.Repositories;

namespace ProvidorBackendTest.Application.Queries
{
    public class GetMeter
    {
        public class Query : IRequest<Result>
        {
            public string MeterType { get; set; }

            public Query(string meterType)
            {
                MeterType = meterType;
            }
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

        public class Handler : IRequestHandler<Query, Result>
        {
            private readonly ILogger<Handler> _logger;
            private readonly IMeterRepository _meterRepository;

            public Handler(ILogger<Handler> logger, IMeterRepository meterRepository)
            {
                _logger = logger;
                _meterRepository = meterRepository;
            }

            public async Task<Result> Handle(Query query, CancellationToken cancellationToken)
            {
                _logger.LogDebug("Entering {handlerName}", nameof(Handler));

                var meter = _meterRepository.GetMeter(query.MeterType);

                if (meter == null)
                {
                    _logger.LogWarning("Could not find a Meter with Name {meterName}", query.MeterType);
                    return null;
                }

                return new Result(meter.MeterType, meter.NumberOfRegisters, meter.OperatingMode);
            }
        }
    }
}
