using System.Windows;
using System.Windows.Media.Imaging;

namespace Desktoper_V5
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            Check.CheckOrCreate();
            InitializeComponent();

            Icon = new BitmapImage(new Uri("icon.ico", UriKind.Relative));
            StartProgram();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                GlobalVals.WindowWidth = double.Parse(GlobalVals.settings["WindowWidth"]);
                GlobalVals.WindowHeight = double.Parse(GlobalVals.settings["WindowHeight"]);
            }
            catch
            {
                GlobalVals.settings = GlobalVals.baseSettings;
            }
            finally 
            {
                this.Width = GlobalVals.WindowWidth;
                this.Height = GlobalVals.WindowHeight;
            }
        }

        public void OnClose(object sender, EventArgs e)
        {
            GlobalVals.WindowHeight = this.ActualHeight;
            GlobalVals.WindowWidth = this.ActualWidth;

            GlobalVals.settings["WindowWidth"] = GlobalVals.WindowWidth.ToString();
            GlobalVals.settings["WindowHeight"] = GlobalVals.WindowHeight.ToString();

            Save.All();
        }

        public void StartProgram()
        {

            MainWindowFrame.Navigate(new MainWindowPage());
        }
    }
}