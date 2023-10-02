using ProyectoRefaccionaria2.Catalogos;
using ProyectoRefaccionaria2.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProyectoRefaccionaria2.Views.UsuariosViews;
using GalaSoft.MvvmLight.Command;
using System.ComponentModel;
using System.Windows.Input;
using System.Printing;
using ProyectoRefaccionaria2.Helpers;
using System.ComponentModel.DataAnnotations;

namespace ProyectoRefaccionaria2.ViewModels
{
    public class UsuariosViewModel: INotifyPropertyChanged
    {

        UsuariosCatalogo catalogousuarios = new UsuariosCatalogo();
        UsuariosRolCatalogo catalogoroles = new UsuariosRolCatalogo();
        

        public event PropertyChangedEventHandler? PropertyChanged;

        public ObservableCollection<Usuarios> ListaUsuarios { get; set; }= new ObservableCollection<Usuarios>();
        public ObservableCollection<Rolesusuarios> ListaRolesUsuarios { get; set; } = new ObservableCollection<Rolesusuarios>();
        public Usuarios? Usuario { get; set; } = new Usuarios();
        
        ValidarUsuarios Validador = new ValidarUsuarios();
        public Rolesusuarios? rol { get; set; }
        public string Error { get; set; }
        public string Vista { get; set; }
        public ICommand VerAgregarCommand { get; set; }
        public ICommand AgregarUsuarioCommand {  get; set; }
        public ICommand VerEditarUsuarioCommand { get; set; }
        public ICommand VerEliminarUsuariosCommand { get; set; }
        public ICommand EliminarUsuariosCommand { get; set; }
        public ICommand RegresarCommand { get; set; }

        public UsuariosViewModel()
        {
            VerAgregarCommand = new RelayCommand(VerAgregarUsuarios);
            AgregarUsuarioCommand = new RelayCommand(AgregarUsuario);
            VerEditarUsuarioCommand = new RelayCommand<Usuarios>(VerEditarUsuarios);
            VerEliminarUsuariosCommand = new RelayCommand<Usuarios>(VerEliminarUsuarios);
            EliminarUsuariosCommand = new RelayCommand(EliminarUsuarios);

            RegresarCommand = new RelayCommand(Regresar);

            ActualizarBD();
            Actualizar();
            
        }
        private void AgregarUsuario()
        {
            var resultado = Validador.Validar(Usuario);
            if (resultado == string.Empty)
            {
                if (Vista == "VerAgregarUsuarios" && Usuario != null)
                {
                    catalogousuarios.Create(Usuario);
                    Vista = "";
                }
                if (Vista == "VerEditarUsuarios" && Usuario != null)
                {
                    catalogousuarios.Update(Usuario);
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

        private void VerAgregarUsuarios()
        {
            Usuario = new();
            Vista = "VerAgregarUsuarios";
            Actualizar();
        }
        private void VerEliminarUsuarios(Usuarios usuario)
        {
            Usuario = usuario;
            Vista = "VerEliminarUsuarios";
            Actualizar();
        }

        private void EliminarUsuarios()
        {
            catalogousuarios.Delete(Usuario);
            Vista = "";
            ActualizarBD();
        }

        private void VerEditarUsuarios(Usuarios usuario)
        {
            Usuario = usuario;
            Vista = "VerEditarUsuarios";
            Actualizar();
        }
        private void Regresar()
        {
            Vista = "VerUsuarios";
            catalogousuarios.Recargar(Usuario);
            Actualizar();
        }
        private void Actualizar(string? propiedad = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propiedad));
        }

        private void ActualizarBD()
        {
            ListaUsuarios.Clear();
            ListaRolesUsuarios.Clear();
            foreach (var item in catalogousuarios.GetAllUsuarios())
            {
                ListaUsuarios.Add(item);
            }
            foreach (var item in catalogoroles.GetAllRolesUsuarios())
            {
                ListaRolesUsuarios.Add(item);
            }

            Actualizar();
        }
    }
}
