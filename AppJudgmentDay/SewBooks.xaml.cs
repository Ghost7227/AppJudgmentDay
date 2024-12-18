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
    /// Логика взаимодействия для SewBooks.xaml
    /// </summary>
    public partial class SewBooks : Window
    {
        public SewBooks()
        {
            InitializeComponent();
            LoadUserList();
        }

        private void LoadUserList()
        {
            string filePath = "DataBaseBooks.txt";
            if (File.Exists(filePath))
            {
                var users = File.ReadAllLines(filePath);
                foreach (var user in users)
                {
                    var userDetails = user.Split('#');
                    if (userDetails.Length == 6)
                    {//название //авторы //жанры //издательство //сотрудник, который выдал //читатель
                        string formatedUser = $"{userDetails[0]} {userDetails[1]} {userDetails[2]}, {userDetails[3]} лет, {userDetails[4]}, {userDetails[5]}, {userDetails[5]}";
                        booksListBox.Items.Add(formatedUser);
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
