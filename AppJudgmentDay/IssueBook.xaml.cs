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
    /// Логика взаимодействия для IssueBook.xaml
    /// </summary>
    public partial class IssueBook : Window
    {
        public string content;
        public string book;
        public IssueBook()
        {
            InitializeComponent();
            LoadUserList();
            Loadreader();
            reader.Text = content;
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
                    if (userDetails.Length == 8)
                    {
                        string formatedUser = $"{userDetails[0]}, {userDetails[1]}, {userDetails[2]}, {userDetails[3]}, {userDetails[4]}, {userDetails[5]}, {userDetails[6]}, {userDetails[7]}";
                        booksListBox.Items.Add(formatedUser);
                    }
                }
            }
            else
            {
                MessageBox.Show("Что-то пошло не так");
            }
        }

        private void Loadreader()
        {
            using (var read = new StreamReader("BufReaders.txt"))
            {
                content = read.ReadLine();
                read.Close();
            }
            using (FileStream fs = new FileStream("BufReaders.txt", FileMode.Truncate));
        }

        private void booksListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (booksListBox.SelectedItem != null)
            {
                string selectedItem = booksListBox.SelectedItem.ToString();
                
                ChooseBooks.Items.Add(selectedItem);

            }
        }
    }
}
