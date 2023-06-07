namespace Demo.WebApi.Application.Features.Parking.Commands
{
    public class EntryCommandResult
    {
        // for external use ... or just a unique identifier different from than license plate which is a string
        public int Ticket { get; set; }
    }
}
