using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ProyectoRefaccionaria2.Views.UsuariosViews
{
    /// <summary>
    /// Interaction logic for LoginView.xaml
    /// </summary>
    public partial class LoginView : UserControl
    {
        public LoginView()
        {
            InitializeComponent();
        }

        private void pwb1_TextChanged(object sender, TextChangedEventArgs e)
        {
            
        }

        private void pwb1_TextChanged_1(object sender, TextChangedEventArgs e)
        {
            txtPassword.Clear();
            txtPassword.Text = "";
            
        }
    }
}
