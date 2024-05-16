using CKK.Logic.Interfaces;
using CKK.Logic.Models;
using CKK.Persistance.Models;
using System.Windows;


namespace CKK.UI
{
    /// <summary>
    /// Interaction logic for HomePage.xaml
    /// </summary>
    public partial class HomeWindow : Window
    {
        FileStore store;
        public HomeWindow(IStore store)
        {
            this.store = (FileStore) store;
            InitializeComponent();
        }

        private void viewAllItemsButton_Click(object sender, RoutedEventArgs e)
        {
            AllItemsWindow window = new AllItemsWindow(store);
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
    }
}
