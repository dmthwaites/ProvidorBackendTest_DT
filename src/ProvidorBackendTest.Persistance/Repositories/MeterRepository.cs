using System.Collections.Generic;
using System.Linq;
using ProvidorBackendTest.Domain.Entities;

namespace ProvidorBackendTest.Persistance.Repositories
{
    // Would swap these out for implementations using entity framework and SQL server in a real scenario
    // Using the interfaces would mean this can be done just by changing this implementation in the persistance layer only
    public class MeterRepository : IMeterRepository
    {
        private IList<Meter> _meters;

        public MeterRepository()
        {
            _meters = new List<Meter>();
        }

        public void InitialiseData()
        {
            var meters = new List<Meter>();
            meters.Add(new Meter("Gas", "2", "Credit"));
            meters.Add(new Meter("Electric", "1", "Credit"));

            InitialiseData(meters);
        }

        public void InitialiseData(IEnumerable<Meter> meters)
        {
            foreach (var meter in meters)
            {
                _meters.Add(meter);
            }
        }

        public IQueryable<Meter> GetAllMeters()
        {
            return _meters.AsQueryable();
        }

        public Meter GetMeter(string meterType)
        {
            return _meters.FirstOrDefault(m => m.MeterType.ToLower() == meterType.ToLower());
        }
    }
}
