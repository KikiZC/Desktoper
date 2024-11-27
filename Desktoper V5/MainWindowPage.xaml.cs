using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Desktoper_V5
{
    /// <summary>
    /// Interakční logika pro MainWindowPage.xaml
    /// </summary>
    public partial class MainWindowPage : Page
    {
        public MainWindowPage()
        {
            InitializeComponent();
            StartProgram();
        }

        public MainWindowPage(Page page)
        {
            InitializeComponent();
            StartProgram(page);
        }

        public void OnClose(object sender, EventArgs e)
        {
            Save.All();
        }

        public void StartProgram()
        {
            Check.CheckOrCreate();

            ReGenerateWindow();

            MainFrame.Navigate(new Home());
            Console.WriteLine(GlobalVals.basePath);
        }

        private void ReGenerateWindow()
        {
            Uri uri = new Uri("pack://application:,,,/Fonts/saiba-45.regular.ttf");
            FontFamily font = new(uri, "./#SAIBA-45");

            HeaderText.FontFamily = font;

            MainBorder.BorderBrush = GetBrush("mainBorderBrush");
            MainBorder.Background = GetBrush("mainBorderBackground");

            MainHeaderBorder.BorderBrush = GetBrush("mainHeaderBorderBrush");
            MainHeaderBorder.Background = GetBrush("mainHeaderBorderBackground");

            HeaderText.Foreground = GetBrush("mainHeaderTextForeground");

            ExitButton.Background = GetBrush("mainExitButtonBackground");
            ExitButton.Foreground = GetBrush("mainExitButtonForeground");
            ExitButton.BorderBrush = GetBrush("mainExitButtonBorderBrush");

            HomeButton.Background = GetBrush("mainHomeButtonBackground");
            HomeButton.Foreground = GetBrush("mainHomeButtonForeground");
            HomeButton.BorderBrush = GetBrush("mainHomeButtonBorderBrush");

            SettingsButton.Background = GetBrush("mainSettingsButtonBackground");
            SettingsButton.Foreground = GetBrush("mainSettingsButtonForeground");
            SettingsButton.BorderBrush = GetBrush("mainSettingsButtonBorderBrush");

            ThemesButton.Background = GetBrush("mainThemesButtonBackground");
            ThemesButton.Foreground = GetBrush("mainThemesButtonForeground");
            ThemesButton.BorderBrush = GetBrush("mainThemesButtonBorderBrush");

            MainContentBorder.BorderBrush = GetBrush("mainMainContentBorderBorderBrush");
            MainContentBorder.Background = GetBrush("mainMainContentBorderBackground");
        }

        private static SolidColorBrush GetBrush(string typ) => new((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString(GlobalVals.Themes[typ][GlobalVals.selectedThemeIndex]));

        public void StartProgram(Page page)
        {
            ReGenerateWindow();
            MainFrame.Navigate(page);
        }

        public void Reload()
        {
            MainFrame.Navigate(new Themes());
        }

        private void HomeButton_Click(object sender, RoutedEventArgs e) => MainFrame.Navigate(new Home());
        private void SettingsButton_Click(object sender, RoutedEventArgs e) => MainFrame.Navigate(new Settings());
        private void ThemesButton_Click(object sender, RoutedEventArgs e) => MainFrame.Navigate(new Themes());

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.GetWindow(this).Close();
        }

        private void OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            // Začne pohyb okna, když uživatel klikne na panel
            MainWindow.GetWindow(this).DragMove();
        }
    }
}
