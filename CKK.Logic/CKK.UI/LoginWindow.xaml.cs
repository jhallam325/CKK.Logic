using CKK.DB.Interfaces;
using System.Windows;

namespace CKK.UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        private readonly IConnectionFactory connectionFactory;
        public LoginWindow(IConnectionFactory connection)
        {
            connectionFactory = connection;
            InitializeComponent();
        }

        private void loginButton_Click(object sender, RoutedEventArgs e)
        {
            // Validate the user name and password here. In this case, I'm just checking to make sure
            // they aren't blank.
            if (userNameTextBox.Text != "" && passwordTextBox.Text != "")
            {
                HomeWindow page = new HomeWindow(connectionFactory);
                page.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Please fill out the user name and password fields.");
            }
            
        }
    }
}