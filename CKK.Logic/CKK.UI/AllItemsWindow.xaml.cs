using CKK.DB.Interfaces;
using CKK.DB.UOW;
using System.IO.Packaging;
using System.Windows;
using System.Collections.Generic;
using System.Collections;
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
            var descCheckBox = GetCheckBox();
            //var itemListView = GetListView();

            string comboBoxSelection = temp.SelectedValue?.ToString();
            if ((bool) descCheckBox.IsChecked)
            {
                itemListView.ItemsSource = uow.Products.SortByDesc(comboBoxSelection);
            }
            else
            {
                itemListView.ItemsSource = uow.Products.SortByAsc(comboBoxSelection);
            }

        }

        private void descCheckBox_Checked(object sender, RoutedEventArgs e) 
        {
            //var comboBox = GetComboBox();
            string comboBoxSelection = sortComboBox.SelectedValue.ToString();
            itemListView.ItemsSource = uow.Products.SortByDesc(comboBoxSelection);
        }
        private void descCheckBox_Unchecked(object sender, RoutedEventArgs e) 
        {
            string comboBoxSelection = sortComboBox.SelectedValue.ToString();
            itemListView.ItemsSource = uow.Products.SortByAsc(comboBoxSelection);
        }

        private void searchSubmitButton_Click(object sender, RoutedEventArgs e)
        {
            itemListView.ItemsSource = uow.Products.SearchFor(searchTextBox.Text);
        }

        private void clearButton_Click(object sender, RoutedEventArgs e)
        {
            itemListView.ItemsSource = uow.Products.GetAll();
            searchTextBox.Text = string.Empty;
            descCheckBox.IsChecked = false;
            sortComboBox.SelectedIndex = 0;
        }

        private CheckBox GetCheckBox()
        {
            var panelContent = (Panel) Content;
            UIElementCollection panelContentChildren = panelContent.Children;
            List<FrameworkElement> fremeworkElementList = panelContentChildren.Cast<FrameworkElement>().ToList();
            var listControl = fremeworkElementList.OfType<Control>();
            return (CheckBox) listControl.FirstOrDefault(s => s.Name == "descCheckBox") ?? new CheckBox();
        }

        private ComboBox GetComboBox()
        {
            var panelContent = (Panel) Content;
            UIElementCollection panelContentChildren = panelContent.Children;
            List<FrameworkElement> fremeworkElementList = panelContentChildren.Cast<FrameworkElement>().ToList();
            var listControl = fremeworkElementList.OfType<Control>();
            return (ComboBox) listControl.FirstOrDefault(s => s.Name == "sortComboBox") ?? new ComboBox();
        }

        private ListView GetListView()
        {
            var panelContent = (Panel)this.Content;
            UIElementCollection panelContentChildren = panelContent.Children;
            List<FrameworkElement> fremeworkElementList = panelContentChildren.Cast<FrameworkElement>().ToList();
            var listControl = fremeworkElementList.OfType<Control>();
            return (ListView) listControl.FirstOrDefault(s => s.Name == "itemListView");
        }

        private T GetControlObject<T>(string name)
        {
            var panelContent = (Panel)Content;
            UIElementCollection panelContentChildren = panelContent.Children;
            List<FrameworkElement> frameworkElements = panelContentChildren.Cast<FrameworkElement>().ToList();
            var frameworkElementsOfControl = frameworkElements.OfType<Control>();
            var typeWanted = frameworkElementsOfControl.GetType();
            //if (typeWanted is CheckBox)
            //{
            //    return (T)Convert.ChangeType(frameworkElementsOfControl.FirstOrDefault(x => x.Name == name), typeof(T)) 
            //        ?? new(T) Convert.ChangeType(CheckBox(), typeof(T));
            //}
            return (T)Convert.ChangeType(frameworkElementsOfControl.FirstOrDefault(x => x.Name == name), typeof(T));
        }
    }
}
