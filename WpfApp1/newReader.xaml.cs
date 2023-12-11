using Npgsql;
using System.Windows;

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для newReader.xaml
    /// </summary>
    public partial class newReader : Window
    {
        public newReader()
        {
            InitializeComponent();
        }

        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            NpgsqlConnection connection = ConnectionToDB.GetDbConnection();
            try
            {
                string querry = $"CALL INSERT_READERS('${FIOTextBox.Text}', '{AddressTextBox.Text}', '{SeriaTextBox.Text}', '{NomerTextBox.Text}', '{VydanTextBox.Text}', '{PhoneTextBox.Text}');";
                connection.Open();
                NpgsqlCommand cmd = new(querry, connection);
                cmd.ExecuteNonQuery();
                connection.Close();
                StatusLBL.Content = "Success!";
            } catch (Exception ex)
            {
                StatusLBL.Content = ex;
            }
        }
    }
}
