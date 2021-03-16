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
using System.Windows.Shapes;

namespace PasswordManager._2___UI
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public bool isAuthenticated = false;
        public string masterPass = "qwertyuiopasdf";

        public Login()
        {
            InitializeComponent();
        }

        private void ConnectButton_Click(object sender, RoutedEventArgs e)
        {
            if(MasterPassTextBox.Text.Equals(masterPass))
            {
                isAuthenticated = true;
                this.Close();
            }
            else
            {
                isAuthenticated = false;
            }
        }
    }
}
