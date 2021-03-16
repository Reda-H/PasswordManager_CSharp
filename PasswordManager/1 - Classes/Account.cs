using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager._1___Classes
{
    public class Account
    {
        public Int32 Id { get; set; }
        public string AccountName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ExtraInformation1 { get; set; }
        public string ExtraInformation2 { get; set; }
        public string ExtraInformation3 { get; set; }

        public Account()
        {
            AccountName = "";
            Email = "";
            Password = "";
        }

        public Account(string accountName,  string email, string password)
        {
            AccountName = accountName;
            Email = email;
            Password = password;
        }

        public Account(string accountName, string email, string password,
            string extraInformation1 = "", string extraInformation2 = "", string extraInformation3 = "")
        {
            AccountName = accountName;
            Email = email;
            Password = password;
            ExtraInformation1 = extraInformation1;
            ExtraInformation2 = extraInformation2;
            ExtraInformation3 = extraInformation3;
        }

        public Account(int id, string accountName, string email, string password,
            string extraInformation1 = "", string extraInformation2 = "", string extraInformation3 = "")
        {
            Id = id;
            AccountName = accountName;
            Email = email;
            Password = password;
            ExtraInformation1 = extraInformation1;
            ExtraInformation2 = extraInformation2;
            ExtraInformation3 = extraInformation3;
        }

    }
}
