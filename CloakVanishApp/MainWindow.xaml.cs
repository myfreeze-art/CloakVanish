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
            TxtAliasOutput.Text = $"{newAlias}@cv-mail.node";
        }

        private void BtnNeutralize_Click(object sender, RoutedEventArgs e)
        {
            string inputHtml = TxtHtmlInput.Text;
            if (string.IsNullOrWhiteSpace(inputHtml))
            {
                MessageBox.Show("Please enter some HTML to neutralize.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            string sanitizedHtml = TrackerNeutralizer.NeutralizeTrackers(inputHtml);
            TxtHtmlOutput.Text = sanitizedHtml;
        }
    }
}
