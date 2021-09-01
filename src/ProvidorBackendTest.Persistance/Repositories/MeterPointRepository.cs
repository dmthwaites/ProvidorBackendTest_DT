using System.Collections.Generic;
using System.Linq;
using ProvidorBackendTest.Domain.Entities;

namespace ProvidorBackendTest.Persistance.Repositories
{
    // Would swap these out for implementations using entity framework and SQL server in a real scenario
    // Using the interfaces would mean this can be done just by changing this implementation in the persistance layer only
    public class MeterPointRepository : IMeterPointRepository
    {
        private IList<MeterPoint> _meterPoints;

        public MeterPointRepository()
        {
            _meterPoints = new List<MeterPoint>();
        }

        public void InitialiseData()
        {
            var gasMeter = new Meter("Gas", "2", "Credit");
            var electricMeter = new Meter("Electric", "1", "Credit");

            var meterPoints = new List<MeterPoint>();
            meterPoints.Add(new MeterPoint("123456", "1", new Meter[] { gasMeter, electricMeter }));
            meterPoints.Add(new MeterPoint("7891234", "1", new Meter[] { gasMeter, electricMeter }));
            meterPoints.Add(new MeterPoint("8887828", "1", new Meter[] { gasMeter, electricMeter }));
            meterPoints.Add(new MeterPoint("2165265", "1", new Meter[] { gasMeter, electricMeter }));

            InitialiseData(meterPoints);
        }

        public void InitialiseData(IEnumerable<MeterPoint> meterPoints)
        {
            foreach (var meterPoint in meterPoints)
            {
                _meterPoints.Add(meterPoint);
            }
        }

        public IQueryable<MeterPoint> GetAllMeterPoints()
        {
            return _meterPoints.AsQueryable();
        }

        public MeterPoint GetMeterPoint(string mpan)
        {
            return _meterPoints.FirstOrDefault(mp => mp.Mpan.ToLower() == mpan.ToLower());
        }
    }
}
