using System.IO;

namespace Desktoper_V5
{
    class Moving
    {
        public static void MoveAll(int IndexDalsiPlochy)
        {
            string sourceDirectory = Path.Combine(GlobalVals.basePath, GlobalVals.Plochy[IndexDalsiPlochy]);
            string destinationDirectory = Path.Combine(GlobalVals.basePath, GlobalVals.Plochy[GlobalVals.indexPlochy]);
            string desktopPath = GlobalVals.desktopPath;

            //RetryFailedMoves();

            Moveing(desktopPath, destinationDirectory);
            Moveing(sourceDirectory, desktopPath);
        }

        private static void RetryFailedMoves()
        {
            if (GlobalVals.NepresunuteSoubory.Count > 0)
            {
                foreach (var nepovedenejString in GlobalVals.NepresunuteSoubory.ToArray()) // Rozbalíme tuple do dvou proměnných
                {
                    string[] pair = nepovedenejString.Split("|");
                    if (pair.Length != 2) continue; // Ověříme, že máme obě části

                    string sourcePath = pair[0];
                    string destPath = pair[1];

                    try
                    {
                        // Zjištění, zda je sourcePath soubor nebo adresář
                        if (File.Exists(sourcePath))
                        {
                            // Přesun souboru
                            File.Move(sourcePath, destPath);
                            GlobalVals.NepresunuteSoubory.Remove(nepovedenejString); // Úspěšně přesunuto, odstranění ze seznamu
                        }
                        else if (Directory.Exists(sourcePath))
                        {
                            // Přesun složky
                            Moveing(sourcePath, destPath);
                            Directory.Delete(sourcePath, true); // Smazání prázdné složky po přesunu
                            GlobalVals.NepresunuteSoubory.Remove(nepovedenejString);
                        }
                        else
                        {
                            // Pokud položka již neexistuje, odstraníme ji ze seznamu
                            GlobalVals.NepresunuteSoubory.Remove(nepovedenejString);
                        }
                    }
                    catch (Exception)
                    {
                        
                    }
                }
            }
        }

        public static void Moveing(string sourceDir, string destDir)
        {
            string[] files = Directory.GetFiles(sourceDir);
            foreach (string dir in Directory.GetDirectories(sourceDir))
            {
                if (Path.GetFileName(dir) == "NoMoveToOtherDesktop") continue;

                string destDirPath = Path.Combine(destDir, Path.GetFileName(dir));
                try
                {
                    Directory.Move(dir, destDirPath);
                }
                catch (Exception)
                {
                    // Přidáme původní a cílovou cestu do seznamu
                    string nepovedeno = dir + "|" + destDirPath;
                    GlobalVals.NepresunuteSoubory.Add(nepovedeno); // Používáme Tuple<string, string>
                }

            }

            foreach (string file in files)
            {
                string name = Path.GetFileName(file);

                if (name == "Desktoper.exe") continue;
                if (name == "Desktoper.appref-ms") continue;

                string destFile = Path.Combine(destDir, name);
                try
                {
                    File.Move(file, destFile);
                }
                catch (Exception)
                {
                    // Přidáme původní a cílovou cestu do seznamu
                    string nepovedeno = file + "|" + destFile;
                    GlobalVals.NepresunuteSoubory.Add(nepovedeno); // Používáme Tuple<string, string>
                }
            }

            
        }
    }
}
