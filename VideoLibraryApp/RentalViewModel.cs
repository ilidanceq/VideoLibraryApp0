using Prism.Commands;
using System;
using System.Collections.ObjectModel;
using VideoLibraryApp;

public class RentalViewModel : ObservableObject
{
    private ObservableCollection<MovieModel> availableMovies;
    private MovieModel selectedMovie;
    private string filmTitle;
    private string customerName;
    public event EventHandler<MovieRentedEventArgs> MovieRented;
    public string FilmTitle
    {
        get { return filmTitle; }
        set { SetProperty(ref filmTitle, value, nameof(FilmTitle)); }
    }

    public DelegateCommand<object> RentCommand { get; }

    public RentalViewModel(ObservableCollection<MovieModel> movies)
    {
        availableMovies = movies;
        RentCommand = new DelegateCommand<object>(RentMovie, CanRentMovie)
            .ObservesProperty(() => SelectedMovie)
            .ObservesProperty(() => CustomerName);
    }

    public ObservableCollection<MovieModel> AvailableMovies
    {
        get { return availableMovies; }
        set { SetProperty(ref availableMovies, value, nameof(AvailableMovies)); }
    }

    public MovieModel SelectedMovie
    {
        get { return selectedMovie; }
        set { SetProperty(ref selectedMovie, value, nameof(SelectedMovie)); }
    }

    public string CustomerName
    {
        get { return customerName; }
        set { SetProperty(ref customerName, value, nameof(CustomerName)); }
    }

    private void RentMovie(object parameter)
    {
        if (SelectedMovie != null && !string.IsNullOrEmpty(CustomerName))
        {
            AvailableMovies.Remove(SelectedMovie);


            OnMovieRented(SelectedMovie);
        }
    }


    protected virtual void OnMovieRented(MovieModel rentedMovie)
    {
        MovieRented?.Invoke(this, new MovieRentedEventArgs(rentedMovie));
    }

    private bool CanRentMovie(object parameter)
    {
        return SelectedMovie != null && !string.IsNullOrEmpty(CustomerName);
    }
}
