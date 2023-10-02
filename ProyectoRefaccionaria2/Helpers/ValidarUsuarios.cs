using ProyectoRefaccionaria2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoRefaccionaria2.Helpers
{
    internal class ValidarUsuarios
    {
        public string Validar(Usuarios usuario)
        {
            if (string.IsNullOrEmpty(usuario.Nombre))
            {
                return "El nombre no puede estar vacio";
            }
            if (string.IsNullOrEmpty(usuario.Correo))
            {
                return "El Correo no puede estar vacio";
            }
            if (usuario.Contraseña?.Length<8)
            {
                return "La longitud de la contraseña es deasiado corta";
            }
            if (usuario.Contraseña?.Length > 13) 
            {
                return "La longitud de la contraseña es demasiado larga";
            }
            else 
            {
                return string.Empty;
            }
        }
    }
}
