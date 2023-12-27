using System;

namespace VideoLibraryApp
{
    public class MovieRentedEventArgs : EventArgs
    {
        public MovieModel RentedMovie { get; }

        public MovieRentedEventArgs(MovieModel rentedMovie)
        {
            RentedMovie = rentedMovie;
        }
    }

}
