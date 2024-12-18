using System;
using System.Collections.Generic;
using System.IO;
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

namespace AppJudgmentDay
{
    /// <summary>
    /// Логика взаимодействия для Userlist.xaml
    /// </summary>
    public partial class Userlist : Window
    {
        public Userlist()
        {
            InitializeComponent();
            LoadUserList();
        }
        
        private void LoadUserList()
        {
            string filePath = "DataBaseUsers.txt";
            if (File.Exists(filePath))
            {
                var users = File.ReadAllLines(filePath);
                foreach ( var user in users )
                {
                    var userDetails = user.Split('#');
                    if ( userDetails.Length == 6 ) 
                    {
                        string formatedUser = $"{userDetails[0]} {userDetails[1]} {userDetails[2]}, {userDetails[3]} лет, {userDetails[4]}, {userDetails[5]}";
                        UserListBox.Items.Add( formatedUser );
                    }
                }
            }
            else
            {
                MessageBox.Show("Что-то пошло не так");
            }
        }
    }
}
