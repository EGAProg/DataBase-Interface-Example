using Npgsql;
using System.Windows;

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для newBook.xaml
    /// </summary>
    public partial class NewBook : Window
    {
        public NewBook()
        {
            InitializeComponent();
        }

        public int aID;
        public int gID;

        private void AddBookBtn_Click(object sender, RoutedEventArgs e)
        {
            NpgsqlConnection connection = new(connectionString: "Server=localhost;Port=5432;User Id=postgres;Password=1234;Database=postgres;");
            connection.Open();
            try
            {
                string addQuerry = $"SELECT AUTHOR_ID FROM AUTHORS WHERE AUTHOR_NAME like '{AuthorTextBox.Text}'";
                NpgsqlCommand cmd = new(addQuerry, connection);
                using (NpgsqlCommand command = new(addQuerry, connection))
                {
                    using NpgsqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        aID = Convert.ToInt32(reader["AUTHOR_ID"]);
                    } else
                    {
                        MessageBox.Show("No data found for the condition.");
                    }
                    
                }
                string addQuerry2 = $"SELECT GANRE_ID FROM GANRES WHERE GANRE_NAME like '{GanreTextBox.Text}'";
                using (NpgsqlCommand command2 = new(addQuerry2, connection))
                {
                    MessageBox.Show("Тест2");
                    using (NpgsqlDataReader reader2 = command2.ExecuteReader())
                    {
                        if (reader2.Read())
                        {
                            gID = Convert.ToInt32(reader2["GANRE_ID"]);
                        } else
                        {
                            MessageBox.Show("No data found for the condition.");
                        }
                    }
                }
                string querry = $"CALL INSERT_BOOKS('{titleTextBox.Text}', '{gID}', '{aID}', '{QuantityTextBox.Text}');";
                cmd = new(querry, connection);
                cmd.ExecuteNonQuery();
                connection.Close();
                StatusLBL.Content = "Success!";

            } catch (Exception ex)
            {
                StatusLBL.Content = ex;
                MessageBox.Show(e.ToString());
            }
        }
    }
}
