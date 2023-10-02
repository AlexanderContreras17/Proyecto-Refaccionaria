using Microsoft.EntityFrameworkCore;
using ProyectoRefaccionaria2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoRefaccionaria2.Catalogos
{
    public class MarcasCatalogo
    {
        RefaccionariaContext context = new RefaccionariaContext();
        
        public IEnumerable<Marcas> GetAllMarcas() 
        {
            return context.Marcas.OrderBy(x => x.Nombre);
        }
        internal IEnumerable<Productos> GetAllProducts()
        {
            return context.Productos.OrderBy(x => x.IdMarcaP);
        }

        public void Create(Marcas m)
        {
            context.Database.ExecuteSqlRaw($"call refaccionaria.spAgregarMarca('{m.Nombre}');");
            context.SaveChanges();
            //context.Entry(m).Reload();
        }
        public void Update(Marcas m)
        {
            context.Update(m);
            context.SaveChanges();
            context.Entry(m).Reload();
        }
        public void Delete(Marcas m)
        {
           context.Remove(m);
           context.SaveChanges();
        }
        internal void Reload(Marcas? Marca)
        {
            context.Entry(Marca).Reload();
        }
    }
}
