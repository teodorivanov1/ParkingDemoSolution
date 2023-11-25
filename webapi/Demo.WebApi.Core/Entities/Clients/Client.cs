using Demo.WebApi.Core.Abstraction;
using Demo.WebApi.Core.Entities.Addresses;

namespace Demo.WebApi.Core.Entities.Clients
{
    public class Client : Entity
    {
        public Client(string name)
        {
            Name = name;
            Addresses = new List<Address>();
        }

        public Client()
        {

        }

        public string Name { get; private set; }

        public IEnumerable<Address> Addresses { get; private set; }
    }
}
