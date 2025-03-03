using System;
using System.Collections.Generic;

namespace Challange.Data.Models;

public partial class Address
{
    public Guid Id { get; set; }

    public string City { get; set; } = null!;

    public string Street { get; set; } = null!;

    public string Zipcode { get; set; } = null!;

    public string State { get; set; } = null!;

    public DateTime? Creationdate { get; set; }

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
