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
    /// Логика взаимодействия для SewWorkers.xaml
    /// </summary>
    public partial class SewWorkers : Window
    {
        public SewWorkers()
        {
            InitializeComponent();
            LoadWorkers();
        }
        private void LoadWorkers()
        {
            string filePath = "DataBaseWorkers.txt";
            if (File.Exists(filePath))
            {
                var users = File.ReadAllLines(filePath);
                foreach (var user in users)
                {
                    var userDetails = user.Split('#');
                    if (userDetails.Length == 8)
                    {
                        string formatedUser = $"{userDetails[0]}, {userDetails[1]}, {userDetails[2]}, {userDetails[3]}, {userDetails[4]}, {userDetails[5]}, {userDetails[6]}, {userDetails[7]}";
                        workersListBox.Items.Add(formatedUser);
                    }
                }
            }
            else
            {
                MessageBox.Show("Что-то пошло не так");
            }
        }

        private void workersListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string selectedItem = workersListBox.SelectedItem.ToString();
            using (var writer = new StreamWriter("BufWorkers.txt"))
            {
                writer.Write(selectedItem);
                writer.Close();
            }
            IssueBook issueBook = new IssueBook();
            this.Close();
            issueBook.ShowDialog();
        }
    }
}
