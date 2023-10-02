using GalaSoft.MvvmLight.Command;
using ProyectoRefaccionaria2.Catalogos;
using ProyectoRefaccionaria2.Helpers;
using ProyectoRefaccionaria2.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ProyectoRefaccionaria2.ViewModels
{
    public class ProductosViewModel : INotifyPropertyChanged
    {
        #region eventos
        public event PropertyChangedEventHandler? PropertyChanged;
        #endregion

        ProductosCatalogo catalogoproductos = new ProductosCatalogo();
       
        public ObservableCollection<Productos> ListaProductos { get; set; } = new ObservableCollection<Productos>();
        public ObservableCollection<Marcas> ListaMarcas { get; set; } = new ObservableCollection<Marcas>();
        public Productos? Producto { get; set; } = new Productos();

        private ValidarProducto Validador = new ValidarProducto();
        public string Error { get; set; }
        public string Vista { get; set; }

        #region commands
        public ICommand VerProductosCommand { get; set; }
        public ICommand AgregarProductoCommand { get; set; }
        public ICommand VerAgregarProductosCommand { get; set; }
        public ICommand EditarProductoCommand { get; set; }
        public ICommand VerEliminarProductosCommand { get; set; }
        public ICommand EliminarProductosCommand { get; set; }
        public ICommand RegresarCommand { get; set; }
        #endregion
        public ProductosViewModel()
        {
            VerProductosCommand = new RelayCommand(VerProductos);
            VerAgregarProductosCommand = new RelayCommand(VerAgregarProductos);
            AgregarProductoCommand = new RelayCommand(AgregarProducto);
            EditarProductoCommand = new RelayCommand<Productos>(EditarProductos);
            VerEliminarProductosCommand = new RelayCommand<Productos>(VerEliminarProductos);
            EliminarProductosCommand = new RelayCommand(EliminarProductos);
            RegresarCommand = new RelayCommand(Regresar);

            //Se actualiza primero la base de datos para que se 
            //muestren los cambios y luego ya actualizas para que se vean los cambios ¿no?
            ActualizarBD();
            //Movi esto aqui yop(victor)
            Actualizar();
        }

        private void AgregarProducto()
        {
            var resultado = Validador.Validar(Producto);

            if (resultado == string.Empty)
            {
                if (Vista == "VerEditarProductos")
                {
                    catalogoproductos.Update(Producto);
                    Vista = "";
                }
                else if (Vista == "VerAgregarProductos")
                {
                    catalogoproductos.Create(Producto);
                    Vista = "";       
                }
                ActualizarBD();
            }
            else
            {
                Error = resultado;
                Actualizar();
            }
        }

        private void VerProductos()
        {
            Vista = "";
        }

        private void VerAgregarProductos()
        {
            Producto = new();
            Vista = "VerAgregarProductos";
            Actualizar();
        }
        private void EditarProductos(Productos producto)
        {
            Producto = producto;
            Vista = "VerEditarProductos";
            Actualizar();
        }

        private void VerEliminarProductos(Productos producto)
        {
            Producto = producto;
            Vista = "VerEliminarProductos";
            Actualizar();

        }

        private void EliminarProductos()
        {
            catalogoproductos.Delete(Producto);
            Vista = "";
            ActualizarBD();
        }

        private void Regresar()
        {
            Vista = "VerProductos";
            catalogoproductos.Reload(Producto);
            Actualizar();
        }

        //Actualizar la base de datos para que se vean los cambios en la lista
        //por eso primero se borra y se llena de nuevo ¿no?
        private void ActualizarBD()
        {
            ListaProductos.Clear();
            ListaMarcas.Clear();
            foreach (var item in catalogoproductos.GetAllProductos())
            {
                ListaProductos.Add(item);
            }
            foreach (var item in catalogoproductos.GetAllMarcas())
            {
                ListaMarcas.Add(item);
            }
            Actualizar();
        }
        //Metodo que uso para actualizar y cambiar las vistas
        private void Actualizar(string? propiedad = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propiedad));
        }
    }
}
