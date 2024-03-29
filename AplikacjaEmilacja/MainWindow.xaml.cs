﻿using System;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data.SqlClient;
using System.Data;

namespace AplikacjaEmilacja
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        string conectionString = "Data Source=KZ120SZOTTN\\SQLEXPRESS;Initial Catalog=Test;Integrated Security=True";

        public MainWindow()
        {
            InitializeComponent();
            FillDataGrid();
        }

        public void QuerySQL(string queryy)
        {

            SqlConnection con = new SqlConnection(conectionString);
            con.Open();
            SqlDataAdapter adpt = new SqlDataAdapter(queryy, con);
            DataTable table = new DataTable();
            adpt.Fill(table);

            sqlDataGrid.ItemsSource = table.DefaultView;
            con.Close();


        }

        public void FillDataGrid()
        {

            string queryLoadUsers = "select * from dbo.Users;";

            QuerySQL(queryLoadUsers);

        }

        private void Przycisk1_Click(object sender, RoutedEventArgs e)
        {
            string queryInsertUsers = "exec dbo.addUser @name='"+ userName.Text +"', @surname='"+ userSurname.Text +"'; ";


            if (userName.Text != "" && userSurname.Text != "")
            {

                QuerySQL(queryInsertUsers);

            } else
            {
                MessageBox.Show("Podaj imię i nazwisko użytkownika!!!");
            }
        }

        private void Usun_Click(object sender, RoutedEventArgs e)
        {
            if (sqlDataGrid.SelectedIndex != -1)
            {

                object x = sqlDataGrid.SelectedItem;
                string ID = (sqlDataGrid.SelectedCells[0].Column.GetCellContent(x) as TextBlock).Text;
                string queryDelUser = " exec dbo.delUser @id =" + ID + ";";

                QuerySQL(queryDelUser);

            } else
            {
                MessageBox.Show("Zaznacz użytkownika, którego chcesz usunąć");
            }


      
        }
                private void edytuj_click(object sender, RoutedEventArgs e)
        {

        }
    }
}
