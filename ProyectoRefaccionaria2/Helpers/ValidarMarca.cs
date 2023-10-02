using ProyectoRefaccionaria2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoRefaccionaria2.Helpers
{
    internal class ValidarMarca
    {
        public string Validar(Marcas marcas)
        {
            if (string.IsNullOrEmpty(marcas?.Nombre ?? ""))
            {
                return "El campo Nombre no puede estar vacio";
            }
            
            else
            {
                return string.Empty;
            }
        }
    }
}
