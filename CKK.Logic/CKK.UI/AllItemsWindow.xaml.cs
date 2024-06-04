using CKK.DB.Interfaces;
using CKK.DB.UOW;
using System.IO.Packaging;
using System.Windows;
using System.Windows.Controls;

namespace CKK.UI
{
    /// <summary>
    /// Interaction logic for AllItems.xaml
    /// </summary>
    public partial class AllItemsWindow : Window
    {
        private readonly IConnectionFactory connectionFactory;
        private UnitOfWork uow;
        private string oldSortValue;
        private bool ascending = true;

        public AllItemsWindow(IConnectionFactory connection, UnitOfWork uow)
        {
            connectionFactory = new DatabaseConnectionFactory();
            this.uow = uow;
            InitializeComponent();

            // Bind the list of all products to the itemListView
            itemListView.ItemsSource = uow.Products.GetAll();
        }

        private void homeButton_Click(object sender, RoutedEventArgs e)
        {
            HomeWindow window = new HomeWindow(connectionFactory, uow);
            window.Show();
            this.Close();
        }

        private void createNewItemsButton_Click(object sender, RoutedEventArgs e)
        {
            CreateNewItemsWindow page = new CreateNewItemsWindow(connectionFactory, uow);
            page.Show();
            this.Close();
        }

        private void editItemsButton_Click(object sender, RoutedEventArgs e)
        {
            EditItemWindow window = new EditItemWindow(connectionFactory, uow);
            window.Show();
            this.Close();
        }

        private void removeItemsButton_Click(object sender, RoutedEventArgs e)
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

        private void sortComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var temp = (ComboBox) sender;

            string comboBoxSelection = temp.SelectedValue.ToString();
            itemListView.ItemsSource = uow.Products.SortByAsc(comboBoxSelection);
        }

        private void searchSubmitButton_Click(object sender, RoutedEventArgs e)
        {
            itemListView.ItemsSource = uow.Products.SearchFor(searchTextBox.Text);
        }

        private void clearButton_Click(object sender, RoutedEventArgs e)
        {
            // Removed for testing
            // itemListView.ItemsSource = store.GetStoreItems();
            throw new NotImplementedException();
        }
    }
}
