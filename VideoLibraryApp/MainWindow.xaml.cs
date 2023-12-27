using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using System.Windows.Input;
namespace VideoLibraryApp
{
    public partial class MainWindow : Window
    {
        private MainWindowViewModel viewModel;

        public MainWindow()
        {
            viewModel = new MainWindowViewModel(this);
            InitializeComponent();
            DataContext = viewModel; 
        }
    }

}