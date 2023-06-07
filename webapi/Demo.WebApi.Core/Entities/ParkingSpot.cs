using Demo.WebApi.Common.Enums;
using Demo.WebApi.Core.Abstraction;
using Demo.WebApi.Core.Extensions;
using Demo.WebApi.Core.Settings.Parking;

namespace Demo.WebApi.Core.Entities
{
    public class ParkingSpot : Entity
    {
        public ParkingSpot(
            string plate,
            VehicleType vehicleType,
            DiscountType discountType,
            ICoreValidator<ParkingSpot> validator)
        {
            Plate = plate;
            VehicleType = vehicleType;
            EntryAt = DateTime.Now;
            DiscountType = discountType;

            validator.Validate(this);
        }

        // Used from AppDbContext. In real world application and similar design will be better to have DbEntities model.
        // This model will be mapped to DbEtntities model.
        public ParkingSpot()
        {
            Plate = string.Empty;
        }

        public string Plate { get; set; }

        public VehicleType VehicleType { get; set; }

        public DateTime EntryAt { get; set; }

        public DiscountType DiscountType { get; set; }

        public int Places => PlacesSettings.PlacesPerVehicle[VehicleType];

        public ParkingSpotRates Rate => ParkingSpotRateSettings.Rate[VehicleType];

        public decimal TotalMoney { get; set; }

        public int TotalHours => EntryAt.ToTotalHours();
    }
}
