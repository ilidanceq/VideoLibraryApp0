using System.Windows;

namespace VideoLibraryApp
{
    public partial class LoginWindow : Window
    {



        public LoginWindow()
        {
            InitializeComponent();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string username = usernameTextBox.Text;
            string password = passwordBox.Password;

            // Перевірка логіна та пароля 
            if (username == "admin" && password == "admin")
            {

                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();


                this.Close();
            }
            else
            {
                MessageBox.Show("Невірний логін чи пароль. Спробуйте ще раз.", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

    }
}



