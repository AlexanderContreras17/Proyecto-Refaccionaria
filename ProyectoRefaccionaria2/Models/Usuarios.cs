using System;
using System.Collections.Generic;

namespace ProyectoRefaccionaria2.Models;

public partial class Usuarios
{
    public int IdUsuarios { get; set; }

    public string? Nombre { get; set; }

    public string? Correo { get; set; }

    public string? Contraseña { get; set; }

    public int? IdTipoRol { get; set; }

    public virtual Rolesusuarios? IdTipoRolNavigation { get; set; }
}
