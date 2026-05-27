using Api.DTOs.Countries;
using Domain.Entities;
using MediatR;

namespace Api.Controllers.Countries;

public sealed class CountriesController : CrudController<Country, CreateCountryRequest, UpdateCountryRequest, CountryResponse>
{
    public CountriesController(ISender sender) : base(sender) { }
}
