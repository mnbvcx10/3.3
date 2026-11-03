using Avalonia.Controls;
using Avalonia.Interactivity;

namespace katalog;

public partial class DetailsView : UserControl
{
    private readonly MainWindow? _mainWindow;

    public DetailsView()
    {
        InitializeComponent();
    }

    public DetailsView(MainWindow mainWindow, string title, string year, string rating) : this()
    {
        _mainWindow = mainWindow;
        DisplayDetails(title, year, rating);
    }

    private void DisplayDetails(string title, string year, string rating)
    {
        TitleText.Text = title;
        YearText.Text = year;
        RatingText.Text = $"{rating} / 10";
    }

    private void OnBackClick(object? sender, RoutedEventArgs e)
    {
        _mainWindow?.ShowListView();
    }
}
