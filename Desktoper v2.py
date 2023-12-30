import os
import shutil
import pygame
import time

# Inicializace Pygame
pygame.init()

# Nastavení okna
width, height = 300, 500
screen = pygame.display.set_mode((width, height))
pygame.display.set_caption("Desktoper")

font = pygame.font.SysFont("Monocraft.ttf", 36)

# Barvy
seda = (50, 50, 50)
zelena = (50, 150, 50)
bila = (255, 255, 255)
cerna = (0, 0, 0)
modra = (20, 20, 200)
oranzova = (255, 73, 51)

desktop_path = os.path.expanduser("~/Desktop")
breaker = True

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

def Icon_swap(seznam_ploch, vybrana_plocha_path):
    index_plochy = Posledni_Plocha()
    do_plocha = os.path.join(desktop_path, seznam_ploch[index_plochy])
    z_plocha = vybrana_plocha_path

    for soubor in os.listdir(desktop_path):
        if soubor not in nemenitelnost:
            try:
                puvodni_cesta = os.path.join(desktop_path, soubor)
                cilova_cesta = os.path.join(do_plocha, soubor)
                shutil.move(puvodni_cesta, cilova_cesta)
            except:
                print("Nepovedlo se přesunout: " + soubor)

    for soubor in os.listdir(z_plocha):
        if soubor not in nemenitelnost:
            try:
                puvodni_cesta = os.path.join(z_plocha, soubor)
                cilova_cesta = os.path.join(desktop_path, soubor)
                shutil.move(puvodni_cesta, cilova_cesta)
            except:
                print("Nepovedlo se přesunout: " + soubor)

    print("Ikony byly přesunuty mezi plochami. Poslední použitá plocha: {}".format(z_plocha))
    pass  

seznam_ploch = Seznam_ploch_load()
nemenitelnost = seznam_ploch + ["Desktoper.py", "desktop_index.dsk", "Plochy.dsk"]

# Malovani tlacitek s odpovedma
tlacitko_sirka = 300
tlacitko_vyska = 50
tlacitko_mezera = 30

tlacitka = []

def Draw_moznosti(seznam):
    global tlacitka
    tlacitka = []

    tlacitka_rozdil = tlacitko_vyska + tlacitko_mezera

    for i, moznost in enumerate(seznam):
        x = 0
        y = i * tlacitka_rozdil

        tlacitko_rect = pygame.Rect(x, y, tlacitko_sirka, 50)
        tlacitka.append(tlacitko_rect)

        # Změna barvy tlačítka
        try:
            if tlacitko_rect.collidepoint(pygame.mouse.get_pos()):
                pygame.draw.rect(screen, modra, tlacitko_rect)
            else:
                pygame.draw.rect(screen, zelena, tlacitko_rect)
        except:
            if tlacitko_rect.collidepoint(pygame.mouse.get_pos()):
                pygame.draw.rect(screen, modra, tlacitko_rect)
            else:
                pygame.draw.rect(screen, zelena, tlacitko_rect)

        text = font.render(moznost, True, bila)
        text_rect = text.get_rect(center=tlacitko_rect.center)
        screen.blit(text, text_rect)

def Kliknuto(index):
    global breaker, seznam_ploch
    if breaker:
        breaker = False
        Icon_swap(seznam_ploch, os.path.join(desktop_path, seznam_ploch[index]))
        akt_index_plochy(index)
        seznam_ploch = Seznam_ploch_load()

running = True
while running:
    screen.fill(seda)

    for event in pygame.event.get():
        if event.type == pygame.MOUSEBUTTONDOWN and event.button == 1:
            mouse_pos = pygame.mouse.get_pos()

            for i, odpoved_rect in enumerate(tlacitka):
                if odpoved_rect.collidepoint(mouse_pos):
                    Kliknuto(i)

        if event.type == pygame.MOUSEBUTTONUP and event.button == 1:
            breaker = True
        
        if event.type == pygame.QUIT:
            running = False

    Draw_moznosti(seznam_ploch)

    pygame.display.flip()
