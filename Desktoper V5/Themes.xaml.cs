using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Desktoper_V5
{
    /// <summary>
    /// Interakční logika pro Themes.xaml
    /// </summary>
    public partial class Themes : Page
    {
        public Themes()
        {
            InitializeComponent();
            GenerateItems();
        }

        private void GenerateItems()
        {
            Uri uri = new Uri("pack://application:,,,/Fonts/saiba-45.regular.ttf");
            FontFamily font = new(uri, "./#SAIBA-45");

            for (int y = 0; y < GlobalVals.Themes["Nazvy"].ToArray().Length; y++)
            {
                Brush borderBrusher;

                if (GlobalVals.selectedThemeIndex != -1 && GlobalVals.selectedThemeIndex == y)
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
                    BorderThickness = new Thickness(3)
                };


                // Vytvoření StackPanel
                StackPanel stackPanel = new();

                // TextBlock s názvem
                TextBlock textBlock = new()
                {
                    Text = GlobalVals.Themes["Nazvy"][y],
                    FontWeight = FontWeights.Bold,
                    Foreground = GetBrush("homeTextColor"),
                    FontSize = 25
                };
                stackPanel.Children.Add(textBlock);

                // Tlačítko
                Button button = GetButton(font);
                button.Click += LoadButton_Click;
                button.Tag = y;
                stackPanel.Children.Add(button);

                // Přidání StackPanel do Border
                border.Child = stackPanel;

                // Přidání Border do WrapPanel
                MainWrapPanel.Children.Add(border);
            }
        }

        private static Button GetButton(FontFamily font)
        {
            Button button = new()
            {
                Content = "Load",
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
                GlobalVals.selectedThemeIndex = id;
                GlobalVals.settings["Theme"] = $"{id}";

                MainWindow.GetWindow(this).Content = new MainWindowPage(new Themes());
            }
        }
    }
}
