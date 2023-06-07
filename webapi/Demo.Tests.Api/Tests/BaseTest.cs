using Demo.Tests.Api.Fixtures;
using Demo.WebApi.Application.Abstractions;
using Demo.WebApi.Core.Entities;
using Demo.WebApi.ServiceHost.Controllers;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using SimpleStoreWeb.Data.EntityConfigurations;
using System.Text;

namespace Demo.Tests.Api.Tests
{
    public class BaseTest : IClassFixture<BaseFixture>
    {
        protected readonly BaseFixture baseFixture;

        public BaseTest()
        {
            baseFixture = new BaseFixture();
            RenewDb();
        }

        // Тhis is far from a good implementation and should be refactored
        protected Response<T> PostRequest<T>(string json)
        {
            var body = new StringContent(json, Encoding.UTF8, "application/json");
            var uri = $"/{nameof(ParkingController)[..^10]}/Entry/";
            HttpResponseMessage response = baseFixture.Client.PostAsync(uri, body).Result;
            Response<T>? result = null;
            try
            {
                string str = response.Content.ReadAsStringAsync().Result;
#nullable disable
                result = JsonConvert.DeserializeObject<Response<T>>(str);
#nullable enable
            }
            catch { }

            Assert.NotNull(result);
            return result;
        }

        private void RenewDb()
        {
            var prefix = EntityTypeConfiguration<ParkingSpot>.TablePrefix;
            baseFixture.AppDbContext.Database.ExecuteSqlRaw($"DELETE FROM {prefix}ParkingSpots");
        }
    }
}
