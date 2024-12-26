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
    /// Логика взаимодействия для ReceiveBook.xaml
    /// </summary>
    public partial class ReceiveBook : Window
    {
        public string WorkPath = "DataBaseWorkers.txt";
        public string BookPath = "DataBaseBooks.txt";
        public string reader1;
        public ReceiveBook()
        {
            InitializeComponent();
            LoadItemListView();
            Loadreader();
            reader.Text = reader1;
        }
        //Вывод сотрудников в список
        private void LoadItemListView()
        {
            if (File.Exists(WorkPath))
            {
                var users = File.ReadAllLines(WorkPath);
                foreach (var user in users)
                {
                    var userDetails = user.Split('#');
                    if (userDetails.Length == 8)
                    {
                        string formatedUser = $"{userDetails[0]}, {userDetails[1]} {userDetails[2]} {userDetails[3]}, {userDetails[4]}, {userDetails[5]}, {userDetails[6]}, {userDetails[7]}";
                        worker.Items.Add(formatedUser);
                    }
                }
            }
            else
            {
                MessageBox.Show("Что-то пошло не так");
            }
        }

        private void booksListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
        public int selectedindex;
        private void worker_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (worker.SelectedItem != null)
            {
                string selectedItem = worker.SelectedItem.ToString();
                selectedindex = worker.SelectedIndex;
                //ChooseBooks.Items.Add(selectedItem);
                //chooseBook.Add(selectedItem);
                worker.IsEnabled = false;
                MessageBox.Show(selectedItem);
                Searchbooks();
            }
        }
        //Получение данных о читателе из буферного файла
        private void Loadreader()
        {
            using (var read = new StreamReader("BufReaders.txt"))
            {
                reader1 = read.ReadLine();
                read.Close();
            }
            using (FileStream fs = new FileStream("BufReaders.txt", FileMode.Truncate)) ;
        }
        //Поиск книг, в записях о держателе которых содержится полученный читатель
        List<int> indexs = new List<int>();
        private void Searchbooks()
        {
            try
            {
                var lines = File.ReadAllLines(BookPath);
                for (int i = 0; i < lines.Length; i++)
                {
                    if (lines[i].Contains(reader1))
                    {
                        indexs.Add(i);
                        MessageBox.Show(i.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Fatality!", ex.Message);
            }
            LoadBooks();
        }

        private void LoadBooks()
        {
            try
            {
                string[] lines = File.ReadAllLines(BookPath);
                booksList.Items.Clear();
                var uniqueindex = indexs.Distinct().OrderBy(i => i);
                foreach (int index in uniqueindex)
                {
                    if (index >= 0 && index < lines.Length)
                    {
                        string line = lines[index];
                        booksList.Items.Add(line);
                    } 
                }
            }
            catch
            {
                MessageBox.Show("Ошибка чтения или вывода книг");
            }
        }


    }
}
