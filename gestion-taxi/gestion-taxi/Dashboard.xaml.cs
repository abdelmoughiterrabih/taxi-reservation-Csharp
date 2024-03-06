using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
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

namespace gestion_taxi
{
    /// <summary>
    /// Interaction logic for Dashboard.xaml
    /// </summary>
    public partial class Dashboard : Window
    {
        static MySqlConnection conn = new MySqlConnection("Datasource=127.0.0.1;username=root;password=;database=gestion-taxi");

        public Dashboard()
        {
            InitializeComponent();
            
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

        private void client_Click(object sender, RoutedEventArgs e)
        {
            
            Client client = new Client();
            client.Show();
            
            this.Close();
        }

        private void taxi_Click(object sender, RoutedEventArgs e)
        { 
            Taxi taxi = new Taxi();
            taxi.Show();

            this.Close();
        }
        private void rd_Click(object sender, RoutedEventArgs e)
        {
            Reservation reservation = new Reservation();
            reservation.Show();
            this.Close();
        }

        private void chauffeur_Click(object sender, RoutedEventArgs e)
        {
            Chauffeur chauffeur = new Chauffeur();
            chauffeur.Show();
            this.Close();
        }
    }
}
