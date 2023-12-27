using System;
using System.IO;
using System.Text.Json;
using System.Windows;

namespace VideoLibraryApp
{
    /// <summary>
    /// Interaction logic for RentalWindow.xaml
    /// </summary>
    public partial class RentalWindow : Window
    {
        public RentalWindow(RentalViewModel rentalViewModel, MainWindow mainWindow)
        {
            InitializeComponent();
            DataContext = rentalViewModel;         
        }
    }

}
