using CKK.Logic.Interfaces;
using CKK.Logic.Models;
using CKK.Persistance.Models;
using System.Windows;
using System.Windows.Controls;
using System.Xml.Linq;

namespace CKK.UI
{
    /// <summary>
    /// Interaction logic for AllItems.xaml
    /// </summary>
    public partial class AllItemsWindow : Window
    {
        FileStore store;
        public AllItemsWindow(FileStore store)
        {
            this.store = store;
            InitializeComponent();

            //*********************************************************
            // Put Store Item list Items in the List View             *
            //itemListView.ItemsSource = store.GetStoreItems();       *
            //*********************************************************
        }

        private void homeButton_Click(object sender, RoutedEventArgs e)
        {
            HomeWindow window = new HomeWindow(store);
            window.Show();
            this.Close();
        }

        private void createNewItemsButton_Click(object sender, RoutedEventArgs e)
        {
            CreateNewItemsWindow page = new CreateNewItemsWindow(store);
            page.Show();
            this.Close();
        }

        private void editItemsButton_Click(object sender, RoutedEventArgs e)
        {
            EditItemWindow window = new EditItemWindow(store);
            window.Show();
            this.Close();
        }

        private void removeItemsButton_Click(object sender, RoutedEventArgs e)
        {
            RemoveItemWindow window = new RemoveItemWindow(store);
            window.Show();
            this.Close();
        }

        private void logoutButton_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow page = new LoginWindow();
            page.Show();
            this.Close();
        }

        private void sortComboBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            /*
            var temp = (ComboBox)sender;
            
            string comboBoxSelection = temp.SelectedValue.ToString();
            if (comboBoxSelection == "ID Number")
            {
                // sort by ID
                itemListView.ItemsSource = store.Products.OrderBy(x => x.Product.Id);
            }
            else if (comboBoxSelection == "Quantity")
            {
                // sort by quantity
                itemListView.ItemsSource = store.GetProductsByQuantity();

            }
            else
            {
                // sort by price
                itemListView.ItemsSource = store.GetProductsByPrice();
            }
            */
            throw new NotImplementedException();
        }

        private void searchSubmitButton_Click(object sender, RoutedEventArgs e)
        {
            // Removed for testing
            // itemListView.ItemsSource = store.GetAllProductsByName(searchTextBox.Text);
            throw new NotImplementedException();
        }

        private void clearButton_Click(object sender, RoutedEventArgs e)
        {
            // Removed for testing
            // itemListView.ItemsSource = store.GetStoreItems();
            throw new NotImplementedException();
        }
    }
}
