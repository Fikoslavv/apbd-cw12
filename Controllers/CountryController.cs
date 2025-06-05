using apbd_cw12.DTOs;
using apbd_cw12.Models;
using apbd_cw12.Services;
using Microsoft.AspNetCore.Mvc;

namespace apbd_cw12.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class CountryController : ControllerBase
{
    protected IService<Country, CountryDto> service;

    public CountryController(IService<Country, CountryDto> service) { this.service = service; }

    [HttpGet]
    public async Task<IActionResult> GetAllCountriesAsync()
    {
        try
        {
            var countries = await this.service.GetAllAsync();

            if (countries.Any()) return this.Ok(countries);
            else return this.NoContent();
        }
        catch (Exception e) { return this.StatusCode(500, e.Message); }
    }

    [HttpPost]
    public async Task<IActionResult> PostCountryAsync([FromBody] CountryDto country)
    {
        try
        {
            return this.Ok(await this.service.InsertAsync(country));
        }
        catch (Exception e) { return this.StatusCode(500, e.Message); }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCountryAsync(int id)
    {
        try
        {
            await this.service.DeleteAsync(id);

            return this.NoContent();
        }
        catch (ArgumentNullException) { return this.NotFound("Country with given id was not found."); }
        catch (Exception e) { return this.StatusCode(500, e.Message); }
    }
}
