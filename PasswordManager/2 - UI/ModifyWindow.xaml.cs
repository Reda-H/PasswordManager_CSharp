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
    /// Interaction logic for ModifyWindow.xaml
    /// </summary>
    public partial class ModifyWindow : Window
    {
        public Account accountToChange;
        public ModifyWindow()
        {
            InitializeComponent();
        }

        public ModifyWindow(Account account)
        {
            InitializeComponent();
            this.accountToChange = account;
            this.DataContext = this.accountToChange;
            //idTB.Text = this.accountToChange.Id.ToString();
            //accountNameTB.Text = this.accountToChange.AccountName;
            //emailTB.Text = this.accountToChange.Email;
            //passwordTB.Text = this.accountToChange.Password;
            //extraInfo1TB.Text = this.accountToChange.ExtraInformation1;
            //extraInfo2TB.Text = this.accountToChange.ExtraInformation2;
            //extraInfo3TB.Text = this.accountToChange.ExtraInformation3;

        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void AddButton_Click_1(object sender, RoutedEventArgs e)
        {

        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ModifyButton_Click(object sender, RoutedEventArgs e)
        {
            DBConnect connect = new DBConnect();
            connect.Update(accountNameTB.Text, emailTB.Text, passwordTB.Text, extraInfo1TB.Text, extraInfo2TB.Text, extraInfo3TB.Text);
            accountToChange = new Account(accountNameTB.Text, emailTB.Text, passwordTB.Text, extraInfo1TB.Text, extraInfo2TB.Text, extraInfo3TB.Text);
            this.Close();
        }

        private void CopyExtraInfo3Button_Click(object sender, RoutedEventArgs e)
        {
            Clipboard.SetText(extraInfo3TB.Text);
        }

        private void CopyExtraInfo2Button_Click(object sender, RoutedEventArgs e)
        {
            Clipboard.SetText(extraInfo2TB.Text);
        }

        private void CopyExtraInfo1Button_Click(object sender, RoutedEventArgs e)
        {
            Clipboard.SetText(extraInfo1TB.Text);
        }

        private void CopyPasswordButton_Click(object sender, RoutedEventArgs e)
        {
            Clipboard.SetText(passwordTB.Text);
        }

        private void CopyEmailNameButton_Click(object sender, RoutedEventArgs e)
        {
            Clipboard.SetText(emailTB.Text);
        }

        private void CopyAccountNameButton_Click(object sender, RoutedEventArgs e)
        {
            Clipboard.SetText(accountNameTB.Text);
        }

        private void CopyIDButton_Click(object sender, RoutedEventArgs e)
        {
            Clipboard.SetText(idTB.Text);
        }

    }
}
