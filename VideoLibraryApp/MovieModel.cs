using System.ComponentModel;

namespace VideoLibraryApp
{
    public class MovieModel : INotifyPropertyChanged
    {
        private string title;
        private string studio;
        private string genre;
        private int year;
        private string director;
        private string mainActors;
        private double rating;
        private string summary;
        private bool inLibrary;

        public string Title
        {
            get { return title; }
            set
            {
                if (title != value)
                {
                    title = value;
                    OnPropertyChanged(nameof(Title));
                }
            }
        }

        public string Studio
        {
            get { return studio; }
            set
            {
                if (studio != value)
                {
                    studio = value;
                    OnPropertyChanged(nameof(Studio));
                }
            }
        }

        public string Genre
        {
            get { return genre; }
            set
            {
                if (genre != value)
                {
                    genre = value;
                    OnPropertyChanged(nameof(Genre));
                }
            }
        }

        public int Year
        {
            get { return year; }
            set
            {
                if (year != value)
                {
                    year = value;
                    OnPropertyChanged(nameof(Year));
                }
            }
        }

        public string Director
        {
            get { return director; }
            set
            {
                if (director != value)
                {
                    director = value;
                    OnPropertyChanged(nameof(Director));
                }
            }
        }

        public string MainActors
        {
            get { return mainActors; }
            set
            {
                if (mainActors != value)
                {
                    mainActors = value;
                    OnPropertyChanged(nameof(MainActors));
                }
            }
        }

        public double Rating
        {
            get { return rating; }
            set
            {
                if (rating != value)
                {
                    rating = value;
                    OnPropertyChanged(nameof(Rating));
                }
            }
        }

        public string Summary
        {
            get { return summary; }
            set
            {
                if (summary != value)
                {
                    summary = value;
                    OnPropertyChanged(nameof(Summary));
                }
            }
        }

        public bool InLibrary
        {
            get { return inLibrary; }
            set
            {
                if (inLibrary != value)
                {
                    inLibrary = value;
                    OnPropertyChanged(nameof(InLibrary));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
