using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
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
using Org.BouncyCastle.Crypto.Engines;
using PasswordManager._1___Classes;
using PasswordManager._2___UI;
using PasswordManager._3___DatabaseConnector;
using PasswordManager._4___ExtVariables;

namespace PasswordManager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static string root = Directory.GetCurrentDirectory();
        string dotenv = System.IO.Path.Combine(root, @"..\..\.env");
        public List<Account> Accounts = new List<Account>();
        private string MasterPass;
        public MainWindow()
        {
            DotEnv.Load(dotenv);
            Login loginWindow = new Login();
            loginWindow.ShowDialog();
            if (loginWindow.isAuthenticated)
            {
                MasterPass = loginWindow.masterPass;
                InitializeComponent();
            }
            else this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DBConnect connect = new DBConnect();
            this.Accounts = connect.Select();
            AccountDataGrid.ItemsSource = this.Accounts;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            NewAccount window = new NewAccount();
            window.ShowDialog();
            AccountDataGrid.Items.Refresh();
        }

        private void ModifyButton_Click(object sender, RoutedEventArgs e)
        {
            if (AccountDataGrid.SelectedItem != null)
            {
                Account accountToChange = (Account)AccountDataGrid.SelectedItem;
                string enc = Encryption.AESGCM.SimpleEncryptWithPassword(accountToChange.AccountName, MasterPass);
                Console.WriteLine(enc);
                string dec = Encryption.AESGCM.SimpleDecryptWithPassword(enc, MasterPass);
                Console.WriteLine(dec);
                ModifyWindow window = new ModifyWindow(accountToChange);
                window.ShowDialog();
                AccountDataGrid.Items.Refresh();
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if(AccountDataGrid.SelectedItem != null)
            {
                Account accountToChange = (Account)AccountDataGrid.SelectedItem;
                DBConnect connect = new DBConnect();
                connect.Delete(accountToChange.AccountName, accountToChange.Email);
                Accounts.Remove(accountToChange);
                AccountDataGrid.Items.Refresh();
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Row_DoubleClick(object sender, RoutedEventArgs e)
        {
            ModifyButton_Click(sender, e);
        }


        public string AESEncryption(string plain, string key, bool fips = false)
        {
            BCEngine bcEngine = new BCEngine(new AesEngine(), Encoding.ASCII);
            bcEngine.SetPadding(null);
            return bcEngine.Encrypt(plain, key);
        }

        public string AESDecryption(string cipher, string key, bool fips = false)
        {
            BCEngine bcEngine = new BCEngine(new AesEngine(), Encoding.ASCII);
            bcEngine.SetPadding(null);
            return bcEngine.Decrypt(cipher, key);
        }

    }
}
