using Npgsql;
using System.Windows;

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для newGanre.xaml
    /// </summary>
    public partial class NewGanre : Window
    {
        public NewGanre()
        {
            InitializeComponent();
        }

        private void AddGanreBtn_Click(object sender, RoutedEventArgs e)
        {
            NpgsqlConnection connection = new(connectionString: "Server=localhost;Port=5432;User Id=postgres;Password=1234;Database=postgres;");
            try
            {
                string query = $"CALL INSERT_GANRES('{opisTextBox.Text}', '{titleTextBox.Text}');";
                connection.Open();
                NpgsqlCommand cmd = new(query, connection);
                cmd.ExecuteNonQuery();
                connection.Close();
                StatusLBL.Content = "Success!";
            }
            catch(Exception ex)
            {
                StatusLBL.Content = ex;
            }
        }
    }
}
