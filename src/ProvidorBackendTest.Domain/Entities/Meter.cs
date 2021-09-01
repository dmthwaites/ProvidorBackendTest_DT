namespace ProvidorBackendTest.Domain.Entities
{
    public class Meter
    {
        public string MeterType { get; }
        public string NumberOfRegisters { get; }
        public string OperatingMode { get; }

        public Meter(string meterType, string numberOfRegisters, string operatingMode)
        {
            MeterType = meterType;
            NumberOfRegisters = numberOfRegisters;
            OperatingMode = operatingMode;
        }
    }
}
