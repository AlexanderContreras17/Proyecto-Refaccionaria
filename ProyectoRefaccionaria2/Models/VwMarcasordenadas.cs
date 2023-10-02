using System;
using System.Collections.Generic;

namespace ProyectoRefaccionaria2.Models;

public partial class VwMarcasordenadas
{
    public int IdMarca { get; set; }

    public string Nombre { get; set; } = null!;

    public int? CantProductos { get; set; }
}
