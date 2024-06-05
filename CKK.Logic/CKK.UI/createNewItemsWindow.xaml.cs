using CKK.DB.Interfaces;
using CKK.DB.UOW;
using CKK.Logic.Models;
using System.Windows;

namespace CKK.UI
{
    /// <summary>
    /// Interaction logic for createNewItemsPage.xaml
    /// </summary>
    public partial class CreateNewItemsWindow : Window
    {
        private readonly IConnectionFactory connectionFactory;
        private UnitOfWork uow;

        public CreateNewItemsWindow(IConnectionFactory connection, UnitOfWork uow)
        {
            connectionFactory = connection;
            this.uow = uow;
            InitializeComponent();
            
        }

        private void createItemButton_Click(object sender, RoutedEventArgs e)
        {
            //int quantity = int.Parse(quantityTextBox.Text);

            //ShoppingCartItem newProduct = new ShoppingCartItem();
            //newProduct.Price = decimal.Parse(priceTextBox.Text);
            //newProduct.Name = nameTextBox.Text;

            //store.AddStoreItem(newProduct, quantity);

            Product newProduct = new Product();
            newProduct.Name = nameTextBox.Text;
            newProduct.Quantity = int.Parse(quantityTextBox.Text);
            newProduct.Price = decimal.Parse(priceTextBox.Text);
            newProduct.Picture = null;// I need to convert the image located at the string address into a byte[] to add to the database

            //uow.Products.Add(newProduct);

            Product oldProduct = uow.Products.Get(2);

            MessageBox.Show($"New item created!\n" +
                $"Name: \t {newProduct.Name}\n" +
                $"ID: \t {uow.Products.GetId(oldProduct)}\n" +
                $"Quantity: {newProduct.Quantity}\n" +
                $"Price: \t {newProduct.Price:C}\n" +
                $"Picture: \t {pictureTextBox.Text}");
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

        private void editItemButton_Click(object sender, RoutedEventArgs e)
        {
            EditItemWindow window = new EditItemWindow(connectionFactory, uow);
            window.Show();
            this.Close();
        }

        private void removeItemButton_Click(object sender, RoutedEventArgs e)
        {
            RemoveItemWindow window = new RemoveItemWindow(connectionFactory, uow);
            window.Show();
            this.Close();
        }

        private void logoutButton_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow page = new LoginWindow(connectionFactory, uow);
            page.Show();
            this.Close();
        }
    }
}
