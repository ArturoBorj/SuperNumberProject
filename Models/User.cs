using System;
using System.Collections.Generic;

namespace SuperNumberProject.Models;

public partial class User
{
    public Guid Id { get; set; }

    public string? Name { get; set; }

    public string? PassHash { get; set; }

    public DateTime? RegiDate { get; set; }

    public virtual ICollection<UserSnumero> UserSnumeros { get; set; } = new List<UserSnumero>();
}
