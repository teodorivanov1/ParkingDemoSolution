namespace Demo.WebApi.Core.Entities
{
    public struct ParkingSpotRates
    {
        public ParkingSpotRates(decimal hourly, decimal daily)
        {
            Hourly = hourly;
            Daily = daily;
        }

        public decimal Hourly { get; set; }

        public decimal Daily { get; set; }
    }
}
