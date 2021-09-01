using System.Linq;
using ProvidorBackendTest.Domain.Entities;

namespace ProvidorBackendTest.Persistance.Repositories
{
    public interface IMeterPointRepository
    {
        IQueryable<MeterPoint> GetAllMeterPoints();
        MeterPoint GetMeterPoint(string mpan);
    }
}
