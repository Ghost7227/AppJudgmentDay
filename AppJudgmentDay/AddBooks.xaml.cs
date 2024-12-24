using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Логика взаимодействия для AddBooks.xaml
    /// </summary>
    public partial class AddBooks : Window
    {
        public string ver = "Верно";
        public string nev = "Неверно";

        private string Author;
        private string NameBook;
        private string Style;
        private string Produser;
        private string Worker;
        private string Reader;

        public string pattern = @"^[A-Za-zА-Яа-яЁё\s-*]+$";
        public AddBooks()
        {
            InitializeComponent();
        }

        private void authorTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            Author = authorTextBox.Text;
            if (Regex.IsMatch(Author, pattern))
            {
                author_result.Text = ver;
                author_result.Foreground = System.Windows.Media.Brushes.Green;
            }
            else
            {
                author_result.Text = nev;
                author_result.Foreground = System.Windows.Media.Brushes.Red;
            }
        }

        private void nameTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            NameBook = nameTextBox.Text;
            if (Regex.IsMatch(NameBook, pattern))
            {
                name_result.Text = ver;
                name_result.Foreground = System.Windows.Media.Brushes.Green;
            }
            else
            {
                name_result.Text = nev;
                name_result.Foreground = System.Windows.Media.Brushes.Red;
            }
        }

        private void styleTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            string stylepattern = @"[A-Za-zА-ЯЁа-яё]+$";
            Style = styleTextBox.Text;
            if (Regex.IsMatch(Style, stylepattern))
            {
                style_result.Text = ver;
                style_result.Foreground = System.Windows.Media.Brushes.Green;
            }
            else
            {
                style_result.Text = nev;
                style_result.Foreground = System.Windows.Media.Brushes.Red;
            }
        }

        private void produserTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            Produser = produserTextBox.Text;
            if (Regex.IsMatch(Produser, pattern))
            {
                produser_result.Text = ver;
                produser_result.Foreground = System.Windows.Media.Brushes.Green;
            }
            else
            {
                produser_result.Text = nev;
                produser_result.Foreground = System.Windows.Media.Brushes.Red;
            }
        }

        private void workerTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            Worker = workerTextBox.Text;
            if (Regex.IsMatch(Worker, pattern))
            {
                worker_result.Text = ver;
                worker_result.Foreground = System.Windows.Media.Brushes.Green;
            }
            else
            {
                worker_result.Text = nev;
                worker_result.Foreground = System.Windows.Media.Brushes.Red;
            }
        }

        private void readerTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            Reader = readerTextBox.Text;
            if (Regex.IsMatch(Reader, pattern))
            {
                reader_result.Text = ver;
                reader_result.Foreground = System.Windows.Media.Brushes.Green;
            }
            else
            {
                reader_result.Text = nev;
                produser_result.Foreground = System.Windows.Media.Brushes.Red;
            }
        }

        private void addData(object sender, RoutedEventArgs e)
        {
            if (author_result.Text == ver && name_result.Text == ver && style_result.Text == ver && produser_result.Text == ver && worker_result.Text == ver && reader_result.Text == ver)
            {
                using (StreamWriter stream = new StreamWriter("DataBaseUsers.txt", true))
                {
                    string line = $"{Author}#{NameBook}#{Style}#{}#{phoneNumber}#{email}";
                    stream.WriteLine(line);
                    stream.Close();
                }
                MessageBox.Show("Данные внесены");
                this.Close();
            }
            else
            {
                MessageB
        }

        private void cancel(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Вы уверены, что хотите вернуться назад?", "Выход", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result == MessageBoxResult.Yes)
            {
                this.Close();
            }
        }
    }
}
