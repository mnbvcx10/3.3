using System;
using System.IO;
using System.Text;
using Avalonia.Controls;
using Avalonia.Interactivity;

namespace katalog;

public partial class AddView : UserControl
{
    private readonly MainWindow? _mainWindow;

    public AddView()
    {
        InitializeComponent();
    }

    public AddView(MainWindow mainWindow) : this()
    {
        _mainWindow = mainWindow;
    }

    private void OnCancelClick(object? sender, RoutedEventArgs e)
    {
        _mainWindow?.ShowListView();
    }

    private void OnSaveClick(object? sender, RoutedEventArgs e)
    {
        var title = TitleInput.Text?.Trim() ?? string.Empty;
        var year = YearInput.Text?.Trim() ?? string.Empty;
        var rating = RatingInput.Text?.Trim() ?? string.Empty;

        if (!ValidateInputs(title, year, rating, out var error))
        {
            ErrorText.Text = error;
            return;
        }

        SaveItem($"{title};{year};{rating}");
        _mainWindow?.ShowListView();
    }

    private bool ValidateInputs(string title, string year, string rating, out string error)
    {
        if (string.IsNullOrEmpty(title))
        {
            error = "Tytuł nie może być pusty.";
            return false;
        }
        if (!int.TryParse(year, out var y) || y < 1800 || y > 2100)
        {
            error = "Podaj poprawny rok (1800-2100).";
            return false;
        }
        if (!int.TryParse(rating, out var r) || r < 1 || r > 10)
        {
            error = "Podaj poprawną ocenę w skali 1-10.";
            return false;
        }
        error = string.Empty;
        return true;
    }

    private void SaveItem(string record)
    {
        var targetPath = GetTargetPath();
        File.AppendAllText(targetPath, record + Environment.NewLine, Encoding.UTF8);
        if (targetPath == "dane/pozycje.txt" && File.Exists("katalog/dane/pozycje.txt"))
        {
            File.AppendAllText("katalog/dane/pozycje.txt", record + Environment.NewLine, Encoding.UTF8);
        }
    }

    private string GetTargetPath()
    {
        if (File.Exists("katalog/dane/pozycje.txt") && !File.Exists("dane/pozycje.txt"))
        {
            return "katalog/dane/pozycje.txt";
        }
        return "dane/pozycje.txt";
    }
}

