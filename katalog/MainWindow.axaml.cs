using System;
using System.Collections.Generic;
using System.IO;
using Avalonia.Controls;
using Avalonia.Interactivity;

namespace katalog;

public partial class MainWindow : Window
{
    private readonly List<string> _rawLines = new();

    public MainWindow()
    {
        InitializeComponent();
        LoadItems();
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

        StatusText.Text = $"Wybrano: {_rawLines[ItemsListBox.SelectedIndex]}";
    }

    private void OnAddClick(object? sender, RoutedEventArgs e)
    {
        StatusText.Text = "Formularz dodawania zostanie otwarty.";
    }

    private void OnRefreshClick(object? sender, RoutedEventArgs e)
    {
        LoadItems();
    }
}
