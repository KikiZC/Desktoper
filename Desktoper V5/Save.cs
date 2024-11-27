using System.IO;

namespace Desktoper_V5
{
    class Save
    {
        public static void All()
        {
            Settings();
            SeznamPloch();
            IndexPlochy();
            SeznamThemu();
        }

        private static void SeznamThemu()
        {
            string pathToFile = Path.Combine(GlobalVals.basePath, "themes.dsk");

            List<string> list = [];
            foreach (var pair in GlobalVals.Themes)
            {
                string values = pair.Key + ":" + string.Join("|", pair.Value);
                list.Add(values);
            }

            File.WriteAllLines(pathToFile, list);
        }

        private static void IndexPlochy()
        {
            string indexSoubor = Path.Combine(GlobalVals.basePath, "last_index.dsk");

            File.WriteAllText(indexSoubor, Convert.ToString(GlobalVals.indexPlochy));
        }

        private static void SeznamPloch()
        {
            string pathToFile = Path.Combine(GlobalVals.basePath, "plochy.dsk");

            List<string> list = [];
            foreach (var pair in GlobalVals.Plochy)
            {
                string values = pair.Key + "|" + pair.Value;
                list.Add(values);
            }

            File.WriteAllLines(pathToFile, list);
        }

        private static void Settings()
        {
            string pathToFile = Path.Combine(GlobalVals.basePath, "settings.dsk");

            List<string> list = [];
            foreach (var pair in GlobalVals.settings)
            {
                string values = pair.Key + ":" + pair.Value;
                list.Add(values);
            }

            File.WriteAllLines(pathToFile, list);
        }
    }
}
