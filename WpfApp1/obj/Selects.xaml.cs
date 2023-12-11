using Npgsql;
using System.Windows;

namespace WpfApp1
{

    /// <summary>
    /// Логика взаимодействия для selects.xaml
    /// </summary>
    public partial class Selects : Window
    {
        public Selects()
        {
            InitializeComponent();
            ComboBooks();
            ComboAuthors();
            ComboGanres();
            ComboReaders();
        }
        private void ComboBooks()
        {
            try
            {
                NpgsqlConnection connection = new(connectionString: "Server=localhost;Port=5432;User Id=postgres;Password=1234;Database=postgres;");
                connection.Open();
                string query = "SELECT TITLE FROM BOOKS";
                using NpgsqlCommand command = new(query, connection);
                NpgsqlDataReader npgsqlDataReader = command.ExecuteReader();
                using NpgsqlDataReader reader = npgsqlDataReader;
                List<string> items = [];

                while (reader.Read())
                {
                    string columnName = reader.GetString(0);
                    items.Add(columnName);
                }

                ComboBook.ItemsSource = items;
            } catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void ComboReaders()
        {
            try
            {
                NpgsqlConnection connection = new(connectionString: "Server=localhost;Port=5432;User Id=postgres;Password=1234;Database=postgres;");
                connection.Open();
                string query = "SELECT FIO FROM READERS";
                NpgsqlCommand npgsqlCommand = new(query, connection);
                using NpgsqlCommand command = npgsqlCommand;
                NpgsqlDataReader npgsqlDataReader = command.ExecuteReader();
                using NpgsqlDataReader reader = npgsqlDataReader;
                List<string> items = [];

                while (reader.Read())
                {
                    string columnName = reader.GetString(0);
                    items.Add(columnName);
                }

                ComboReader.ItemsSource = items;
            } catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void ComboAuthors()
        {
            try
            {
                NpgsqlConnection connection = new(connectionString: "Server=localhost;Port=5432;User Id=postgres;Password=1234;Database=postgres;");
                connection.Open();
                string query = "SELECT AUTHOR_NAME FROM AUTHORS";
                NpgsqlCommand npgsqlCommand = new(query, connection);
                using NpgsqlCommand command = npgsqlCommand;
                using NpgsqlDataReader reader = command.ExecuteReader();
                List<string> items = [];

                while (reader.Read())
                {
                    string columnName = reader.GetString(0);
                    items.Add(columnName);
                }

                ComboAuthor.ItemsSource = items;
            } catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void ComboGanres()
        {
            try
            {
                NpgsqlConnection connection = new(connectionString: "Server=localhost;Port=5432;User Id=postgres;Password=1234;Database=postgres;");
                connection.Open();
                string query = "SELECT GANRE_NAME FROM GANRES";
                NpgsqlCommand npgsqlCommand = new(query, connection);
                using NpgsqlCommand command = npgsqlCommand;
                NpgsqlDataReader npgsqlDataReader = command.ExecuteReader();
                using NpgsqlDataReader reader = npgsqlDataReader;
                List<string> items = [];

                while (reader.Read())
                {
                    string columnName = reader.GetString(0);
                    items.Add(columnName);
                }

                ComboGanre.ItemsSource = items;
            } catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
