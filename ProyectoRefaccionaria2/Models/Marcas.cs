using System;
using System.Collections.Generic;

namespace ProyectoRefaccionaria2.Models;

public partial class Marcas
{
    public int IdMarca { get; set; }

    public string Nombre { get; set; } = null!;

    public int? CantProductos { get; set; }

    public virtual ICollection<Productos> Productos { get; set; } = new List<Productos>();
}
