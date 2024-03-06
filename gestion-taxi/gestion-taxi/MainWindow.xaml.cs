using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
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
using System.Text.RegularExpressions;

namespace gestion_taxi
{
    public partial class MainWindow : Window
    {
        static MySqlConnection conn = new MySqlConnection("Datasource=127.0.0.1;username=root;password=;database=gestion-taxi");

        public MainWindow()
        {
            InitializeComponent();
            CheckDatabaseConnection();
        }

        private void TextBox_TextChanged(object sender, RoutedEventArgs e)
        {
           
            
        }

        private void CheckDatabaseConnection()
        {
            try
            {
                conn.Open();
                MessageBox.Show("Connection to the database successful!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error connecting to the database: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
        }
        //login
        private void btnlo_Click(object sender, RoutedEventArgs e)
        {
           
            PerformLoginCheck();
        }
        //login check
        private void PerformLoginCheck()
        {
            try
            {
                
                conn.Open();

                
                string query = "SELECT * FROM login WHERE username = @username AND password = @password";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@username", email.Text);
                cmd.Parameters.AddWithValue("@password", password.Password);

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        MessageBox.Show("Vous êtes connecté!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                        Dashboard dashboard = new Dashboard();
                        dashboard.Show();
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Nom d'utilisateur ou mot de passe incorrect!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error during login check: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
        }
    }
}