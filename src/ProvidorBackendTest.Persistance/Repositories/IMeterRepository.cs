using System.Linq;
using ProvidorBackendTest.Domain.Entities;

namespace ProvidorBackendTest.Persistance.Repositories
{
    public interface IMeterRepository
    {
        IQueryable<Meter> GetAllMeters();
        Meter GetMeter(string meterType);
    }
}