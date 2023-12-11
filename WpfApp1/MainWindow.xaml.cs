using System.Data;
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
using Npgsql;

namespace WpfApp1
{

    public class ConnectionToDB
    {
        public static NpgsqlConnection GetDbConnection()
        {
            // Provide your database connection details here
            string connectionString = "Server=localhost;Port=5432;User Id=postgres;Password=1234;Database=postgres;";
            return new NpgsqlConnection(connectionString);
        }
    }

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void SelectsBtn_Click(object sender, RoutedEventArgs e)
        {
            Selects sel = new();
            sel.Show();            
        }

        private void NewBookBtn_Click(object sender, RoutedEventArgs e)
        {
            NewBook nb = new();
            nb.Show();
        }

        private void NewReaderBtn_Click(object sender, RoutedEventArgs e)
        {
            newReader nr = new();
            nr.Show();
            
        }

        private void NewAuthorBtn_Click(object sender, RoutedEventArgs e)
        {
            newAuthor na = new();
            na.Show();
            
        }

        private void NewGanreBtn_Click(object sender, RoutedEventArgs e)
        {
            NewGanre ng = new();
            ng.Show();
            
        }

        private void MoovingBookBtn_Click(object sender, RoutedEventArgs e)
        {
            MoovingBook mb = new();
            mb.Show();
        }
    }
}