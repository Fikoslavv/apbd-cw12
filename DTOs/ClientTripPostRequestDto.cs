namespace apbd_cw12.DTOs;

public class ClientTripPostRequestDto
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Telephone { get; set; } = string.Empty;
    public string Pesel { get; set; } = string.Empty;
    public int IdTrip { get; set; }
    public string TripName { get; set; } = string.Empty;
    public DateTime? PaymentDate { get; set; }
}
