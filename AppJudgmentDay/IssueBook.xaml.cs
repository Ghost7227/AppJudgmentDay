using AppJudgmentDay.Entities;
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
        public string issueworker;
        public string content;
        public string book;
        private string author;
        private string nameBook;
        private string bookStyle;
        private string produser;
        private string isbn;
        private string ID;
        public string[] workerArr;
        public string[] holderArr;
        public string workerObrez;
        public string holderObrez;
        public int selectedindex;
        List<string> chooseBook = new List<string>();
        public string[] bookdata;
        private string lastHolder;

        public IssueBook()
        {
            InitializeComponent();
            LoadUserList();
            Loadreader();
            Loadworker();
            reader.Text = content;
            worker.Text = issueworker;
            Raschlenenie();
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
        //Получение данных работника из буферного файла
        private void Loadworker()
        {
            using (var work = new StreamReader("BufWorkers.txt"))
            {
                issueworker = work.ReadLine();
                work.Close();
            }
            using (FileStream fd = new FileStream("BufWorkers.txt", FileMode.Truncate)) ;
        }
        //Получение данных читателя из буферного файла
        private void Loadreader()
        {
            using (var read = new StreamReader("BufReaders.txt"))
            {
                content = read.ReadLine();
                read.Close();
            }
            using (FileStream fs = new FileStream("BufReaders.txt", FileMode.Truncate)) ;
        }
        //Обработка выбора книги
        private void booksListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (booksListBox.SelectedItem != null)
            {
                string selectedItem = booksListBox.SelectedItem.ToString();//Получаем элемент списка в виде строки
                selectedindex = booksListBox.SelectedIndex;//Получаем индекс выбранного элемента
                ChooseBooks.Items.Add(selectedItem);//Добавляем выбранный элемент в список выбранных
                chooseBook.Add(selectedItem);
                booksListBox.IsEnabled = false;//Делаем первый список недоступным
                iss.IsEnabled = true;
            }
        }
                
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string choosebookstr = chooseBook[0];
            bookdata = choosebookstr.Split(',');
            ID = bookdata[0];
            isbn = bookdata[1];
            author = bookdata[2];
            nameBook = bookdata[3];
            bookStyle = bookdata[4];
            produser = bookdata[5];
            lastHolder = bookdata[6];
            //Изменяем данные о держателе книги и сотруднеке, выдавшем ее
            var temp = DB.Books.GetAll().ToArray();
            DB.Books.Modify(temp[selectedindex], new Book()
            {
                Idbook = ID,
                Isbn = isbn,
                Author = author,
                Title = nameBook,
                Janre = bookStyle,
                Produser = produser,
                Holder = $"{holderObrez}; {lastHolder}",//Запись дополнительного пользователя
                Work = workerObrez //работник, выдавший книгу
            });
            DB.Books.SaveAll();
            //Делаем запись в журнал выдачи
            string lineWrite = $"({workerObrez}) выдал ({holderObrez}) книгу ({bookdata[0]}{bookdata[1]}{bookdata[2]}{bookdata[3]}{bookdata[5]})";
            using (StreamWriter cw = new StreamWriter("JurnalIssue.txt", true))
            {
                cw.WriteLine(lineWrite);
                cw.Close();
            }
            this.Close();
        }
        private void Raschlenenie()
        {
            //Получаем данные о работнике в виде массива строк и берем нужные нам части
            workerArr = issueworker.Split(',');
            workerObrez = workerArr[7] + workerArr[1] + workerArr[2] + workerArr[3];
            //Получаем данные о читателе в виде массива строк и берем нужные нам части
            holderArr = content.Split(',');
            holderObrez = holderArr[0] + holderArr[1];
        }
        
    }
}
