using System.Windows;

namespace VideoLibraryApp
{
    public partial class AddMovieDialog : Window
    {
        public MovieModel Movie { get; private set; }

        public AddMovieDialog()
        {
            InitializeComponent();
            Movie = new MovieModel();
            DataContext = this;
        }

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            Movie = new MovieModel
            {
                Title = TitleTextBox.Text,
                Studio = StudioTextBox.Text,
                Genre = GenreTextBox.Text,
                Year = int.Parse(YearTextBox.Text),
                Director = DirectorTextBox.Text,
                MainActors = ActorsTextBox.Text,
                Rating = double.Parse(RatingTextBox.Text),
                Summary = SummaryTextBox.Text
            };

            DialogResult = true;
            Close();
        }



        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
