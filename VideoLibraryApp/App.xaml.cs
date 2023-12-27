using System.Windows;

namespace VideoLibraryApp
{
    public partial class App : Application
    {
        private void OnStartup(object sender, StartupEventArgs e)
        {
            // Створюємо і відображаємо вікно авторизації
            LoginWindow loginWindow = new LoginWindow();
            loginWindow.ShowDialog();

            // Перевіряємо, чи авторизація пройшла успішно
            if (loginWindow.DialogResult == true)
            {
                // Якщо так, створюємо і відображаємо головне вікно
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
            }
            else
            {
                // Якщо користувач вибрав вийти або щось пішло не так
                Shutdown();
            }
        }
    }
}
