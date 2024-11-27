using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Desktoper_V5
{
    /// <summary>
    /// Interakční logika pro Settings.xaml
    /// </summary>
    public partial class Settings : Page
    {
        public Settings()
        {
            InitializeComponent();

            Uri uri = new("pack://application:,,,/Fonts/saiba-45.regular.ttf");
            FontFamily font = new(uri, "./#SAIBA-45");

            SettingsStackPanel.Children.Add(new Border { Height = 40 }); // Přidá prázdnou mezeru

            Border textBlockOutput = GetTextBlock("settingsCreateOutputTextBlock", "State:", 425, font);

            SettingsStackPanel.Children.Add(textBlockOutput);

            Border textBlock = GetTextBlock("CreateNewDesktopTextBlock", "Enter new desktop´s name: ", 300, font);

            SettingsStackPanel.Children.Add(textBlock);

            TextBox tbx = GetTextBox(font);
            SettingsStackPanel.Children.Add(tbx);

            Button but = GetButton(font);
            but.Click += CreateDesktopButton_Click;
            SettingsStackPanel.Children.Add(but);

        }

        private static SolidColorBrush GetBrush(string typ) => new((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString(GlobalVals.Themes[typ][GlobalVals.selectedThemeIndex]));

        private static Border GetTextBlock(string name, string text, int width, FontFamily font)
        {
            // Vytvoření obalového Border pro TextBlock
            Border border = new()
            {
                Background = GetBrush("settingsCreateDesktopTextBolockBackground"),
                BorderBrush = GetBrush("settingsCreateDesktopTextBoxBorderBrush"), // Použití stejné barvy jako TextBox
                BorderThickness = new Thickness(2), // Shodné s TextBoxem
                CornerRadius = new CornerRadius(5), // Pokud TextBox používá zaoblené rohy
                Width = width,
                Height = 40,
                Margin = new Thickness(10),
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Center,
                Padding = new Thickness(0) // Padding nastavíme uvnitř TextBlocku
            };

            TextBlock tbx = new()
            {
                Background = Brushes.Transparent, // Transparentní, protože obalový Border už má pozadí
                Foreground = GetBrush("settingsCreateDesktopTextBolockForeground"),
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Center,
                FontFamily = font,
                FontSize = 16,
                Padding = new Thickness(10), // Shodné s TextBoxem
                TextAlignment = TextAlignment.Center,
                Name = name,
                Text = text
            };

            // Přidání TextBlocku do obalového Border
            border.Child = tbx;

            return border;
        }

        private static TextBox GetTextBox(FontFamily font)
        {
            TextBox textBox = new()
            {
                Background = GetBrush("settingsCreateDesktopTextBoxBackground"),
                Foreground = GetBrush("settingsCreateDesktopTextBoxForeground"),
                BorderBrush = GetBrush("settingsCreateDesktopTextBoxBorderBrush"),
                BorderThickness = new Thickness(2),
                Width = 400,
                Height = 40,
                Margin = new Thickness(10),
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Center,
                FontFamily = font,
                FontSize = 16,
                Padding = new Thickness(10),
                TextAlignment = TextAlignment.Center,
                Name = "NewDesktopName"
            };

            // Definice šablony pro TextBox
            ControlTemplate template = new(typeof(TextBox));

            FrameworkElementFactory border = new(typeof(Border));
            border.SetValue(Border.BackgroundProperty, new TemplateBindingExtension(TextBox.BackgroundProperty));
            border.SetValue(Border.BorderBrushProperty, new TemplateBindingExtension(TextBox.BorderBrushProperty));
            border.SetValue(Border.BorderThicknessProperty, new TemplateBindingExtension(TextBox.BorderThicknessProperty));
            border.SetValue(Border.CornerRadiusProperty, new CornerRadius(5));

            FrameworkElementFactory grid = new(typeof(Grid));

            FrameworkElementFactory textBoxTextBlock = new(typeof(TextBlock));
            textBoxTextBlock.SetValue(TextBlock.TextProperty, new TemplateBindingExtension(TextBox.TextProperty));
            textBoxTextBlock.SetValue(TextBlock.ForegroundProperty, new TemplateBindingExtension(TextBox.ForegroundProperty));
            textBoxTextBlock.SetValue(TextBlock.MarginProperty, new Thickness(10));
            textBoxTextBlock.SetValue(TextBlock.VerticalAlignmentProperty, VerticalAlignment.Center);
            textBoxTextBlock.SetValue(TextBlock.FontFamilyProperty, new FontFamily("SAIBA-45"));
            textBoxTextBlock.SetValue(TextBlock.FontSizeProperty, 16.0);
            textBoxTextBlock.SetValue(TextBlock.LineHeightProperty, 16.0);
            textBoxTextBlock.SetValue(TextBlock.TextAlignmentProperty, TextAlignment.Center);

            // Vložení TextBlock do Grid
            grid.AppendChild(textBoxTextBlock);

            // Vložení Grid do Border
            border.AppendChild(grid);

            // Nastavení šablony pro TextBox
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
                Property = TextBox.BorderBrushProperty,
                Value = GetBrush("homeButtonOverBakground")
            });

            template.Triggers.Add(mouseOverTrigger);

            // Nastavení šablony pro TextBox
            textBox.Template = template;

            return textBox;
        }

        private static Button GetButton(FontFamily font)
        {
            Button button = new()
            {
                Content = "CREATE",
                Background = GetBrush("settingsCreateDesktopButtonBackground"),
                Foreground = GetBrush("settingsCreateDesktopButtonForeground"),
                BorderBrush = GetBrush("settingsCreateDesktopButtonBorderBrush"),
                BorderThickness = new Thickness(2),
                Width = 200,
                Height = 40,
                Margin = new Thickness(10),
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Center,
                Cursor = Cursors.Hand,

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

        private void CreateDesktopButton_Click(object sender, RoutedEventArgs e)
        {
            TextBox textBox = SettingsStackPanel.Children.OfType<TextBox>().FirstOrDefault();
            string desktopName = textBox.Text;

            TextBlock? tbx = SettingsStackPanel.Children
                .OfType<Border>()
                .Select(border => border.Child as TextBlock)
                .FirstOrDefault(tb => tb != null && tb.Name == "settingsCreateOutputTextBlock");

            if (!string.IsNullOrWhiteSpace(desktopName))
            {
                string path = Path.Combine(GlobalVals.basePath, desktopName);
                Directory.CreateDirectory(path);

                int newKey = GlobalVals.Plochy.Keys.Count != 0 ? GlobalVals.Plochy.Keys.Max() + 1 : 0;

                if (GlobalVals.Plochy.Keys.Count == 0) GlobalVals.indexPlochy = newKey;

                GlobalVals.Plochy[newKey] = desktopName;

                if (tbx != null) tbx.Text = "State: Successfully added a new desktop.";


                Save.All();
            }
            else
            {
                if (tbx != null) tbx.Text = "State: Error, please type a new name.";
            }
        }
    }
}
