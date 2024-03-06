using System;
using MySql.Data.MySqlClient;
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
using System.Reflection;
using System.Data.Common;
namespace gestion_taxi
{

    public partial class Client : Window
    {
        public Client()
        {
            InitializeComponent();
            LoadGrid();
        }
        static MySqlConnection con = new MySqlConnection("Datasource = 127.0.0.1; username=root;password=;database=gestion-taxi");



        public void clearData()
        {

            prenom_txt.Clear();
            nom_txt.Clear();
            numero_tel_txt.Clear();

        }
        public void LoadGrid()
        {
            MySqlCommand cmd = new MySqlCommand("select * from client", con);
            DataTable dt = new DataTable();
            con.Open();
            MySqlDataReader sdr = cmd.ExecuteReader();
            dt.Load(sdr);
            con.Close();
            dataGrid.ItemsSource = dt.DefaultView;
        }

        string idclient = default;
        private void dataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid dg1 = (DataGrid)sender;
            DataRowView row_selected = dg1.SelectedItem as DataRowView;
            if (row_selected != null)
            {
                idclient = row_selected["id_client"].ToString();
                nom_txt.Text = row_selected["nom"].ToString();
                prenom_txt.Text = row_selected["prenom"].ToString();
                numero_tel_txt.Text = row_selected["num_tel"].ToString();

            }
        }
        private void ClearDataBtn_Click(object sender, RoutedEventArgs e)
        {
            clearData();
        }
        public bool isValid()
        {

            if (prenom_txt.Text == string.Empty)
            {
                MessageBox.Show("prenom est obligatoire ", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (nom_txt.Text == string.Empty)
            {
                MessageBox.Show("nom est obligatoire ", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (numero_tel_txt.Text == string.Empty)
            {
                MessageBox.Show("numero telephone est obligatoire ", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            return true;
        }
        //fonction pour ajouter
        private void InsertBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (isValid())
                {
                    MySqlCommand cmd = new MySqlCommand("INSERT INTO client( nom,prenom,num_tel ) VALUES (@nom, @prenom, @numero)", con);
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@prenom", prenom_txt.Text);
                    cmd.Parameters.AddWithValue("@nom", nom_txt.Text);
                    cmd.Parameters.AddWithValue("@numero", numero_tel_txt.Text);

                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    LoadGrid();
                    MessageBox.Show("Ajout effectué avec succès !!", "Saved", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
            }





        }

        private void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand("DELETE FROM client WHERE id_client = @id", con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@id", idclient);

                cmd.ExecuteNonQuery();
                con.Close();
                LoadGrid();
                MessageBox.Show("Suppression effectuée avec succès !!", "Saved", MessageBoxButton.OK, MessageBoxImage.Information);
                clearData();

            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }





        private void UpdateBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                con.Open();
                if (isValid())
                {
                    MySqlCommand cmd = new MySqlCommand("UPDATE client SET nom=@nom,prenom=@prenom,num_tel=@numero WHERE id_client = @id", con);
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@id", idclient);
                    cmd.Parameters.AddWithValue("@prenom", prenom_txt.Text);
                    cmd.Parameters.AddWithValue("@nom", nom_txt.Text);
                    cmd.Parameters.AddWithValue("@numero", numero_tel_txt.Text);

                    cmd.ExecuteNonQuery();
                    con.Close();

                    // Reload the DataGrid after the update
                    LoadGrid();

                    MessageBox.Show("Modification effectuée avec succès !!", "Saved", MessageBoxButton.OK, MessageBoxImage.Information);
                    clearData();
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


    }
}