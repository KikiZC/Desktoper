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

            this.Width = GlobalVals.WindowWidth;
            this.Height = GlobalVals.WindowHeight;

            InitializeComponent();

            Icon = new BitmapImage(new Uri("icon.ico", UriKind.Relative));
            StartProgram();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.Width = GlobalVals.WindowWidth;
            this.Height = GlobalVals.WindowHeight;
        }

        public void OnClose(object sender, EventArgs e)
        {
            Save.All();
        }

        public void StartProgram()
        {

            MainWindowFrame.Navigate(new MainWindowPage());
        }
    }
}