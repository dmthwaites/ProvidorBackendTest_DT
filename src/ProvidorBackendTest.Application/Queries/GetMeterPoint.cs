using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using ProvidorBackendTest.Persistance.Repositories;

namespace ProvidorBackendTest.Application.Queries
{
    public class GetMeterPoint
    {
        public class Query : IRequest<Result>
        {
            public string Mpan { get; }

            public Query(string mpan)
            {
                Mpan = mpan;
            }
        }

        public class Result
        {
            public string Mpan { get; }
            public string MpanIndicator { get; }
            public IEnumerable<MeterResult> Meters { get; }

            public Result(string mpan, string mpanIndicator, IEnumerable<MeterResult> meters)
            {
                Mpan = mpan;
                MpanIndicator = mpanIndicator;
                Meters = meters;
            }
        }

        public class MeterResult
        {
            public string MeterType { get; }
            public string NumberOfRegisters { get; }
            public string OperatingMode { get; }

            public MeterResult(string meterType, string numberOfRegisters, string operatingMode)
            {
                MeterType = meterType;
                NumberOfRegisters = numberOfRegisters;
                OperatingMode = operatingMode;
            }
        }

        public class Handler : IRequestHandler<Query, Result>
        {
            private readonly ILogger<Handler> _logger;
            private readonly IMeterPointRepository _meterPointRepository;

            public Handler(ILogger<Handler> logger, IMeterPointRepository meterPointRepository)
            {
                _logger = logger;
                _meterPointRepository = meterPointRepository;
            }

            public async Task<Result> Handle(Query query, CancellationToken cancellationToken)
            {
                _logger.LogDebug("Entering {handlerName}", nameof(Handler));

                var result = _meterPointRepository.GetMeterPoint(query.Mpan);

                if (result == null)
                {
                    _logger.LogWarning("Could not find a MeterPoint with Mpan {mpan}", query.Mpan);
                    return null;
                }

                return new Result(result.Mpan, result.MpanIndicator, result.Meters.Select(_ => new MeterResult(_.MeterType, _.NumberOfRegisters, _.OperatingMode)));
            }
        }
    }
}
