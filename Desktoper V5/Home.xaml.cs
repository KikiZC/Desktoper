using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Desktoper_V5
{
    /// <summary>
    /// Interakční logika pro Home.xaml
    /// </summary>
    public partial class Home : Page
    {
        public Home()
        {
            InitializeComponent();
            GenerateItems();
        }

        private void GenerateItems()
        {
            Uri uri = new Uri("pack://application:,,,/Fonts/saiba-45.regular.ttf");
            FontFamily font = new(uri, "./#SAIBA-45");

            MainWrapPanel.Children.Clear();

            foreach (var plocha in GlobalVals.Plochy)
            {
                Brush borderBrusher;

                if (GlobalVals.indexPlochy != -1 && plocha.Key == GlobalVals.indexPlochy)
                {
                    borderBrusher = GetBrush("homeBorderSelected");
                }
                else
                {
                    borderBrusher = GetBrush("homeBorderSelectedNot");
                }

                // Vytvoření kontejneru Border
                Border border = new()
                {
                    Margin = new Thickness(5),
                    Padding = new Thickness(10),
                    BorderBrush = borderBrusher,
                    BorderThickness = new Thickness(3),
                    Background = GetBrush("homeDesktopNameBorderBackground")
                };

                // Vytvoření StackPanel
                StackPanel stackPanel = new()
                {
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center
                };

                // TextBlock s názvem (bude nad tlačítkem)
                TextBlock textBlock = new()
                {
                    Text = plocha.Value,
                    FontWeight = FontWeights.Bold,
                    Foreground = GetBrush("homeTextColor"),
                    FontSize = 25,
                    HorizontalAlignment = HorizontalAlignment.Center
                };
                stackPanel.Children.Add(textBlock);

                // Vytvoření tlačítka s nápisem "Load"
                Button button = GetButton(font);

                // Přidání události pro kliknutí na tlačítko
                button.Click += LoadButton_Click;
                button.Tag = plocha.Key;

                // Přidání tlačítka do StackPanel
                stackPanel.Children.Add(button);

                // Přidání StackPanelu do Border
                border.Child = stackPanel;

                // Přidání Border do WrapPanel
                MainWrapPanel.Children.Add(border);
            }
        }

        private static Button GetButton(FontFamily font)
        {
            Button button = new()
            {
                Content = "LOAD",
                Background = GetBrush("homeButtonBackground"),
                Foreground = GetBrush("homeButtonForeground"),
                BorderBrush = GetBrush("homeButtonBorderBrush"),
                BorderThickness = new Thickness(2),
                Width = 200,
                Height = 40,
                Margin = new Thickness(10),
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Center,
                Cursor = Cursors.Hand
            };

            // Definice šablony tlačítka
            ControlTemplate template = new(typeof(Button));
            FrameworkElementFactory border = new(typeof(Border));
            border.SetValue(Border.BackgroundProperty, new TemplateBindingExtension(Button.BackgroundProperty));
            border.SetValue(Border.BorderBrushProperty, new TemplateBindingExtension(Button.BorderBrushProperty));
            border.SetValue(Border.BorderThicknessProperty, new TemplateBindingExtension(Button.BorderThicknessProperty));
            border.SetValue(Border.CornerRadiusProperty, new CornerRadius(5));

            FrameworkElementFactory grid = new(typeof(Grid));
            FrameworkElementFactory textBlock = new(typeof(TextBlock));
            textBlock.SetValue(TextBlock.TextProperty, new TemplateBindingExtension(Button.ContentProperty));
            textBlock.SetValue(TextBlock.ForegroundProperty, new TemplateBindingExtension(Button.ForegroundProperty));
            textBlock.SetValue(TextBlock.MarginProperty, new Thickness(10));
            textBlock.SetValue(TextBlock.VerticalAlignmentProperty, VerticalAlignment.Center);
            textBlock.SetValue(TextBlock.FontFamilyProperty, font);
            textBlock.SetValue(TextBlock.FontSizeProperty, 36.0);
            textBlock.SetValue(TextBlock.LineHeightProperty, 16.0);
            textBlock.SetValue(TextBlock.TextAlignmentProperty, TextAlignment.Center);

            // Vložení TextBlock do Grid
            grid.AppendChild(textBlock);

            // Vložení Grid do Border
            border.AppendChild(grid);

            // Nastavení šablony
            template.VisualTree = border;

            // Definice Triggeru pro změnu vzhledu při najetí myší
            Trigger mouseOverTrigger = new()
            {
                Property = IsMouseOverProperty,
                Value = true
            };
            mouseOverTrigger.Setters.Add(new Setter()
            {
                Property = BackgroundProperty,
                Value = GetBrush("homeButtonOverBakground")
            });
            mouseOverTrigger.Setters.Add(new Setter()
            {
                Property = Button.BorderBrushProperty,
                Value = GetBrush("homeButtonOverBorderBrush")
            });

            template.Triggers.Add(mouseOverTrigger);

            // Nastavení šablony tlačítka
            button.Template = template;

            return button;
        }

        private static SolidColorBrush GetBrush(string typ) => new((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString(GlobalVals.Themes[typ][GlobalVals.selectedThemeIndex]));

        private void LoadButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.Tag is int id)
            {
                Moving.MoveAll(id);
                GlobalVals.indexPlochy = id;

                // Zajištění znovuvykreslení s novým výběrem
                GenerateItems();
            }
        }
    }
}
