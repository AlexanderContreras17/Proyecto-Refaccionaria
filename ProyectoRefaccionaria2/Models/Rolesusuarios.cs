using System;
using System.Collections.Generic;

namespace ProyectoRefaccionaria2.Models;

public partial class Rolesusuarios
{
    public int IdRol { get; set; }

    public string? Nombre { get; set; }

    public virtual ICollection<Usuarios> Usuarios { get; set; } = new List<Usuarios>();
}
