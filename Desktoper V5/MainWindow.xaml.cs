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
            InitializeComponent();

            Icon = new BitmapImage(new Uri("icon.ico", UriKind.Relative));
            StartProgram();
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