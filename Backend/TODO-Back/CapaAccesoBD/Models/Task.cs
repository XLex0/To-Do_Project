using System;
using System.Collections.Generic;

namespace CapaAccesoBD.Models;

public partial class Task
{
    public int Idtask { get; set; }

    public string Description { get; set; } = null!;

    public string? Priority { get; set; }

    public DateOnly Creationdate { get; set; }

    public DateOnly? Enddate { get; set; }

    public int Iduser { get; set; }

    public virtual ICollection<Asignation> Asignations { get; set; } = new List<Asignation>();

    public virtual Usuario IduserNavigation { get; set; } = null!;
}
