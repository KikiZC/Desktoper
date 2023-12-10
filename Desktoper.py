import os
import shutil

index_plochy = None

def Seznam_ploch_load():
    plochy_soubor = os.path.join(desktop_path, "Plochy.dsk")

    try:
        with open(plochy_soubor, "r") as f:
            obsah = f.readlines()
            return [plocha.strip() for plocha in obsah]
    except FileNotFoundError:
        return []

def Posledni_Plocha():
    index_soubor = os.path.join(desktop_path, "desktop_index.dsk")

    try:
        with open(index_soubor, "r") as f:
            obsah = f.read().strip()
            if obsah.isdigit():
                return int(obsah)
    except FileNotFoundError:
        pass

    return 0

def akt_index_plochy(brana_plocha):
    index_soubor = os.path.join(desktop_path, "desktop_index.dsk")

    with open(index_soubor, "w") as f:
        f.write(str(brana_plocha))

# Code by KikiZC

def Icon_swap(seznam_ploch):
    global index_plochy
    do_plocha = os.path.join(desktop_path, seznam_ploch[Posledni_Plocha()])
    z_plocha = vybrana_plocha_path

    for soubor in os.listdir(desktop_path):
        if soubor not in nemenitelnost:
            puvodni_cesta = os.path.join(desktop_path, soubor)
            cilova_cesta = os.path.join(do_plocha, soubor)
            shutil.move(puvodni_cesta, cilova_cesta)

    for soubor in os.listdir(z_plocha):
        if soubor not in nemenitelnost:
            puvodni_cesta = os.path.join(z_plocha, soubor)
            cilova_cesta = os.path.join(desktop_path, soubor)
            shutil.move(puvodni_cesta, cilova_cesta)

    akt_index_plochy(index_plochy)

    print("Ikony byly přesunuty mezi plochami. Poslední použitá plocha: {}".format(z_plocha))
    pass

def Chose_Plocha(seznam_ploch):
    global index_plochy
    print("Seznam dostupných ploch:")
    for i, plocha in enumerate(seznam_ploch, start=1):
        print(f"{i}. {plocha}")

    # Získej vstup od uživatele
    p = True
    while p:
        vybrana_plocha_index = int(input("Zadej číslo plochy (1 - {}): ".format(len(seznam_ploch))))
        if vybrana_plocha_index < 1 or vybrana_plocha_index > len(seznam_ploch):
            print("Neplatné číslo plochy.")
            print("")
            for i, plocha in enumerate(seznam_ploch, start=1):
                print(f"{i}. {plocha}")
        else:
            p = False
            index_plochy = vybrana_plocha_index - 1

    return seznam_ploch[vybrana_plocha_index - 1]

desktop_path = os.path.expanduser("~/Desktop")
seznam_ploch = Seznam_ploch_load()
nemenitelnost = seznam_ploch + ["Desktoper.py", "desktop_index.dsk", "Plochy.dsk"]

if not seznam_ploch:
    print("Seznam ploch nenalezen. Přidejte plochy do souboru 'Plochy.dsk'.")
else:
    vybrana_plocha_path = os.path.join(desktop_path, Chose_Plocha(seznam_ploch))

    Icon_swap(seznam_ploch)

