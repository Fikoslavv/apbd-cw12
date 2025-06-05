using apbd_cw12.DTOs;
using apbd_cw12.Models;

namespace apbd_cw12.Services;

public interface ITripService : IService<Trip, TripDto>
{
    Task<IEnumerable<TripDto>> GetOnPagesAsync(uint page, uint pageSize);

    Task InsertClientTripAsync(ClientTripPostRequestDto dto);
}
