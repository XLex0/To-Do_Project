using System;
using System.Collections.Generic;

namespace CapaAccesoBD.Models;

public partial class Asignation
{
    public int Idasignation { get; set; }

    public int Idlabel { get; set; }

    public int Idtask { get; set; }

    public virtual Categorylabel IdlabelNavigation { get; set; } = null!;

    public virtual Task IdtaskNavigation { get; set; } = null!;
}
