using System;
using System.Collections.Generic;
namespace Challange.Data.Models;

public partial class User
{
    public Guid Id { get; set; }

    public string Email { get; set; } = null!;

    public string Username { get; set; } = null!;

    public string Fullname { get; set; } = null!;

    public string? Profilepicurl { get; set; }

    public string? Gender { get; set; }

    public string? Phonenumber { get; set; }

    public string? Employment { get; set; }

    public string? Keyskill { get; set; }

    public Guid? Addressid { get; set; }

    public DateTime? Creationdate { get; set; }

    public virtual Address? Address { get; set; }
}
