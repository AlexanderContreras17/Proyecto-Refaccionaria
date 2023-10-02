using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ProyectoRefaccionaria2.Catalogos;
using ProyectoRefaccionaria2.Models;
using Microsoft.EntityFrameworkCore;

namespace ProyectoRefaccionaria2.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        RefaccionariaContext context = new RefaccionariaContext();
        public object ViewModelActual { get; set; } = new ProductosViewModel();
        public string IsLogged { get; set; }
        public Usuarios? usuario { get; set; } = new();
        public string Error { get; set; }

        public ICommand NavegarMarcasCommand { get; set; }
        public ICommand NavegarProductosCommand { get; set; }
        public ICommand NavegarUsuariosCommand { get; set; }
        public ICommand CerrarSesionCommand { get; set; }
        public ICommand IniciarSesionCommand { get; set; }

        public MainViewModel()
        {
            NavegarMarcasCommand = new RelayCommand(NavegarMarcas);
            NavegarProductosCommand = new RelayCommand(NavegarProductos);
            IniciarSesionCommand = new RelayCommand(IniciarSesion);
            NavegarUsuariosCommand = new RelayCommand(NavegarUsuarios);
            CerrarSesionCommand = new RelayCommand(CerrarSesion);
        }

        private void IniciarSesion()
        {
            string query = $"select fnIniciarSesion('{usuario.Correo}','{usuario.Contraseña}');";
            var result = 
             context
            .Database.SqlQueryRaw<int>(query, usuario.Correo,usuario.Contraseña!).ToList().FirstOrDefault();
            if (result == 1)
            {
                IsLogged = "VerGeneral";
            }
            if (result == 2) 
            {
                Error = "Usuario Incorrecto";
            }
            else 
            {
                Error = "Contraseña Incorrecta";
            }
            Actualizar();
        }
        private void CerrarSesion() 
        {
            usuario = new();
            IsLogged = "VerLoggin";
            Error = "";
            Actualizar();
        }

        private void NavegarUsuarios()
        {
            ViewModelActual = new UsuariosViewModel();
            Actualizar(nameof(ViewModelActual));
        }

        private void NavegarProductos()
        {
            ViewModelActual = new ProductosViewModel();
            Actualizar(nameof(ViewModelActual));
        }

        private void NavegarMarcas()
        {
            ViewModelActual = new MarcasViewModel();
            Actualizar(nameof(ViewModelActual));
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        private void Actualizar(string? propiedad = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propiedad));
        }
    }
}
