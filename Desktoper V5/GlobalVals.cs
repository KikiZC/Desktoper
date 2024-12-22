using System.IO;

namespace Desktoper_V5
{
    class GlobalVals
    {
        public static string basePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Desktoper");
        public static string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

        public static int WindowWidth = 1900;
        public static int WindowHeight = 1080;

        public static Dictionary<string, string> settings = [];
        public static Dictionary<string, string> baseSettings = new()
        {
            { "Theme", "0" },
            { "", "" }
        };

        public static Dictionary<string, string[]> Themes = [];
        public static Dictionary<string, string[]> BaseThemes = new() // base: {"", []},
        {
            {"Nazvy",                                       ["DarkLightBlue", "Green",       "Blue",        "DarkGreen",      "DarkRed",      "DarkYelow",                          ]},
            {"homeBorderSelected",                          ["#4169E1",     "#006400",       "#000000",     "#047513",        "#a80505",      "#e0b107",                            ]}, //0
            {"homeBorderSelectedNot",                       ["#000000",     "#000000",       "#050F42",     "#000000",        "#000000",      "#000000",                            ]}, //1
            {"homeTextColor",                               ["LightBlue",   "LightGreen",    "#03071C",     "Green",          "Red",          "Yellow",                              ]}, //2
            {"homeButtonBackground",                        ["#3B3B3B",     "#2E4E2E",       "#091C7d",     "#3B3B3B",        "#3B3B3B",      "#3B3B3B",                            ]}, //3
            {"homeButtonBorderBrush",                       ["#000000",     "#000000",       "#050F42",     "#000000",        "#000000",      "#000000",                            ]}, //4
            {"homeButtonForeground",                        ["White",       "White",         "#03071C",     "#05a81b",        "Red",          "Yellow",                            ]}, //5
            {"homeButtonOverBakground",                     ["#4C4C4C",     "#3B603B",       "#000000",     "#4C4C4C",        "#4C4C4C",      "#4C4C4C",                            ]}, //6
            {"homeButtonOverBorderBrush",                   ["#7F7F7F",     "#5F7F5F",       "#FFFFFF",     "#7F7F7F",        "#7F7F7F",      "#7F7F7F",                            ]}, //7
            {"mainBorderBrush",                             ["#2E2E2E",     "#204020",       "#02071C",     "#2E2E2E",        "#2E2E2E",      "#2E2E2E",                            ]}, //8
            {"mainBorderBackground",                        ["#1A1A1A",     "#163216",       "#030B33",     "#1A1A1A",        "#1A1A1A",      "#1A1A1A",                            ]}, //9
            {"mainHeaderBorderBrush",                       ["#3A3A3A",     "#285828",       "#02071C",     "#3A3A3A",        "#3A3A3A",      "#3A3A3A",                            ]}, //10
            {"mainHeaderBorderBackground",                  ["#262626",     "#1E401E",       "#061459",     "#262626",        "#262626",      "#262626",                            ]}, //11
            {"mainHeaderTextForeground",                    ["#9AC0FF",     "#9ACD32",       "#03071C",     "Green",          "Red",          "Yellow",                              ]}, //12
            {"mainExitButtonBackground",                    ["#3B3B3B",     "#2E4E2E",       "#091C7d",     "#3B3B3B",        "#3B3B3B",      "#3B3B3B",                            ]}, //13
            {"mainExitButtonForeground",                    ["White",       "White",         "#03071C",     "#05a81b",        "Red",          "Yellow",                            ]}, //14
            {"mainExitButtonBorderBrush",                   ["#5F5F5F",     "#3F5F3F",       "#050F42",     "#5F5F5F",        "#5F5F5F",      "#5F5F5F",                            ]}, //15
            {"mainHomeButtonBackground",                    ["#3B3B3B",     "#2E4E2E",       "#061459",     "#3B3B3B",        "#3B3B3B",      "#3B3B3B",                            ]}, //16
            {"mainHomeButtonForeground",                    ["White",       "White",         "#03071C",     "#05a81b",        "Red",          "Yellow",                            ]}, //17
            {"mainHomeButtonBorderBrush",                   ["#5F5F5F",     "#3F5F3F",       "#040F2E",     "#5F5F5F",        "#5F5F5F",      "#5F5F5F",                            ]}, //18
            {"mainSettingsButtonBackground",                ["#3B3B3B",     "#2E4E2E",       "#061459",     "#3B3B3B",        "#3B3B3B",      "#3B3B3B",                            ]}, //19
            {"mainSettingsButtonForeground",                ["White",       "White",         "#03071C",     "#05a81b",        "Red",          "Yellow",                            ]}, //20
            {"mainSettingsButtonBorderBrush",               ["#5F5F5F",     "#3F5F3F",       "#040F2E",     "#5F5F5F",        "#5F5F5F",      "#5F5F5F",                            ]}, //21
            {"mainThemesButtonBackground",                  ["#3B3B3B",     "#2E4E2E",       "#061459",     "#3B3B3B",        "#3B3B3B",      "#3B3B3B",                            ]}, //22
            {"mainThemesButtonForeground",                  ["White",       "White",         "#03071C",     "#05a81b",        "Red",          "Yellow",                            ]}, //23
            {"mainThemesButtonBorderBrush",                 ["#5F5F5F",     "#3F5F3F",       "#040B2E",     "#5F5F5F",        "#5F5F5F",      "#5F5F5F",                            ]}, //24
            {"mainMainContentBorderBorderBrush",            ["#3A3A3A",     "#285828",       "#040B2E",     "#3A3A3A",        "#3A3A3A",      "#3A3A3A",                            ]}, //25
            {"mainMainContentBorderBackground",             ["#121212",     "#1A331A",       "#061459",     "#121212",        "#121212",      "#121212",                            ]}, //26
            {"settingsCreateDesktopButtonBackground",       ["#3B3B3B",     "#2E4E2E",       "#061459",     "#3B3B3B",        "#3B3B3B",      "#3B3B3B",                            ]}, //27
            {"settingsCreateDesktopButtonForeground",       ["White",       "White",         "#03071C",     "#05a81b",        "#05a81b",      "#05a81b",                            ]}, //28
            {"settingsCreateDesktopButtonBorderBrush",      ["#5F5F5F",     "#3F5F3F",       "#050F42",     "#5F5F5F",        "#5F5F5F",      "#5F5F5F",                            ]}, //29
            {"settingsCreateDesktopTextBoxBackground",      ["#3B3B3B",     "#2E4E2E",       "#061459",     "#3B3B3B",        "#3B3B3B",      "#3B3B3B",                            ]}, //30
            {"settingsCreateDesktopTextBoxForeground",      ["White",       "White",         "#03071C",     "#05a81b",        "#05a81b",      "#05a81b",                            ]}, //31
            {"settingsCreateDesktopTextBoxBorderBrush",     ["#5F5F5F",     "#3F5F3F",       "#050F42",     "#5F5F5F",        "#5F5F5F",      "#5F5F5F",                            ]}, //32
            {"settingsCreateDesktopTextBolockForeground",   ["White",       "White",         "#03071C",     "#05a81b",        "#05a81b",      "#05a81b",                            ]}, //33
            {"settingsCreateDesktopTextBolockBackground",   ["#3B3B3B",     "#2E4E2E",       "#061459",     "#3B3B3B",        "#3B3B3B",      "#3B3B3B",                            ]}, //34
            {"homeDesktopNameBorderBackground",             ["#00000000",   "#00000000",     "#00000000",   "#00000000",      "#00000000",    "#00000000",                          ]}
        };
        public static int selectedThemeIndex = 0;

        public static Dictionary<int, string> Plochy = [];
        public static int indexPlochy = -1;

        public static List<string> NepresunuteSoubory = [];
    }
}
