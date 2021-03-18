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
        //public string masterPass = "qwertyuiopasdf";
        public string masterPass;

        public Login()
        {
            masterPass = System.IO.File.ReadAllText(@"C:\Users\Public\Documents\TestFolder\MPass.txt");
            //System.IO.File.WriteAllText(@"C:\Users\Public\Documents\TestFolder\MPass.txt", Encryption.AESGCM.SimpleEncryptWithPassword(masterPass, Environment.GetEnvironmentVariable("DEFAULT_MASTERPASS")));
            InitializeComponent();
        }

        private void ConnectButton_Click(object sender, RoutedEventArgs e)
        {
            if (
                MasterPassTextBox.Text.Equals(
                    Encryption.AESGCM.SimpleDecryptWithPassword(
                        masterPass, 
                        Environment.GetEnvironmentVariable("DEFAULT_MASTERPASS")
                        )
                    )
                )
            {
                isAuthenticated = true;
                this.Close();
            }
            else
            {
                isAuthenticated = false;
            }
        }

        private void OnKeyDownHandler(object sender, KeyEventArgs e)
        {
            if (
                MasterPassTextBox.Text.Equals(
                    Encryption.AESGCM.SimpleDecryptWithPassword(
                        masterPass,
                        Environment.GetEnvironmentVariable("DEFAULT_MASTERPASS")
                        )
                    )
                )
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
