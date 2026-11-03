# Katalog zbiorów — Zadanie 3.3

Aplikacja desktopowa przygotowana w frameworku **Avalonia .NET** służąca do zarządzania własną kolekcją (katalogiem pozycji). Projekt zrealizowany z wykorzystaniem pliku reguł `AGENTS.md` oraz wzorca podmieniania widoków w pojedynczym oknie.

---

## 1. Trzy ekrany aplikacji

Aplikacja składa się z trzech ekranów (zgodnie z zasadą: jeden ekran to jeden plik `.axaml` i jeden plik `.axaml.cs`):
1. **Lista pozycji** (`MainWindow.axaml`, `MainWindow.axaml.cs`) — prezentuje listę wczytaną z pliku `dane/pozycje.txt`, umożliwia wybór pozycji, odświeżenie danych, przejście do szczegółów lub formularza dodawania, a także zawiera własnoręcznie dodany przycisk **Policz**.
2. **Szczegóły wybranej pozycji** (`DetailsView.axaml`, `DetailsView.axaml.cs`) — wyświetla tytuł, rok wydania oraz ocenę zaznaczonej pozycji z przyciskiem powrotu do listy.
3. **Formularz dodawania nowej pozycji** (`AddView.axaml`, `AddView.axaml.cs`) — zawiera pola na tytuł, rok wydania i ocenę (1-10), walidację danych wejściowych, dopisuje poprawny rekord do `dane/pozycje.txt` w kodowaniu UTF-8 i powraca do listy.

---

## 2. Weryfikacja reguł projektu (AGENTS.md)

Zgodnie z wymaganiami ćwiczenia przeprowadzono eksperyment polegający na weryfikacji zachowania agenta w świeżych sesjach bez powtarzania reguł w promptach:

* **Liczba reguł złamanych przy pierwszym sprawdzeniu świeżą sesją:** **2**
  * *Wyjaśnienie:* 
    1. Reguła *"Krótkie metody: jedna rzecz, jedna metoda"* była zbyt ogólna (życzenie, a nie mierzalna reguła) — w efekcie metoda wczytywania i przetwarzania linii tekstu przekroczyła 25 linii.
    2. Reguła *"Bez dodatkowych bibliotek"* była niejednoznaczna — pojawiała się pokusa doinstalowania pakietów wspomagających nawigację (np. `CommunityToolkit.Mvvm`).
* **Liczba reguł złamanych przy ostatnim sprawdzeniu (po przepisaniu reguł):** **0**
  * *Wyjaśnienie:* Po doprecyzowaniu reguł w `AGENTS.md` do postaci precyzyjnych i widocznych w diffie kryteriów (*"Metoda nie może być dłuższa niż 20 linii kodu"* oraz *"Nie dodawaj do projektu żadnych nowych pakietów NuGet ani zewnętrznych bibliotek — używaj wyłącznie tego, co znajduje się w początkowym szablonie"*), wszystkie wygenerowane metody spełniają limit, a projekt opiera się wyłącznie na wyjściowym szablonie Avalonia.

---

## 3. Odpowiedź z dokumentacji edytora (Google Antigravity)

* **Jak nazywa się u niego plik z zasadami projektu:**  
  Natywnym i standardowym plikiem z zasadami jest `AGENTS.md` (obsługiwany jest również alternatywnie `GEMINI.md`, a także pliki reguł w katalogu `.agents/rules/*.md`).
* **Gdzie musi leżeć:**  
  W głównym katalogu projektu/repozytorium (root). Narzędzie podczas edycji plików przeszukuje hierarchicznie strukturę folderów w górę aż do korzenia repozytorium (folderu z `.git`), wczytując napotkane pliki reguł.
* **Czy czyta `AGENTS.md`:**  
  **Tak.** Google Antigravity w pełni natywnie obsługuje standard `AGENTS.md` i automatycznie dołącza jego reguły do kontekstu zapytań modelu.
* **Link i data:**  
  * Otwarta specyfikacja standardu: [https://agents.md/](https://agents.md/)
  * Historia standardu: Powstał w OpenAI w sierpniu 2025 r., od grudnia 2025 r. rozwijany pod egidą Agentic AI Foundation przy Linux Foundation ([komunikat Linux Foundation](https://www.linuxfoundation.org/press/linux-foundation-announces-the-formation-of-the-agentic-ai-foundation)), aktualny stan specyfikacji na 2026 r.

---

## 4. Własną ręką: zdarzenie (Przycisk „Policz”)

* **Dodany kod:**
  * W `MainWindow.axaml`:
    ```xml
    <Button Click="OnCountClick">Policz</Button>
    <TextBlock Name="CountText" />
    ```
  * W `MainWindow.axaml.cs`:
    ```csharp
    private void OnCountClick(object? sender, RoutedEventArgs e)
    {
        var lines = File.ReadAllLines("dane/pozycje.txt");
        CountText.Text = $"Pozycji w pliku: {lines.Length}";
    }
    ```
* **Czas wykonania:** 15 minut.
* **Co było najtrudniejsze:** Upewnienie się, że w pliku kodu zaimportowano przestrzenie `Avalonia.Interactivity` (dla `RoutedEventArgs`) oraz `System.IO` (dla `File`), a także dopilnowanie, by nazwa zdarzenia `Click` dokładnie odpowiadała sygnaturze metody.
