using IWshRuntimeLibrary;
using System.IO;
using System.Reflection;

namespace Desktoper_V5
{
    class Check
    {
        public static void CheckOrCreate()
        {
            BaseFolder();
            SettingsFile();
            SeznamPloch();
            IndexPlochy();
            Themes();
            HowTo();
            TryToApplySettings();
            //Odkaz();
        }

        private static void Odkaz()
        {
            string path = Path.Combine(GlobalVals.desktopPath, "Desktoper.appref-ms");
            string exePath = Assembly.GetExecutingAssembly().Location;


            if (!System.IO.File.Exists(path))
            {
                var shell = new WshShell();
                IWshShortcut shortcut = (IWshShortcut)shell.CreateShortcut(path);
                shortcut.Description = "Odkaz na můj program";
                shortcut.TargetPath = exePath; // Cíl zástupce (soubor, na který odkazujeme)
                shortcut.Save();
            }
        }

        private static void TryToApplySettings()
        {
            try
            {
                if (GlobalVals.settings.TryGetValue("Theme", out string? value)) GlobalVals.selectedThemeIndex = Convert.ToInt32(value);

            }
            catch (Exception)
            {

            }
        }

        private static void HowTo()
        {
            string pathToFile = Path.Combine(GlobalVals.basePath, "HowTo.txt");

            if (!System.IO.File.Exists(pathToFile))
            {
                System.IO.File.Create(pathToFile).Dispose();

                string howto = "Manual for Desktoper V5\n" +
                    "Themes:\n" +
                    "   To create or modify themes do this:\n" +
                    "   1. Navigate to " + GlobalVals.basePath + "\n" +
                    "   2. Open themes.dsk in any text editor.\n" +
                    "   3. To modify existing theme just rewrite the values by new ones.\n" +
                    "   4. To create new theme add | behind last value and then write new value.\n" +
                    "   ! If creating new theme you MUST fill all the values otherwise the theme would not work and app will crash.\n" +
                    "   You can use any color in HEX type (#1F1F1F and #FF1F1F1F) or you can look to color name on this site: https://learn.microsoft.com/en-us/dotnet/api/system.windows.media.colors?view=windowsdesktop-8.0 there is a picture with all colors\n" +
                    "\n" +
                    "Unmovables:\n" +
                    "   From Desktop there are a few objects that will stay. First is Desktoper.exe, it will stay there.\n" +
                    "   Second object that stays is a special folder in case you want to take icons and other things from the current desktop to a different one. The special folder must be named like this: NoMoveToOtherDesktop \n" +
                    "   Lastly, the icons that will stay are the ones in both desktops, or other unmovable icons (i don´t know why, but they will stay, e.g. Bin or My Computer). \n" +
                    "\n" +
                    "Licenses and Credit:\n" +
                    "   Font used: https://www.1001fonts.com/saiba-45-font.html\n" +
                    "   You can use this app anywhere you want, but if you want to feature it in a video, give credit to the creator, KikiZC. Thanks.\n" +
                    "\n" +
                    "How to use:\n" +
                    "   To create a new desktop, press \"SETTINGS\", then enter your new desktop´s name and press \"CREATE\".\n" +
                    "   To load a desktop press the \"LOAD\" button under the desired desktop´s name.\n" +
                    "   To change the current theme, just go to \"THEMES\" and press the \"LOAD\" button.\n" +
                    "   To delete a desktop navigate to " + GlobalVals.basePath + " and delete the folder of the desktop you want to delete, next remove the coresponding line in plochy.dsk file.";

                System.IO.File.WriteAllText(pathToFile, howto);
            }
        }

        private static void Themes()
        {
            string pathToFile = Path.Combine(GlobalVals.basePath, "themes.dsk");

            if (System.IO.File.Exists(pathToFile))
            {
                string[] lines = System.IO.File.ReadAllLines(pathToFile);

                foreach (string line in lines)
                {
                    string[] pair = line.Split(':');

                    string key = pair[0];
                    string[] value = pair[1].Split("|");

                    GlobalVals.Themes.TryAdd(key, value);
                }
            }
            else
            {
                System.IO.File.Create(pathToFile).Dispose();
                GlobalVals.Themes = GlobalVals.BaseThemes;
            }
        }

        private static void IndexPlochy()
        {
            string indexSoubor = Path.Combine(GlobalVals.basePath, "last_index.dsk");

            if (System.IO.File.Exists(indexSoubor))
            {
                string content = System.IO.File.ReadAllText(indexSoubor).Trim();

                if (int.TryParse(content, out int index))
                    GlobalVals.indexPlochy = index;
                return;
            }
            else
            {
                System.IO.File.Create(indexSoubor).Dispose();
            }

        }

        private static void SeznamPloch()
        {
            string pathToFile = Path.Combine(GlobalVals.basePath, "plochy.dsk");

            if (System.IO.File.Exists(pathToFile))
            {
                string[] lines = System.IO.File.ReadAllLines(pathToFile);

                foreach (string line in lines)
                {
                    string[] pair = line.Split('|');

                    if (!GlobalVals.Plochy.Keys.Contains(Convert.ToInt32(pair[0]))) GlobalVals.Plochy.Add(Convert.ToInt32(pair[0]), pair[1]);

                }
            }
            else
            {
                System.IO.File.Create(pathToFile).Dispose();
            }
        }

        private static void SettingsFile()
        {
            string pathToFile = Path.Combine(GlobalVals.basePath, "settings.dsk");

            if (System.IO.File.Exists(pathToFile))
            {
                string[] lines = System.IO.File.ReadAllLines(pathToFile);

                foreach (string line in lines)
                {
                    string[] pair = line.Split(':');

                    if (!GlobalVals.settings.Keys.Contains(pair[0])) GlobalVals.settings.Add(pair[0], pair[1]);
                }
            }
            else
            {
                System.IO.File.Create(pathToFile).Dispose();
                GlobalVals.settings = GlobalVals.baseSettings;
            }
        }

        private static void BaseFolder()
        {
            if (!Directory.Exists(GlobalVals.basePath))
            {
                Directory.CreateDirectory(GlobalVals.basePath);
            }
        }
    }
}
