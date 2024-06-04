using CKK.DB.Interfaces;
using System.Windows;


namespace CKK.UI
{
    /// <summary>
    /// Interaction logic for HomePage.xaml
    /// </summary>
    public partial class HomeWindow : Window
    {
        private readonly IConnectionFactory connectionFactory;

        public HomeWindow(IConnectionFactory connection)
        {
            connectionFactory = connection;
            InitializeComponent();
        }

        private void viewAllItemsButton_Click(object sender, RoutedEventArgs e)
        {
            AllItemsWindow window = new AllItemsWindow(connectionFactory);
            window.Show();
            this.Close();
        }

        private void createNewItemsButton_Click(object sender, RoutedEventArgs e)
        {
            CreateNewItemsWindow page = new CreateNewItemsWindow(connectionFactory);
            page.Show();
            this.Close();
        }

        private void editItemsButton_Click(object sender, RoutedEventArgs e)
        {
            EditItemWindow window = new EditItemWindow(connectionFactory);
            window.Show();
            this.Close();
        }

        private void removeItemsButton_Click(object sender, RoutedEventArgs e)
        {
            RemoveItemWindow window = new RemoveItemWindow(connectionFactory);
            window.Show();
            this.Close();
        }

        private void logoutButton_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow page = new LoginWindow(connectionFactory);
            page.Show();
            this.Close();
        }
    }
}
