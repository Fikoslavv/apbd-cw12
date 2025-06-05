using apbd_cw12.DTOs;
using apbd_cw12.Models;
using apbd_cw12.Services;
using Microsoft.AspNetCore.Mvc;

namespace apbd_cw12.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TripController : ControllerBase
{
    protected ITripService service;

    public TripController(ITripService service) { this.service = service; }

    /* [HttpGet]
    public async Task<IActionResult> GetAllTripsAsync()
    {
        try
        {
            var trips = await this.service.GetAllAsync();

            if (trips.Any()) return this.Ok(trips);
            else return this.NoContent();
        }
        catch (Exception e) { return this.StatusCode(500, e.Message); }
    } */

    [HttpGet]
    public async Task<IActionResult> GetTripsOnPagesAsync([FromQuery] uint page = 1, [FromQuery] uint pageSize = 10)
    {
        try
        {
            var trips = await this.service.GetOnPagesAsync(page, pageSize);

            if (trips.Any()) return this.Ok(new { pageNum = page, pageSize, trips });
            else return this.NoContent();
        }
        catch (Exception e) { return this.StatusCode(500, e.Message); }
    }

    [HttpPost]
    public async Task<IActionResult> PostTripAsync([FromBody] TripDto trip)
    {
        try
        {
            return this.Ok(await this.service.InsertAsync(trip));
        }
        catch (Exception e) { return this.StatusCode(500, e.Message); }
    }

    [HttpPost("{idTrip}/clients")]
    public async Task<IActionResult> PostClientTripAsync([FromBody] ClientTripPostRequestDto dto)
    {
        try
        {
            await this.service.InsertClientTripAsync(dto);

            return this.NoContent();
        }
        catch (Exception e) { return this.StatusCode(500, e.Message); }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTripAsync([FromRoute] int id)
    {
        try
        {
            await this.service.DeleteAsync(id);

            return this.NoContent();
        }
        catch (ArgumentNullException) { return this.NotFound("No trip with given id was found."); }
        catch (Exception e) { return this.StatusCode(500, e.Message); }
    }
}
