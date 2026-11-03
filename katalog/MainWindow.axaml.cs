using System;
using System.Collections.Generic;
using System.IO;
using Avalonia.Controls;
using Avalonia.Interactivity;

namespace katalog;

public partial class MainWindow : Window
{
    private readonly List<string> _rawLines = new();
    private object? _listContent;

    public MainWindow()
    {
        InitializeComponent();
        _listContent = Content;
        LoadItems();
    }

    public void ShowListView()
    {
        Content = _listContent;
        LoadItems();
    }

    public void ShowDetailsView(string title, string year, string rating)
    {
        Content = new DetailsView(this, title, year, rating);
    }

    private void LoadItems()
    {
        _rawLines.Clear();
        var filePath = GetFilePath();
        if (!File.Exists(filePath))
        {
            StatusText.Text = "Plik danych nie istnieje.";
            return;
        }

        var lines = File.ReadAllLines(filePath);
        var displayItems = new List<string>();
        foreach (var line in lines)
        {
            if (string.IsNullOrWhiteSpace(line)) continue;
            _rawLines.Add(line);
            var parts = line.Split(';');
            if (parts.Length >= 3)
            {
                displayItems.Add($"{parts[0]} ({parts[1]}) — Ocena: {parts[2]}/10");
            }
            else
            {
                displayItems.Add(line);
            }
        }

        ItemsListBox.ItemsSource = displayItems;
        StatusText.Text = $"Wczytano {displayItems.Count} pozycji.";
    }

    private string GetFilePath()
    {
        if (File.Exists("dane/pozycje.txt"))
        {
            return "dane/pozycje.txt";
        }
        if (File.Exists("katalog/dane/pozycje.txt"))
        {
            return "katalog/dane/pozycje.txt";
        }
        return "dane/pozycje.txt";
    }

    private void OnDetailsClick(object? sender, RoutedEventArgs e)
    {
        if (ItemsListBox.SelectedIndex < 0 || ItemsListBox.SelectedIndex >= _rawLines.Count)
        {
            StatusText.Text = "Najpierw zaznacz pozycję na liście.";
            return;
        }

        var selected = _rawLines[ItemsListBox.SelectedIndex];
        var parts = selected.Split(';');
        if (parts.Length >= 3)
        {
            ShowDetailsView(parts[0], parts[1], parts[2]);
        }
        else
        {
            ShowDetailsView(selected, "-", "-");
        }
    }

    private void OnAddClick(object? sender, RoutedEventArgs e)
    {
        Content = new AddView(this);
    }

    private void OnRefreshClick(object? sender, RoutedEventArgs e)
    {
        LoadItems();
    }

    private void OnCountClick(object? sender, RoutedEventArgs e)
    {
        var lines = File.ReadAllLines("dane/pozycje.txt");
        CountText.Text = $"Pozycji w pliku: {lines.Length}";
    }
}
