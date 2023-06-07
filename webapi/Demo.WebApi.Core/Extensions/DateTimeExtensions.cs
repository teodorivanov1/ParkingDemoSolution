namespace Demo.WebApi.Core.Extensions
{
    public static class DateTimeExtensions
    {
        public static int ToTotalHours(this DateTime from)
            => (int)Math.Ceiling((DateTime.Now - from).TotalHours);
    }
}
