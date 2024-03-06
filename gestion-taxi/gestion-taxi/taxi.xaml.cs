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
using Microsoft.Data.SqlClient;
using MySql.Data.MySqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Numerics;

namespace gestion_taxi
{

    public partial class Taxi : Window
    {
        static MySqlConnection con = new MySqlConnection(@"Datasource=127.0.0.1;username=root;password=;database=gestion-taxi");
        public void LoadGrid()
        {
            con.Open();
            MySqlCommand command = new MySqlCommand("select id_taxi,marque,matricule,nbrplace from taxi", con);
            DataTable dt = new DataTable();

            dt.Load(command.ExecuteReader());
            DataGrid.ItemsSource = dt.DefaultView;

            con.Close();
        }
        String Id_taxi = default;
        private void DataGrid1_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            DataGrid dg1 = (DataGrid)sender;
            DataRowView row_selected = dg1.SelectedItem as DataRowView;
            if (row_selected != null)
            {
                Id_taxi = row_selected["id_taxi"].ToString();
                Marque_txt.Text = row_selected["marque"].ToString();
                Matricule_txt.Text = row_selected["matricule"].ToString();
                NbrPlace_txt.Text = row_selected["nbrplace"].ToString();

            }
        }

        public Taxi()
        {
            InitializeComponent();
            LoadGrid();
        }



        public void ClearData()
        {
            Marque_txt.Clear();
            Matricule_txt.Clear();
            NbrPlace_txt.Clear();


        }


        private void ClearDataBtn_Click(object sender, RoutedEventArgs e)
        {
            ClearData();
        }

        public bool isValid()
        {
            if (Marque_txt.Text == string.Empty)
            {
                MessageBox.Show("Veuillez saisir la marque", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (Matricule_txt.Text == string.Empty)
            {
                MessageBox.Show("Veuillez saisir le matricule", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (NbrPlace_txt.Text == string.Empty)
            {
                MessageBox.Show("Veuillez saisir le nombre de place", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            return true;
        }

        private void InsertBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (isValid())
                {
                    MySqlCommand cmd = new MySqlCommand("INSERT INTO taxi (Marque, Matricule, NbrPlace,Etat) VALUES (@Marque, @Matricule, @NbrPlace,0)", con);
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@Marque", Marque_txt.Text);
                    cmd.Parameters.AddWithValue("@Matricule", Matricule_txt.Text);
                    cmd.Parameters.AddWithValue("@NbrPlace", NbrPlace_txt.Text);
                    //  cmd.Parameters.AddWithValue("@Etat", Etat_txt.Text);

                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    LoadGrid();
                    MessageBox.Show("Ajout effectué avec succès !", "Saved", MessageBoxButton.OK, MessageBoxImage.Information);
                    ClearData();
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
                MySqlCommand cmd = new MySqlCommand("DELETE FROM taxi WHERE id_taxi = @id", con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@id", Id_taxi);

                cmd.ExecuteNonQuery();
                con.Close();
                LoadGrid();
                MessageBox.Show("Suppression effectuée avec succès !", "Saved", MessageBoxButton.OK, MessageBoxImage.Information);
                ClearData();

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
                    MySqlCommand cmd = new MySqlCommand("UPDATE taxi SET marque=@Marque,matricule=@Matricule,nbrplace=@NbrPlace WHERE id_taxi = @id", con);
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@id", Id_taxi);
                    cmd.Parameters.AddWithValue("@Marque", Marque_txt.Text);
                    cmd.Parameters.AddWithValue("@Matricule", Matricule_txt.Text);
                    cmd.Parameters.AddWithValue("@NbrPlace", NbrPlace_txt.Text);

                    cmd.ExecuteNonQuery();
                    con.Close();
                    LoadGrid();
                    MessageBox.Show("Modification effectuée avec succès !", "Saved", MessageBoxButton.OK, MessageBoxImage.Information);
                    ClearData();

                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void RetourBtn_Click(Object sender, RoutedEventArgs e)
        {
            Dashboard dashboard = new Dashboard();
            dashboard.Show();
            this.Close();
        }

    }
}


