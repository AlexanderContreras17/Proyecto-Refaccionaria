using ProyectoRefaccionaria2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ProyectoRefaccionaria2.Helpers
{
    internal class ValidarProducto
    {
        public string Validar(Productos producto)
        {
            if (string.IsNullOrEmpty(producto.Nombre))
            {
                return "Nombre incorrecto.";
            }
            if (string.IsNullOrEmpty(producto.Descripcion))
            {
                return "La descripcíon no puede estar vacia";
            }
            //if(string.IsNullOrEmpty (producto.Precio? == 0 || producto.Precio? < 0))
            //{
            //    return "El precio no puede estar vacio";
                
                //if (!Regex.IsMatch(producto.Nombre, @"^(0|[1-9]\\d{0,4}|1[0-7]\\d{4}|200000)$"))
                //{

                //}
           // }
            //if(producto.Precio> 99999999 || producto.Precio<0)
            //{
            //    return "El precio es demasiado alto";
            //}
            return string.Empty;

        }
    }
}
