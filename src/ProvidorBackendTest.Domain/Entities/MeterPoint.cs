namespace ProvidorBackendTest.Domain.Entities
{
    public class MeterPoint
    {
        public string Mpan { get; }
        public string MpanIndicator { get; }
        public Meter[] Meters { get; }

        public MeterPoint(string mpan, string mpanIndicator, Meter[] meters)
        {
            Mpan = mpan;
            MpanIndicator = mpanIndicator;
            Meters = meters;
        }
    }
}
