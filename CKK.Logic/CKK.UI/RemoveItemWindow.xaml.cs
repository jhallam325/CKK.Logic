using CKK.DB.Interfaces;
using System.Windows;

namespace CKK.UI
{
    /// <summary>
    /// Interaction logic for RemoveItem.xaml
    /// </summary>
    public partial class RemoveItemWindow : Window
    {
        private readonly IConnectionFactory connectionFactory;
        public RemoveItemWindow(IConnectionFactory connection)
        {
            connectionFactory = connection;
            InitializeComponent();
        }

        private void deleteItemButton_Click(object sender, RoutedEventArgs e)
        {
            int id = int.Parse(idTextBox.Text);
            int quantity = int.Parse(quantityTextBox.Text);

            //store.RemoveStoreItem(id, quantity);

            MessageBox.Show("You have removed the following item(s):\n" +
                $"ID: \t{id}\n" +
                $"Quantity: {quantity}\n");
        }

        private void homeButton_Click(object sender, RoutedEventArgs e)
        {
            HomeWindow window = new HomeWindow(connectionFactory);
            window.Show();
            this.Close();
        }

        private void viewAllItemsButton_Click(object sender, RoutedEventArgs e)
        {
            AllItemsWindow window = new AllItemsWindow(connectionFactory);
            window.Show();
            this.Close();
        }

        private void createItemButton_Click(object sender, RoutedEventArgs e)
        {
            CreateNewItemsWindow window = new CreateNewItemsWindow(connectionFactory);
            window.Show();
            this.Close();
        }

        private void editItemButton_Click(object sender, RoutedEventArgs e)
        {
            EditItemWindow window = new EditItemWindow(connectionFactory);
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
