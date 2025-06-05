using apbd_cw12.DTOs;
using apbd_cw12.Models;
using apbd_cw12.Services;
using Microsoft.AspNetCore.Mvc;

namespace apbd_cw12.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ClientController : ControllerBase
{
    protected IService<Client, ClientDto> service;

    public ClientController(IService<Client, ClientDto> service) { this.service = service; }

    [HttpGet]
    public async Task<IActionResult> GetAllClientsAsync()
    {
        try
        {
            var clients = await this.service.GetAllAsync();

            if (clients.Any()) return this.Ok(clients);
            else return this.NoContent();
        }
        catch (Exception e) when (e is ArgumentNullException || e is FormatException || e is OverflowException) { return this.BadRequest(); }
        catch (Exception e) { return this.StatusCode(500, e.Message); }
    }

    [HttpPost]
    public async Task<IActionResult> PostClientAsync([FromBody] ClientDto client)
    {
        try
        {
            return this.Ok(await this.service.InsertAsync(client));
        }
        catch (Exception e) { return this.StatusCode(500, e.Message); }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteClientAsync([FromRoute] int id)
    {
        try
        {
            await this.service.DeleteAsync(id);

            return this.NoContent();
        }
        catch (ArgumentNullException) { return this.NotFound("No client with given id was found."); }
        catch (ArgumentException e) { return this.BadRequest(e.Message); }
        catch (Exception e) { return this.StatusCode(500, e.Message); }
    }
}
