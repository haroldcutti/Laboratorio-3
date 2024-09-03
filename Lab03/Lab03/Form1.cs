using System.Data;
using System.Data.SqlClient;

namespace Lab03
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string cadena = "Server=LAB1507-19\\SQLEXPRESS03; Database=Tecsup2023DB; Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(cadena))
                {
                    connection.Open();
                    MessageBox.Show("Conexión exitosa");

                    string query = "SELECT * FROM Students";
                    SqlCommand command = new SqlCommand(query, connection);
                    SqlDataAdapter adapter = new SqlDataAdapter(command);

                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    dataGridView1.DataSource = dataTable;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Error de conexión");
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            CargarDatosEnDataGridView2();
        }

        private void CargarDatosEnDataGridView2()
        {
            string cadena = "Server=LAB1507-19\\SQLEXPRESS03; Database=Tecsup2023DB; Integrated Security=True";
            using (SqlConnection connection = new SqlConnection(cadena))
            {
                connection.Open();
                string query = "SELECT * FROM Students"; 
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataReader reader = command.ExecuteReader();

                List<Cliente> listaClientes = new List<Cliente>();

                while (reader.Read())
                {
                    Cliente cliente = new Cliente();
                    cliente.IdCliente = reader["StudentId"].ToString();
                    cliente.NombreContacto = reader["FirstName"].ToString();
                    listaClientes.Add(cliente);
                }

                dataGridView2.DataSource = listaClientes; 
            }
        }

        public class Cliente
        {
            public string IdCliente { get; set; }
            public string NombreContacto { get; set; }
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
