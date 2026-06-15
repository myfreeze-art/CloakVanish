using System;
using System.Windows;
using System.Windows.Controls;
using CloakVanishCore;

namespace CloakVanishApp
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            LoadLanguage("en");
        }

        private void LoadLanguage(string langCode)
        {
            try
            {
                ResourceDictionary dict = new ResourceDictionary();
                dict.Source = new Uri($"Locales/{langCode}.xaml", UriKind.Relative);

                // Keep default dictionaries if any exist, but replace the locale one
                Application.Current.Resources.MergedDictionaries.Clear();
                Application.Current.Resources.MergedDictionaries.Add(dict);
            }
            catch (Exception ex)
            {
                // Fallback silently or log if missing
                Console.WriteLine($"Could not load language {langCode}: {ex.Message}");
            }
        }

        private void CboLanguage_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CboLanguage.SelectedItem is ComboBoxItem selectedItem)
            {
                string langCode = selectedItem.Tag.ToString();
                LoadLanguage(langCode);
            }
        }

        private void BtnGenerateAlias_Click(object sender, RoutedEventArgs e)
        {
            string newAlias = AliasGenerator.GenerateAlias();
            TxtAliasOutput.Foreground = new System.Windows.Media.SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#007ACC"));
            TxtAliasOutput.Text = $"{newAlias}@cv-mail.node";
        }

        private void BtnNeutralize_Click(object sender, RoutedEventArgs e)
        {
            string inputHtml = TxtHtmlInput.Text;
            if (string.IsNullOrWhiteSpace(inputHtml))
            {
                string warningMsg = Application.Current.Resources["MsgNoInput"] as string ?? "Please provide a raw HTML payload to sanitize.";
                string warningTitle = Application.Current.Resources["MsgInputRequiredTitle"] as string ?? "Input Required";
                MessageBox.Show(warningMsg, warningTitle, MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            string sanitizedHtml = TrackerNeutralizer.NeutralizeTrackers(inputHtml);

            if (sanitizedHtml == inputHtml)
            {
                string noTrackersMsg = Application.Current.Resources["MsgNoTrackers"] as string ?? "/* No tracking pixels detected in payload. */";
                TxtHtmlOutput.Text = $"{noTrackersMsg}\n\n{sanitizedHtml}";
                TxtHtmlOutput.Foreground = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.LightGray);
            }
            else
            {
                string successMsg = Application.Current.Resources["MsgSuccess"] as string ?? "/* SUCCESS: Tracking heuristics neutralized. Safe to view. */";
                TxtHtmlOutput.Text = $"{successMsg}\n\n{sanitizedHtml}";
                TxtHtmlOutput.Foreground = new System.Windows.Media.SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#107C10"));
            }
        }
    }
}
