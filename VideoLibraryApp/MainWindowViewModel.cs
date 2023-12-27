using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Text.Json;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;

namespace VideoLibraryApp
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private ICommand addMovieCommand;
        private ICommand openRentalWindowCommand;
        private RentalViewModel rentalViewModel;
        private readonly string JsonFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "movies.json");
        private MainWindow mainWindow;
        public ICommand OpenAddMovieDialogCommand { get; private set; }
        public ICommand ShowHelpCommand { get; private set; }
        private MovieModel selectedMovie;

        public MainWindowViewModel(MainWindow mainWindow)
        {
            OpenAddMovieDialogCommand = new RelayCommand(OpenAddMovieDialogCommandExecute);
            this.mainWindow = mainWindow;
            RentedMovies = new ObservableCollection<RentalViewModel>();
            rentalViewModel = new RentalViewModel(Movies);
            rentalViewModel.MovieRented += RentalViewModel_MovieRented;
            Movies = new ObservableCollection<MovieModel>();
            AddMovieCommand = new RelayCommand(OpenAddMovieDialogCommandExecute);
            ShowHelpCommand = new RelayCommand(ShowHelp);
            LoadDataFromJson();
        }

        public ICommand AddMovieCommand
        {
            get { return addMovieCommand; }
            set
            {
                if (addMovieCommand != value)
                {
                    addMovieCommand = value;
                    OnPropertyChanged(nameof(AddMovieCommand));
                }
            }
        }
        private void AddMovieDialogExecute(object parameter)
        {
            AddMovieDialog addMovieDialog = new AddMovieDialog();
            addMovieDialog.Owner = mainWindow;

            if (addMovieDialog.ShowDialog() == true)
            {
                MovieModel newMovie = new MovieModel
                {
                    Title = addMovieDialog.Title,
                };
                Movies.Add(newMovie);
                SaveDataToJson();
                OnPropertyChanged(nameof(Movies));
            }
        }

        private ObservableCollection<MovieModel> filteredMovies;

        public ObservableCollection<MovieModel> FilteredMovies
        {
            get
            {
                if (filteredMovies == null)
                {
                    filteredMovies = new ObservableCollection<MovieModel>();
                }
                return filteredMovies;
            }
            set
            {
                if (filteredMovies != value)
                {
                    filteredMovies = value;
                    OnPropertyChanged(nameof(FilteredMovies));
                }
            }
        }


        private ObservableCollection<MovieModel> movies;

        private MovieModel newMovie;

        public ObservableCollection<MovieModel> Movies
        {
            get { return movies; }
            set
            {
                if (movies != value)
                {
                    movies = value;
                    OnPropertyChanged(nameof(Movies));
                    OnPropertyChanged(nameof(FilteredMovies));
                }
            }
        }


        public MovieModel SelectedMovie
        {
            get { return selectedMovie; }
            set
            {
                if (selectedMovie != value)
                {
                    selectedMovie = value;
                    if (movies != null)
                    {
                        CollectionViewSource.GetDefaultView(Movies).Refresh();
                    }
                }
            }
        }


        private void RentalViewModel_MovieRented(object sender, MovieRentedEventArgs e)
        {
            Movies.Remove(e.RentedMovie);
        }

        public ObservableCollection<RentalViewModel> RentedMovies { get; set; }

        public ICommand OpenRentalWindowCommand
        {
            get
            {
                if (openRentalWindowCommand == null)
                {
                    openRentalWindowCommand = new RelayCommand(OpenRentalWindow);
                }
                return openRentalWindowCommand;
            }
        }

        // test

        private void ShowHelp(object parameter)
        {
            HelpWindow helpWindow = new HelpWindow();
            helpWindow.Show();
        }


        private void OpenAddMovieDialogCommandExecute(object parameter)
        {
            OpenAddMovieDialog(parameter);
        }

        private void OpenAddMovieDialog(object parameter)
        {
            AddMovieDialog addMovieDialog = new AddMovieDialog();
            addMovieDialog.Owner = mainWindow;

            if (addMovieDialog.ShowDialog() == true)
            {
                MovieModel newMovie = addMovieDialog.Movie;
                Movies.Add(newMovie);
                SaveDataToJson();
                OnPropertyChanged(nameof(Movies));
            }
        }




        private void RentMovie(RentalViewModel rentalViewModel)
        {
            RentedMovies.Add(rentalViewModel);
            MessageBox.Show($"The movie '{rentalViewModel.FilmTitle}' is rented to the customer '{rentalViewModel.CustomerName}'");
        }

        private async void SaveDataToJson()
        {
            try
            {
                string jsonData = JsonSerializer.Serialize(Movies);

                using (StreamWriter writer = new StreamWriter(JsonFilePath, false))
                {
                    await writer.WriteAsync(jsonData);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving data to JSON: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void LoadDataFromJson()
        {
            try
            {
                if (File.Exists(JsonFilePath))
                {
                    string jsonData = File.ReadAllText(JsonFilePath);

                    if (string.IsNullOrWhiteSpace(jsonData))
                    {
                        MessageBox.Show("JSON file is empty or contains invalid data.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }

                    Movies.Clear();
                    var deserializedMovies = JsonSerializer.Deserialize<ObservableCollection<MovieModel>>(jsonData);
                    foreach (var movie in deserializedMovies)
                    {
                        Movies.Add(movie);
                    }
                    OnPropertyChanged(nameof(Movies));
                }
            }
            catch (IOException ex)
            {
                MessageBox.Show($"Error loading data from JSON: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (JsonException ex)
            {
                MessageBox.Show($"Error parsing JSON: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }







        private void OpenRentalWindow(object parameter)
        {
            rentalViewModel.AvailableMovies = new ObservableCollection<MovieModel>(Movies);
            var rentalWindow = new RentalWindow(rentalViewModel, mainWindow);
            rentalWindow.Show();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
