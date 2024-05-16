using CKK.Logic.Interfaces;
using CKK.Logic.Models;
using CKK.Persistance.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace CKK.UI
{
    /// <summary>
    /// Interaction logic for RemoveItem.xaml
    /// </summary>
    public partial class RemoveItemWindow : Window
    {
        FileStore store;
        public RemoveItemWindow(FileStore store)
        {
            this.store = store;
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
            HomeWindow window = new HomeWindow(store);
            window.Show();
            this.Close();
        }

        private void viewAllItemsButton_Click(object sender, RoutedEventArgs e)
        {
            AllItemsWindow window = new AllItemsWindow(store);
            window.Show();
            this.Close();
        }

        private void createItemButton_Click(object sender, RoutedEventArgs e)
        {
            CreateNewItemsWindow window = new CreateNewItemsWindow(store);
            window.Show();
            this.Close();
        }

        private void editItemButton_Click(object sender, RoutedEventArgs e)
        {
            EditItemWindow window = new EditItemWindow(store);
            window.Show();
            this.Close();
        }

        private void logoutButton_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow page = new LoginWindow();
            page.Show();
            this.Close();
        }
    }
}
