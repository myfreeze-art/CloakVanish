using System.Windows;
using CloakVanishCore;

namespace CloakVanishApp
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void BtnGenerateAlias_Click(object sender, RoutedEventArgs e)
        {
            string newAlias = AliasGenerator.GenerateAlias();
            // Clear default placeholder text styling dynamically if needed
            TxtAliasOutput.Foreground = new System.Windows.Media.SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#007ACC"));
            TxtAliasOutput.Text = $"{newAlias}@cv-mail.node";
        }

        private void BtnNeutralize_Click(object sender, RoutedEventArgs e)
        {
            string inputHtml = TxtHtmlInput.Text;
            if (string.IsNullOrWhiteSpace(inputHtml))
            {
                MessageBox.Show("Please provide a raw HTML payload to sanitize.", "Input Required", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            string sanitizedHtml = TrackerNeutralizer.NeutralizeTrackers(inputHtml);

            if (sanitizedHtml == inputHtml)
            {
                TxtHtmlOutput.Text = "/* No tracking pixels detected in payload. */\n\n" + sanitizedHtml;
                TxtHtmlOutput.Foreground = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.LightGray);
            }
            else
            {
                TxtHtmlOutput.Text = "/* SUCCESS: Tracking heuristics neutralized. Safe to view. */\n\n" + sanitizedHtml;
                TxtHtmlOutput.Foreground = new System.Windows.Media.SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#107C10"));
            }
        }
    }
}
