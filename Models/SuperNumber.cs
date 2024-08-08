using System;
using System.Collections.Generic;

namespace SuperNumberProject.Models;

public partial class SuperNumber
{
    public Guid Id { get; set; }

    public int? Number { get; set; }

    public int? SuperNumber1 { get; set; }

    public virtual ICollection<UserSnumero> UserSnumeros { get; set; } = new List<UserSnumero>();
}
