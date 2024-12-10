using System;
using System.Collections.Generic;

namespace CapaAccesoBD.Models;

public partial class Usuario
{
    public int Iduser { get; set; }

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Email { get; set; } = null!;

    public virtual ICollection<Categorylabel> Categorylabels { get; set; } = new List<Categorylabel>();

    public virtual ICollection<Task> Tasks { get; set; } = new List<Task>();
}
