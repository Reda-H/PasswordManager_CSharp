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
using PasswordManager._1___Classes;
using PasswordManager._3___DatabaseConnector;

namespace PasswordManager._2___UI
{
    /// <summary>
    /// Interaction logic for NewAccount.xaml
    /// </summary>
    public partial class NewAccount : Window
    {

        public Account newAccount { get; set; }
        public NewAccount()
        {
            InitializeComponent();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            newAccount = new Account();
            newAccount.AccountName = accountNameTB.Text;
            newAccount.Email = emailTB.Text;
            newAccount.Password = passwordTB.Text;
            newAccount.ExtraInformation1 = extraInfo1TB.Text;
            newAccount.ExtraInformation2 = extraInfo2TB.Text;
            newAccount.ExtraInformation3 = extraInfo3TB.Text;

            DBConnect connection = new DBConnect();
            connection.Insert(newAccount.AccountName, newAccount.Email, newAccount.Password,
                newAccount.ExtraInformation1, newAccount.ExtraInformation2, newAccount.ExtraInformation3);

            ((MainWindow)Application.Current.MainWindow).Accounts.Add(newAccount);

            this.Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
