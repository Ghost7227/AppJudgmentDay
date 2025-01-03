﻿using AppJudgmentDay.Entities;
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
        public string choose;
        private void LoadUserList()
        {
            string filePath = "DataBaseUsers.txt";
            if (File.Exists(filePath))
            {
                var users = File.ReadAllLines(filePath);
                foreach (var user in users)
                {
                    var userDetails = user.Split('#');
                    if (userDetails.Length == 7)
                    {
                        string formatedUser = $"{userDetails[0]}, {userDetails[1]} {userDetails[2]} {userDetails[3]}, {userDetails[4]} лет, {userDetails[5]}, {userDetails[6]}";
                        UserListBox.Items.Add(formatedUser);
                    }
                }
            }
            else
            {
                MessageBox.Show("Что-то пошло не так");
            }
        }

        private void SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (UserListBox.SelectedItem != null)
            {
                string selectedItem = UserListBox.SelectedItem.ToString();
                using (var writer = new StreamWriter("BufReaders.txt"))
                {
                    writer.Write(selectedItem);
                    writer.Close();
                }
                if (choose == "Выдать")
                {
                    SewWorkers workers = new SewWorkers();
                    this.Close();
                    workers.ShowDialog();
                }
                if (choose == "Сдать")
                {
                    ReceiveBook receiveBook = new ReceiveBook();
                    this.Close();
                    receiveBook.ShowDialog();
                }
            }
        }
        //Выдать
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            choose = "Выдать";
            UserListBox.IsEnabled = true;
        }
        //Сдать
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            choose = "Сдать";
            UserListBox.IsEnabled = true;
        }
    }
}
