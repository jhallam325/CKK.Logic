using CKK.DB.Interfaces;
using CKK.DB.UOW;
using CKK.Logic.Models;
using System.Windows;

namespace CKK.UI
{
    /// <summary>
    /// Interaction logic for RemoveItem.xaml
    /// </summary>
    public partial class RemoveItemWindow : Window
    {
        private readonly IConnectionFactory connectionFactory;
        private UnitOfWork uow;

        public RemoveItemWindow(IConnectionFactory connection, UnitOfWork uow)
        {
            connectionFactory = connection;
            this.uow = uow;
            InitializeComponent();
        }

        private void removeItemButton_Click(object sender, RoutedEventArgs e)
        {
            int id = int.Parse(idTextBox.Text);
            int quantity;
            Product product = uow.Products.Get(id);

            // Validate the product
            if (product == null)
            {
                MessageBox.Show("You have entered an ID number that does not exist");
                return;
            }

            // If there is no input in the quantity box, we assume it's 0
            if (quantityTextBox.Text.Length != 0 )
            {
                 quantity = int.Parse(quantityTextBox.Text);
            }
            else
            {
                quantity = 0;
            }
            
            // We allow for negative quantity because of receiving errors and incorrect
            // onhand changes
            int newQuantity = product.Quantity - quantity;
            product.Quantity = newQuantity;
            uow.Products.Update(product);

            if (newQuantity >= 0)
            {
                MessageBox.Show("You have removed the following item(s):\n" +
                    $"ID: \t{id}\n" +
                    $"Quantity: {quantity}\n" +
                    $"{product.Name} now has quantity = {newQuantity}");
            }
            else
            {
                MessageBox.Show($"You're inventory for {product.Name}, ID = {product.Id}\n" +
                    $"is {newQuantity}, which is negative. Please retrain your team to maintain\n" +
                    $"correct inventory levels.");
            }

            if (removeItemCheckBox.IsChecked == true)
            {
                if (product.Quantity == 0)
                {
                    string name = product.Name;
                    int deletedId = product.Id;
                    uow.Products.Delete(product.Id);
                    MessageBox.Show($"ID {deletedId}, {name} has completely been deleted.");
                }
                else
                {
                    MessageBox.Show("The inventory must be 0 to completely remove an item from the database\n" +
                        $"{product.Name}'s quanitity is {product.Quantity}");
                    return;
                }
                resetTextBoxes();
            }
        }

        private void homeButton_Click(object sender, RoutedEventArgs e)
        {
            HomeWindow window = new HomeWindow(connectionFactory, uow);
            window.Show();
            this.Close();
        }

        private void viewAllItemsButton_Click(object sender, RoutedEventArgs e)
        {
            AllItemsWindow window = new AllItemsWindow(connectionFactory, uow);
            window.Show();
            this.Close();
        }

        private void createItemButton_Click(object sender, RoutedEventArgs e)
        {
            CreateNewItemsWindow window = new CreateNewItemsWindow(connectionFactory, uow);
            window.Show();
            this.Close();
        }

        private void editItemButton_Click(object sender, RoutedEventArgs e)
        {
            EditItemWindow window = new EditItemWindow(connectionFactory, uow);
            window.Show();
            this.Close();
        }

        private void logoutButton_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow page = new LoginWindow(connectionFactory, uow);
            page.Show();
            this.Close();
        }

        private void resetTextBoxes()
        {
            idTextBox.Text = string.Empty;
            quantityTextBox.Text = string.Empty;
            removeItemCheckBox.IsChecked = false;
        }
    }
}
