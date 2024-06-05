using CKK.DB.Interfaces;
using CKK.DB.UOW;
using CKK.Logic.Models;
using System.Buffers.Text;
using System.Drawing;
using System.IO;
using System.Windows;
using System.Windows.Markup;

namespace CKK.UI
{
    /// <summary>
    /// Interaction logic for EditItem.xaml
    /// </summary>
    public partial class EditItemWindow : Window
    {
        private readonly IConnectionFactory connectionFactory;
        private UnitOfWork uow;

        public EditItemWindow(IConnectionFactory connection, UnitOfWork uow)
        {
            connectionFactory = connection;
            this.uow = uow;
            InitializeComponent();
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

        private void createNewItemButton_Click(object sender, RoutedEventArgs e)
        {
            CreateNewItemsWindow page = new CreateNewItemsWindow(connectionFactory, uow);
            page.Show();
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

        private void searchSubmitButton_Click(object sender, RoutedEventArgs e)
        {
            // Find the product from the search parameter
            int productID = int.Parse(productIdTextBox.Text);
            Product product = uow.Products.Get(productID);

            // Display the values of the product
            oldNameTextBox.Text = product.Name;
            oldQuantityTextBox.Text = product.Quantity.ToString();
            oldPriceTextBox.Text = product.Price.ToString();

            // How can I display the picture?
            oldPictureImage = byteArrayToImage(product.Picture) as System.Windows.Controls.Image;
        }

        public Image byteArrayToImage(byte[] bytesArr)
        {
            using (MemoryStream memstr = new MemoryStream(bytesArr))
            {
                Image img = Image.FromStream(memstr);
                return img;
            }
        }
    }
}
