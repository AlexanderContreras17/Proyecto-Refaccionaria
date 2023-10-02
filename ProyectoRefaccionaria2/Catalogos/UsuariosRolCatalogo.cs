using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProyectoRefaccionaria2.Models;

namespace ProyectoRefaccionaria2.Catalogos
{
    public class UsuariosRolCatalogo 
    {
        RefaccionariaContext context = new RefaccionariaContext();

        public IEnumerable<Rolesusuarios> GetAllRolesUsuarios()
        {
            return context.Rolesusuarios.OrderBy(x => x.Nombre);
        }

    }
}
