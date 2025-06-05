namespace apbd_cw12.DTOs;

public partial class TripDto
{
    public int IdTrip { get; set; }

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public DateTime DateFrom { get; set; }

    public DateTime DateTo { get; set; }

    public int MaxPeople { get; set; }

    public ICollection<ClientDto>? Clients { get; set; }
    public ICollection<CountryDto>? Countries { get; set; }
}
