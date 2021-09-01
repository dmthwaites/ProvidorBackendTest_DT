using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using ProvidorBackendTest.Persistance.Repositories;

namespace ProvidorBackendTest.Application.Queries
{
    public class GetAllMeterPoints
    {
        public class Query : IRequest<IEnumerable<Result>>
        {
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

        public class Handler : IRequestHandler<Query, IEnumerable<Result>>
        {
            private readonly ILogger<Handler> _logger;
            private readonly IMeterPointRepository _meterPointRepository;

            public Handler(ILogger<Handler> logger, IMeterPointRepository meterPointRepository)
            {
                _logger = logger;
                _meterPointRepository = meterPointRepository;
            }

            public async Task<IEnumerable<Result>> Handle(Query query, CancellationToken cancellationToken)
            {
                _logger.LogDebug("Entering {handlerName}", nameof(Handler));

                return _meterPointRepository.GetAllMeterPoints()
                                            .Select(mp => new Result(mp.Mpan, mp.MpanIndicator, mp.Meters.Select(m => new MeterResult(m.MeterType, m.NumberOfRegisters, m.OperatingMode))));
            }
        }
    }
}
