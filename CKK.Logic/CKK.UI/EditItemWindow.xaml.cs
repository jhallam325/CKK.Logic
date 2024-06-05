using CKK.DB.Interfaces;
using CKK.DB.UOW;
using CKK.Logic.Models;
using System.Buffers.Text;
using System.Drawing;
using System.IO;
using System.Windows;
using System.Windows.Markup;
using System.Windows.Media.Imaging;

namespace CKK.UI
{
    /// <summary>
    /// Interaction logic for EditItem.xaml
    /// </summary>
    public partial class EditItemWindow : Window
    {
        private readonly IConnectionFactory connectionFactory;
        private UnitOfWork uow;
        private bool searchButtonClicked = false;

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

            // Validate the product
            if (product == null)
            {
                MessageBox.Show("You have entered an ID number that does not exist");
                searchButtonClicked = false;
                return;
            }

            // We have found a product
            searchButtonClicked = true;

            // Display the values of the product
            oldNameTextBox.Text = product.Name;
            oldQuantityTextBox.Text = product.Quantity.ToString();
            oldPriceTextBox.Text = product.Price.ToString();

            // How can I display the picture?
            //oldPictureImage.Source = byteArrayToImage(product.Picture);// as System.Windows.Controls.Image;
            oldPictureImage.Source = LoadImage(product.Picture);
        }

        public Image byteArrayToImage(byte[] bytesArr)
        {
            using (MemoryStream memstr = new MemoryStream(bytesArr))
            {
                Image img = Image.FromStream(memstr);
                return img;
            }
        }

        private static BitmapImage LoadImage(byte[] imageData)
        {
            if (imageData == null || imageData.Length == 0) return null;
            var image = new BitmapImage();
            using (var mem = new MemoryStream(imageData))
            {
                mem.Position = 0;
                image.BeginInit();
                image.CreateOptions = BitmapCreateOptions.PreservePixelFormat;
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.UriSource = null;
                image.StreamSource = mem;
                image.EndInit();
            }
            image.Freeze();
            return image;
        }

        private void updateButton_Click(object sender, RoutedEventArgs e)
        {
            // Check to see if a product has been searched for
            if (!searchButtonClicked)
            {
                MessageBox.Show("You must ender a product ID to be updated");
                return;
            }

            // Find the product from the search parameter
            int productID = int.Parse(productIdTextBox.Text);
            Product product = uow.Products.Get(productID);

            // Validate the product
            if (product == null)
            { 
                MessageBox.Show("You have entered an ID number that does not exist");
                return;
            }

            product.Name = newNameTextBox.Text;
            product.Quantity = int.Parse(newQuantityTextBox.Text);
            product.Price = decimal.Parse(newPriceTextBox.Text);
            //product.Picture = 

            uow.Products.Update(product);


        }
    }
}
