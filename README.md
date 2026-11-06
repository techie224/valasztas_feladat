# Választási eredmények elemzője 🗳️

C# konzolalkalmazás, amely egy kitalált helyhatósági választás eredményeit elemzi egy szövegfájl alapján LINQ és Lambda alkalmazásával egyaránt.

## Mit csinál?

- Megszámolja a képviselőjelölteket
- Név alapján megkeresi egy jelölt szavazatait (felhasználói bemenet)
- Kiszámolja a részvételi arányt
- Kiszámolja az egyes pártok szavazatarányát
- Megkeresi a legtöbb szavazatot kapott jelölt(ek)et
- Megadja minden választókerület győztesét, és elmenti a `kepviselok.txt` fájlba

Minden feladat kétféleképpen is meg van megoldva, egy LINQ, egy Lambda.

## Bemeneti fájl formátuma

A `szavazatok.txt` soronként egy jelöltet tartalmaz, szóközzel elválasztva:

```
választókerület szavazatok vezetéknév utónév párt
```

## Technológiák

C#, .NET Framework 4.7.2, LINQ, Lambda, osztályok, fájlkezelés

## Futtatás

Windows és Visual Studio szükséges.

1. Nyisd meg a `valasztas.sln` fájlt (vagy a `valasztas.csproj` fájlt) Visual Studióban
2. Nyomd meg az F5 gombot

A `szavazatok.txt` fájl a build során automatikusan a kimeneti mappába másolódik.