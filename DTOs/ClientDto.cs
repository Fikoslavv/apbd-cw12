﻿namespace apbd_cw12.DTOs;

public partial class ClientDto
{
    public int IdClient { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Telephone { get; set; } = null!;

    public string Pesel { get; set; } = null!;
}
