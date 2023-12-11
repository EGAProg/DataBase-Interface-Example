using Npgsql;
using System.Data;
using System.Runtime.Intrinsics.Arm;
using System.Security.Cryptography.X509Certificates;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Media;

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для moovingBook.xaml
    /// </summary>
    /// 


    public partial class MoovingBook : Window
    {
        public MoovingBook()
        {
            InitializeComponent();
            DrawHistory();
        }

        public int rID, bID;

        private void DrawHistory()
        {
            NpgsqlConnection connection = ConnectionToDB.GetDbConnection();
            connection.Open();

            try
            {
                string addQuery = "SELECT * FROM MOOVING_B;";
                using (var adapter = new NpgsqlDataAdapter(addQuery, connection))
                {
                    var dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    int i = 0;
                    int g = 10;

                    foreach (DataRow row in dataTable.Rows)
                    {
                        int idReader = Convert.ToInt32(row["ID_READER"]);
                        int idBook = Convert.ToInt32(row["ID_BOOK"]);

                        string readerQuery = "SELECT FIO FROM READERS WHERE READER_ID = @ReaderID";
                        using (var readerCmd = new NpgsqlCommand(readerQuery, connection))
                        {
                            readerCmd.Parameters.AddWithValue("ReaderID", idReader);
                            string rID = readerCmd.ExecuteScalar()?.ToString() ?? "Unknown Reader";

                            string bookQuery = "SELECT TITLE FROM BOOKS WHERE BOOK_ID = @BookID";
                            using (var bookCmd = new NpgsqlCommand(bookQuery, connection))
                            {
                                bookCmd.Parameters.AddWithValue("BookID", idBook);
                                string bID = bookCmd.ExecuteScalar()?.ToString() ?? "Unknown Book";

                                Label lb = new()
                                {
                                    Name = "lb" + Convert.ToString(row["MOOVING_ID"]),
                                    Content = $"{bID}, {rID}, {row["DATE_OUT"]}, {row["DATE_IN"]}",
                                    Width = 360,
                                    Margin = new (0, g, 0, 0),
                                    Background = new SolidColorBrush(Colors.White),
                                    Foreground = new SolidColorBrush(Colors.Black)
                                };

                                CheckBox cb = new()
                                {
                                    Name = "cb" + Convert.ToString(row["MOOVING_ID"]),
                                    Content = "На руках",
                                    Margin = new(500, g, 0, 0),
                                    IsChecked = Convert.ToBoolean(row["ON_HANDS"])
                                };

                                cb.Checked += (sender, e) => UpdateLabelContent(lb, cb);
                                cb.Unchecked += (sender, e) => UpdateLabelContent(lb, cb);


                                Grid.SetRow(cb, i);
                                Grid.SetColumn(cb, i);
                                innerGrid.Children.Add(cb);
                                Grid.SetRow(lb, i);
                                Grid.SetColumn(lb, 0);
                                innerGrid.Children.Add(lb);

                                i++;
                                g += 30; 
                            }
                        }
                    }
                }
            } catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            } finally
            {
                connection.Close();
            }
        }


        private void UpdateLabelContent(Label label, CheckBox checkBox)
        {
            string Name = checkBox.Name;
            string uName = Name.Remove(0, 2);
            MessageBox.Show(uName);
            NpgsqlConnection connection = ConnectionToDB.GetDbConnection();
            connection.Open();
            string addQuerry = $"UPDATE MOOVING_B SET ON_HANDS = '{checkBox.IsChecked}' WHERE MOOVING_ID = {Convert.ToInt32(uName)}";
            NpgsqlCommand cmd = new(addQuerry, connection);
            cmd.ExecuteNonQuery();
            connection.Close();
        }

        private void AddBookBtn_Click(object sender, RoutedEventArgs e)
        {
            NpgsqlConnection connection = ConnectionToDB.GetDbConnection();
            
            connection.Open();
            try
            {
                string addQuerry = $"SELECT READER_ID FROM READERS WHERE FIO like '{readerTextBox.Text}'";
                NpgsqlCommand cmd = new(addQuerry, connection);
                using (NpgsqlCommand command = new(addQuerry, connection))
                {
                    using NpgsqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        rID = Convert.ToInt32(reader["READER_ID"]);
                    } else
                    {
                        MessageBox.Show("No data found for the condition.");
                    }

                }
                string addQuerry2 = $"SELECT BOOK_ID FROM BOOKS WHERE TITLE like '{bookTextBox.Text}'";
                using (NpgsqlCommand command2 = new(addQuerry2, connection))
                {
                    using (NpgsqlDataReader reader2 = command2.ExecuteReader())
                    {
                        if (reader2.Read())
                        {
                            bID = Convert.ToInt32(reader2["BOOK_ID"]);
                        } else
                        {
                            MessageBox.Show("No data found for the condition.");
                        }
                    }
                }
                string querry = $"CALL INSERT_MOOVING_B('{bID}', '{rID}');";
                cmd = new(querry, connection);
                cmd.ExecuteNonQuery();
                connection.Close();
                StatusLBL.Content = "Success!";

            } catch (Exception ex)
            {
                connection.Close();
                StatusLBL.Content = ex;
            }
            finally
            {
                connection.Close();
            }
        }
    }
}
