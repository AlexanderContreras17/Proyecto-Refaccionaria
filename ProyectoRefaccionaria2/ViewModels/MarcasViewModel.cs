using System;
using ProyectoRefaccionaria2.Catalogos;
using ProyectoRefaccionaria2.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using ProyectoRefaccionaria2.Views.MarcasViews;
using System.Windows.Controls;
using ProyectoRefaccionaria2.Helpers;
using System.Text.RegularExpressions;

//se agrega un using para tener acceso a las views de marcas

namespace ProyectoRefaccionaria2.ViewModels
{
    public class MarcasViewModel : INotifyPropertyChanged
    {
        #region eventos
        public event PropertyChangedEventHandler? PropertyChanged;
        #endregion
        MarcasCatalogo catalogomarcas = new MarcasCatalogo();
        public ObservableCollection<Productos> ListaProductos { get; set; } = new ObservableCollection<Productos>();
        public ObservableCollection<Marcas> ListaMarcas { get; set; } = new ObservableCollection<Marcas>();
        
        private ValidarMarca ValidadorM = new ValidarMarca();

        // se hace una propiedad para mandar a llamar las vistas
        public string Vista { get; set; }
        public string Error { get; set; }

        //se manda a llamar la clase de models para poder darle uso en los metodos de marcas 
        public Marcas? Marca { get; set; } = new Marcas();

        #region commands
        public ICommand VerMarcasCommand { get; set; }
        public ICommand VerAgregarMarcasCommand { get; set; }
        public ICommand AgregarMarcasCommand { get; set; }
        public ICommand VerEditarMarcasCommand { get; set; }
        public ICommand VerEliminarMarcasCommand { get; set; }
        public ICommand EliminarMarcasCommand { get; set; }
        public ICommand RegresarCommand { get; set; }

        #endregion

        public MarcasViewModel()
        {
            VerMarcasCommand = new RelayCommand(VerMarcas);
            VerAgregarMarcasCommand = new RelayCommand(VerAgregarMarcas);
            AgregarMarcasCommand = new RelayCommand(AgregarMarcas);
            VerEditarMarcasCommand = new RelayCommand<Marcas>(VerEditarMarcas);
            VerEliminarMarcasCommand = new RelayCommand<Marcas>(VerEliminarMarcas);
            EliminarMarcasCommand = new RelayCommand(EliminarMarcas);
            RegresarCommand = new RelayCommand(Regresar);

            //Se actualiza primero la base de datos para que se 
            //muestren los cambios y luego ya actualizas para que se vean los cambios ¿no?
            //si, asi es como se actualiza
            ActualizarBD();
            Actualizar();
        }

        private void VerMarcas()
        {
            Vista = "";
        }

        private void VerAgregarMarcas()
        {
            Marca = new();
            Vista = "VerAgregarMarcas";          
            Actualizar();
        }

        private void AgregarMarcas()
        {
            var resultado = ValidadorM.Validar(Marca);

            if(resultado == string.Empty)
            {
                if (Vista == "VerEditarMarcas" && Marca!=null)
                {
                    catalogomarcas.Update(Marca);
                    Vista = "";
                }
                else if (Vista =="VerAgregarMarcas"&& Marca!=null)
                {
                    catalogomarcas.Create(Marca);
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

        private void VerEditarMarcas(Marcas marca)
        {
            Marca = marca;
            Vista = "VerEditarMarcas";
            Actualizar();
        }
        private void VerEliminarMarcas(Marcas marca)
        {
            Marca = marca;
            Vista = "VerEliminarMarcas";
            Actualizar();
        }
       // se manda a llamar el model en el metodo, si es diferente a nulo, 
       //abre la vista de eliminar, al eliminar se actualiza la base de datos
        private void EliminarMarcas()

        {
            //Validar
           catalogomarcas.Delete(Marca);
            Vista = "";
            ActualizarBD();
            Actualizar();
        }

        //metodo para regresar/cancelar la accion en la vista
        //y regrese a la vista anterior
        private void Regresar()
        {
            Vista = "";
            catalogomarcas.Reload(Marca);
            Actualizar();
        }

        //Actualizar la base de datos para que se vean los cambios en la lista
        //por eso primero se borra y se llena de nuevo ¿no?
        private void ActualizarBD()
        {
            ListaMarcas.Clear();
            foreach (var item in catalogomarcas.GetAllMarcas())
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
