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
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class Reservation : Window
    {
        static MySqlConnection conn = new MySqlConnection("Datasource=127.0.0.1;username=root;password=;database=gestion-taxi");
     
        public void LoadData()
        {
            conn.Open();
            MySqlCommand command = new MySqlCommand("select * from reservation", conn);
            //client Sql commande for the list
            MySqlCommand commandClient = new MySqlCommand("SELECT CONCAT(nom, ' ', prenom) AS np FROM client;", conn);
            //Taxis Sql commande for the list
            MySqlCommand commandMatricule = new MySqlCommand("SELECT matricule from  taxi where etat=0;", conn);
            DataTable dt = new DataTable();
            dt.Load(command.ExecuteReader());
            //reader of client
            MySqlDataReader readerCLient = commandClient.ExecuteReader();

            while (readerCLient.Read())
            {
                ClientList.Items.Add(readerCLient.GetString("np"));
            }
            readerCLient.Close();
            //reader of taxis
            MySqlDataReader readerTaxi = commandMatricule.ExecuteReader();

            while (readerTaxi.Read())
            {
                TaxiList.Items.Add(readerTaxi.GetString("matricule"));
            }
            readerTaxi.Close();

            //ClientList.ItemsSource = dataclient;
            DataGrid1.ItemsSource = dt.DefaultView;
            conn.Close();

        }
        public bool isValid()
        {
            if (string.IsNullOrWhiteSpace(PrixText.Text))
            {
                MessageBox.Show("Entrer le prix svp !", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;


            }
            else if (!Regex.IsMatch(PrixText.Text, @"^\d+$"))
            {
                MessageBox.Show("Entrer le prix ??", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (string.IsNullOrWhiteSpace(HeureText.Text))
            {
                MessageBox.Show("Entrer l'heure svp !", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            else if (!Regex.IsMatch(HeureText.Text, @"^[01][0-9]|2[0-3]:[0-5][0-9]$"))
            {
                MessageBox.Show("Entrer l'heure ??", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }


            return true;
        }
        string select_radio = default;
        private void RadioButton_Compl(object sender, RoutedEventArgs e)
        {
            select_radio = "complete";
        }

        private void RadioButton_Incompl(object sender, RoutedEventArgs e)
        {
            select_radio = "incomplete";
        }
        public Reservation()
        {
            InitializeComponent();
            LoadData();

        }
        public void clearData()
        {
            ClientList.SelectedIndex = -1;
            TaxiList.SelectedIndex = -1;
            PrixText.Clear();
            HeureText.Clear();
            DateText.SelectedDate = null;
            Complete.IsChecked = false;
            Incomplete.IsChecked = false;


        }


        private void AjouterBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (isValid())
                {
                    MySqlCommand cmd = new MySqlCommand("INSERT INTO reservation (client,matriculetaxi,prix,dateHeure,status) VALUES(@client, @matriculetaxi,@prix, @dateHeure,@status)", conn);
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@client", ClientList.Text);
                    cmd.Parameters.AddWithValue("@matriculetaxi", TaxiList.Text);
                    cmd.Parameters.AddWithValue("@prix", PrixText.Text);
                    string dateheure = DateText.Text + " " + HeureText.Text;
                    cmd.Parameters.AddWithValue("@dateHeure", dateheure);
                    cmd.Parameters.AddWithValue("@status", select_radio);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    LoadData();
                    MessageBox.Show("Success", "Saved", MessageBoxButton.OK, MessageBoxImage.Information);
                    clearData();

                }
                else
                {
                    isValid();
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }



        private void RetourBtn_Click(object sender, RoutedEventArgs e)
        {
            Dashboard dashboard = new Dashboard();
            dashboard.Show();
            this.Close();
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }



        private void PrixText_TextChanged(object sender, TextChangedEventArgs e)
        {
        }
        String Id_res = default;
        private void DataGrid1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid dg1 = (DataGrid)sender;
            DataRowView row_selected = dg1.SelectedItem as DataRowView;
            if (row_selected != null)
            {
                String heure = row_selected["dateheure"].ToString().Split()[1];
                String date = row_selected["dateheure"].ToString().Split()[0];
                Id_res = row_selected["id_reservation"].ToString();
                ClientList.Text = row_selected["client"].ToString();
                TaxiList.Text = row_selected["matriculetaxi"].ToString();
                PrixText.Text = row_selected["prix"].ToString();
                HeureText.Text = heure;
                DateText.Text = date;
                if (row_selected["status"].ToString() == "complete")
                {
                    Complete.IsChecked = true;
                }
                else
                {
                    Incomplete.IsChecked = true;
                }
            }
        }
        private void ModifierBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (isValid())
                {
                    MySqlCommand cmd = new MySqlCommand("UPDATE reservation SET client=@client,matriculetaxi=@matriculetaxi,prix=@prix,dateHeure=@dateHeure,status=@status WHERE id_reservation = @id", conn);
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@client", ClientList.Text);
                    cmd.Parameters.AddWithValue("@matriculetaxi", TaxiList.Text);
                    cmd.Parameters.AddWithValue("@prix", PrixText.Text);
                    string dateheure = DateText.Text + " " + HeureText.Text;
                    cmd.Parameters.AddWithValue("@dateHeure", dateheure);
                    cmd.Parameters.AddWithValue("@status", select_radio);
                    cmd.Parameters.AddWithValue("@id", Id_res);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    LoadData();
                    MessageBox.Show("Success", "Saved", MessageBoxButton.OK, MessageBoxImage.Information);
                    clearData();
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void SupprimerBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                MySqlCommand cmd = new MySqlCommand("DELETE FROM reservation WHERE id_reservation = @id", conn);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@id", Id_res);
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
                LoadData();
                MessageBox.Show("Success", "Saved", MessageBoxButton.OK, MessageBoxImage.Information);
                clearData();

            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
