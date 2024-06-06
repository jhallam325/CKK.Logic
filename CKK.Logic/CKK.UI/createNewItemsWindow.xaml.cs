using CKK.DB.Interfaces;
using CKK.DB.UOW;
using CKK.Logic.Models;
using System.IO;
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
            Product newProduct = new Product();
            newProduct.Name = nameTextBox.Text;
            newProduct.Quantity = int.Parse(quantityTextBox.Text);
            newProduct.Price = decimal.Parse(priceTextBox.Text);

            // I have the location of an image, and I need to convert it into a byte[] to assign as the picture variable
            string path = pictureTextBox.Text;
            string fileName = System.IO.Path.GetFileName(path);  //Returns MyImage.jpeg

            newProduct.Picture = File.ReadAllBytes(path);

            uow.Products.Add(newProduct);

            MessageBox.Show($"New item created!\n" +
                $"Name: \t {newProduct.Name}\n" +
                //$"ID: \t {uow.Products.GetId(newProduct)}\n" +
                $"Quantity: {newProduct.Quantity}\n" +
                $"Price: \t {newProduct.Price:C}\n" +
                $"Picture: \t {fileName}");
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // Create OpenFileDialog 
            Microsoft.Win32.OpenFileDialog dialog = new Microsoft.Win32.OpenFileDialog();

            // Set filter for file extension and default file extension 
            dialog.DefaultExt = ".jpg";
            dialog.Filter = "JPG Files (*.jpg)|*.jpg|JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png|GIF Files (*.gif)|*.gif|WEBP Files (*.webp)|*.webp";

            // Display OpenFileDialog by calling ShowDialog method 
            Nullable<bool> result = dialog.ShowDialog();

            // Get the selected file name and display in a TextBox 
            if (result == true)
            {
                // Open document 
                string filename = dialog.FileName;
                pictureTextBox.Text = filename;
            }
        }
    }
}
