using System;
using System.Collections.Generic;

namespace SuperNumberProject.Models;

public partial class UserSnumero
{
    public Guid Id { get; set; }

    public Guid? IdUser { get; set; }

    public Guid? IdSuperNumber { get; set; }

    public DateTime? RegDate { get; set; }

    public virtual SuperNumber? IdSuperNumberNavigation { get; set; }

    public virtual User? IdUserNavigation { get; set; }
}
