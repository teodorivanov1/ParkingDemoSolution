using Demo.WebApi.Core.Abstraction;
using Demo.WebApi.Core.Entities.Clients;

namespace Demo.WebApi.Core.Entities.Addresses
{
    public class Address : Entity
    {
        public Address(string addressLine, bool isUsa, string zipCode)
        {
            Client = new();
            AddressLine = addressLine;
            IsUSA = isUsa;
            ZipCode = zipCode;
        }

        public Address()
        {

        }

        public string AddressLine { get; private set; }
        public bool IsUSA { get; private set; }
        public string ZipCode { get; private set; }


        public Client Client { get; set; }
        public int ClientId { get; set; }
    }
}
