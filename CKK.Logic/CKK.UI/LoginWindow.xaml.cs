using CKK.Logic.Interfaces;
using CKK.Logic.Models;
using CKK.Persistance.Models;
using System.IO;
using System.Windows;

namespace CKK.UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        //Store coreysKnickKnacks = new Store();
        private IStore coreysKnickKnacks;
        public LoginWindow()
        {
            coreysKnickKnacks = new FileStore();
            InitializeComponent();
        }

        private void loginButton_Click(object sender, RoutedEventArgs e)
        {
            // Validate the user name and password here. In this case, I'm just checking to make sure
            // they aren't blank.
            if (userNameTextBox.Text != "" && passwordTextBox.Text != "")
            {
                HomeWindow page = new HomeWindow(coreysKnickKnacks);
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