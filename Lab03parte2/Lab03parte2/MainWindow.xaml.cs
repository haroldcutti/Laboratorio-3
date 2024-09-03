using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Lab03parte2
{
    public partial class MainWindow : Window
    {
        private string connectionString = "Server=LAB1507-19\\SQLEXPRESS03; Database=Tecsup2023DB; Integrated Security=True";

        public MainWindow()
        {
            InitializeComponent();
            LoadStudents(); 
        }

        private void LoadStudents(string searchTerm = "")
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT * FROM Students";
                if (!string.IsNullOrEmpty(searchTerm))
                {
                    query += " WHERE FirstName LIKE @SearchTerm";
                }

                SqlCommand command = new SqlCommand(query, connection);
                if (!string.IsNullOrEmpty(searchTerm))
                {
                    command.Parameters.AddWithValue("@SearchTerm", $"%{searchTerm}%");
                }

                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);

                dataGridStudents.ItemsSource = dataTable.DefaultView;
            }
        }

        private void BtnBuscar_Click(object sender, RoutedEventArgs e)
        {
            string searchTerm = txtSearch.Text;
            LoadStudents(searchTerm);
        }
    }
}