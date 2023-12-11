using Npgsql;
using System.Windows;

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для newAuthor.xaml
    /// </summary>
    public partial class newAuthor : Window
    {
        public newAuthor()
        {
            InitializeComponent();
        }

        private void AddAuthorBtn_Click(object sender, RoutedEventArgs e)
        {
            NpgsqlConnection connection = ConnectionToDB.GetDbConnection();
            try
            {
                string query = $"CALL INSERT_AUTORS('{fioTextBox.Text}', '{bdayTextBox.Text}');";
                connection.Open();
                NpgsqlCommand cmd = new(query, connection);
                cmd.ExecuteNonQuery();
                connection.Close();
                StatusLBL.Content = "Success!";
            } 
            catch (Exception ex)
            {
                StatusLBL.Content = ex;
            }
        }
    }
}
