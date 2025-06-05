using apbd_cw12.DTOs;
using apbd_cw12.Models;
using apbd_cw12.Repositories;

namespace apbd_cw12.Services;

public class ClientTripService : IService<ClientTrip, ClientTripDto>
{
    protected readonly IRepository<ClientTrip> repository;

    public ClientTripService(IRepository<ClientTrip> repository) { this.repository = repository; }

    public async Task DeleteAsync(params object[] keys) => await this.repository.DeleteAsync(keys);

    public async Task<IEnumerable<ClientTripDto>> GetAllAsync() => (await this.repository.GetAllAsync()).Select(this.MapToDto);

    public async Task<ClientTripDto> InsertAsync(ClientTripDto clientTrip) => this.MapToDto(await this.repository.InsertAsync(this.MapFromDto(clientTrip)));

    public ClientTrip MapFromDto(ClientTripDto dto) => new()
    {
        IdClient = dto.IdClient,
        IdTrip = dto.IdTrip,
        RegisteredAt = dto.RegisteredAt,
        PaymentDate = dto.PaymentDate
    };

    public ClientTripDto MapToDto(ClientTrip clientTrip) => new()
    {
        IdClient = clientTrip.IdClient,
        IdTrip = clientTrip.IdTrip,
        RegisteredAt = clientTrip.RegisteredAt,
        PaymentDate = clientTrip.PaymentDate
    };
}
