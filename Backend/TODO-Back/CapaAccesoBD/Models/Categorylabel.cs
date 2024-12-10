using System;
using System.Collections.Generic;

namespace CapaAccesoBD.Models;

public partial class Categorylabel
{
    public int Idlabel { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public int Iduser { get; set; }

    public virtual ICollection<Asignation> Asignations { get; set; } = new List<Asignation>();

    public virtual Usuario IduserNavigation { get; set; } = null!;
}
