using System;
using System.Collections.Generic;

namespace ProyectoRefaccionaria2.Models;

public partial class Productos
{
    public int Codigo { get; set; }

    public string? Descripcion { get; set; }

    public string? Nombre { get; set; }

    public decimal? Precio { get; set; }

    public int? IdMarcaP { get; set; }

    public virtual Marcas? IdMarcaPNavigation { get; set; }
}
