using CKK.DB.Interfaces;
using System.Windows;

namespace CKK.UI
{
    /// <summary>
    /// Interaction logic for createNewItemsPage.xaml
    /// </summary>
    public partial class CreateNewItemsWindow : Window
    {
        private readonly IConnectionFactory connectionFactory;
        public CreateNewItemsWindow(IConnectionFactory connection)
        {
            connectionFactory = connection;
            InitializeComponent();
            
        }

        private void createItemButton_Click(object sender, RoutedEventArgs e)
        {
            int quantity = int.Parse(quantityTextBox.Text);
           /*
            ShoppingCartItem newProduct = new ShoppingCartItem();
            newProduct.Price = decimal.Parse(priceTextBox.Text);
            newProduct.Name = nameTextBox.Text;

            store.AddStoreItem(newProduct, quantity);

            MessageBox.Show($"New item created!\n" +
                $"Name: \t {newProduct.Name}\n" +
                $"ID: \t {newProduct.Id}\n" +
                $"Quantity: {quantity}\n" +
                $"Price: \t {newProduct.Price:C}");
           */
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

        private void editItemButton_Click(object sender, RoutedEventArgs e)
        {
            EditItemWindow window = new EditItemWindow(connectionFactory);
            window.Show();
            this.Close();
        }

        private void removeItemButton_Click(object sender, RoutedEventArgs e)
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
