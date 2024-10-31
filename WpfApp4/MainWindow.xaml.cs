using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TextFormatter
{
    
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        // Funkcja zmieniająca rozmiar czcionki
        private void FontSizeSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            UpdateFormatting();
            UpdateProgress();
        }

        // Funkcja aktualizująca ustawienia formatowania
        private void FormattingOptionChanged(object sender, RoutedEventArgs e)
        {
            UpdateFormatting();
            UpdateProgress();
        }
        // Aktualizacja formatowania tekstu
        private void UpdateFormatting()
        {
            if (RichTextBox.Selection != null)
            {
                var selectedText = RichTextBox.Selection;

                // Pogrubienie
                selectedText.ApplyPropertyValue(TextElement.FontWeightProperty, BoldCheckBox.IsChecked == true ? FontWeights.Bold : FontWeights.Normal);

                // Kursywa
                selectedText.ApplyPropertyValue(TextElement.FontStyleProperty, ItalicCheckBox.IsChecked == true ? FontStyles.Italic : FontStyles.Normal);

                // Podkreślenie
                TextDecorationCollection textDecorations = new TextDecorationCollection();
                if (UnderlineCheckBox.IsChecked == true)
                    textDecorations.Add(TextDecorations.Underline[0]);
                selectedText.ApplyPropertyValue(Inline.TextDecorationsProperty, textDecorations);

                // Rozmiar czcionki
                selectedText.ApplyPropertyValue(TextElement.FontSizeProperty, FontSizeSlider.Value);
            }
        }
        // Ustawienia wyrównania
        private void AlignLeft_Click(object sender, RoutedEventArgs e) => SetAlignment(TextAlignment.Left);
        private void AlignCenter_Click(object sender, RoutedEventArgs e) => SetAlignment(TextAlignment.Center);
        private void AlignRight_Click(object sender, RoutedEventArgs e) => SetAlignment(TextAlignment.Right);
        private void AlignJustify_Click(object sender, RoutedEventArgs e) => SetAlignment(TextAlignment.Justify);

        private void SetAlignment(TextAlignment alignment)
        {
            RichTextBox.Document.TextAlignment = alignment;
            UpdateProgress();
        }

        // Pasek postępu - oblicza procent na podstawie włączonych opcji
        private void UpdateProgress()
        {
            int totalOptions = 7; // liczba opcji do włączenia
            int selectedOptions = 0;

            if (BoldCheckBox.IsChecked == true) selectedOptions++;
            if (ItalicCheckBox.IsChecked == true) selectedOptions++;
            if (UnderlineCheckBox.IsChecked == true) selectedOptions++;
            if (RichTextBox.Document.TextAlignment != TextAlignment.Left) selectedOptions++;
            if (FontSizeSlider.Value > 8) selectedOptions++;

            ProgressBar.Value = (double)selectedOptions / totalOptions * 100;
        }
    }
}