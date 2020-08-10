using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using TastyBoutique.Business.Models.Filter;
using TastyBoutique.Persistance.Models;
using Xunit;

namespace TastyBoutique.IntegrationTesting
{
    public class FiltersControllerIntegrationTests : IntegrationTests
    {
        [Fact]
        public async Task Post_Filter()
        {
            //Arrange
            var filterModel = new CreateFilterModel { Name = "vegan" };

            //Act
            var response = await HttpClient.PostAsJsonAsync($"api/v1/filter", filterModel);

            //Assert
            response.IsSuccessStatusCode.Should().BeTrue();

            var filterId = Guid.Parse(response.Headers.Location.OriginalString);
            Filters filter = null;
            await ExecuteDatabaseAction(async (tastyBoutiqueContext) =>
            {
                filter = await tastyBoutiqueContext.Filters
                    .FirstOrDefaultAsync(i => i.Id == filterId);
            });

            filter.Should().NotBeNull();
        }

        [Fact]
        public async Task Get_Filters()
        {
            //Arrange
            var filter1 = new Filters
            {
                Name = "Vegan"
            };
            var filter2 = new Filters
            {
                Name = "Low-Carb"
            };

            await ExecuteDatabaseAction(async (tastyBoutiqueContext) =>
            {
                await tastyBoutiqueContext.AddAsync(filter1);
                await tastyBoutiqueContext.AddAsync(filter2);
                await tastyBoutiqueContext.SaveChangesAsync();
            });

            //Act
            var response = await HttpClient.GetAsync("api/v1/filter");

            //Assert
            response.IsSuccessStatusCode.Should().BeTrue();

            var filter = await response.Content.ReadAsAsync<IList<FilterModel>>();
            filter.Should().NotBeNull();
            filter.Should().NotBeEmpty();
            filter.Should().HaveCount(2);
            filter.Select(i => i.Name).Should().Contain(filter1.Name);
            filter.Select(i => i.Name).Should().Contain(filter1.Name);
        }

    }
}
