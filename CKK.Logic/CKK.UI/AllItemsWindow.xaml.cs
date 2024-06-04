using CKK.DB.Interfaces;
using System.Windows;

namespace CKK.UI
{
    /// <summary>
    /// Interaction logic for AllItems.xaml
    /// </summary>
    public partial class AllItemsWindow : Window
    {
        private readonly IConnectionFactory connectionFactory;
        public AllItemsWindow(IConnectionFactory connection)
        {
            connectionFactory = connection;
            InitializeComponent();

            //*********************************************************
            // Put Store Item list Items in the List View             *
            //itemListView.ItemsSource = store.GetStoreItems();       *
            //*********************************************************
        }

        private void homeButton_Click(object sender, RoutedEventArgs e)
        {
            HomeWindow window = new HomeWindow(connectionFactory);
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
