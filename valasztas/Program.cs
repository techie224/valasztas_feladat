using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace valasztas
{
    internal class Program
    {
        class Jelolt
        {
            public int valasztokerulet { get; set; }
            public int szavazatok { get; set; }
            public string nev { get; set; }
            public string part { get; set; }

            public Jelolt(string sor)
            {
                string[] adatsor = sor.Split(' ');
                valasztokerulet = Convert.ToInt32(adatsor[0]);
                szavazatok = Convert.ToInt32(adatsor[1]);
                nev = adatsor[2] + " " + adatsor[3];
                part = adatsor[4];
            }
        }

        static void Main()
        {
            List<Jelolt> jeloltek = new List<Jelolt>();
            string[] sorok = File.ReadAllLines("szavazatok.txt");
            foreach (string sor in sorok)
            {
                jeloltek.Add(new Jelolt(sor));
            }

            // 2. feladat
            Console.WriteLine("2. feladat");
            var eredmeny21 = (from jelolt in jeloltek
                              select jelolt.nev).Count();
            Console.WriteLine($"A helyhatósági választáson {eredmeny21} képviselőjelölt indult.");

            var eredmeny22 = jeloltek.Select(x => x.nev).Count();
            Console.WriteLine($"A helyhatósági választáson {eredmeny22} képviselőjelölt indult.");
            Console.WriteLine();

            // 3. feladat
            Console.WriteLine("3. feladat");
            Console.Write("Kérem egy képviselő nevét (vezetéknév utónév): ");
            string nevv = Console.ReadLine();

            var eredmeny31 = from jelolt in jeloltek
                             where jelolt.nev.Trim().ToLower() == nevv.Trim().ToLower()
                             select jelolt.szavazatok;
            if (eredmeny31.Any())
            {
                Console.WriteLine($"{nevv} {eredmeny31.First()} szavazatot kapott.");
            }
            else
            {
                Console.WriteLine("Ilyen nevű képviselőjelölt nem szerepel a nyilvántartásban!");
            }

            var eredmeny32 = jeloltek.Where(y => y.nev.Trim().ToLower() == nevv.Trim().ToLower()).Select(x => x.szavazatok);
            if (eredmeny32.Any())
            {
                Console.WriteLine($"{nevv} {eredmeny32.First()} szavazatot kapott.");
            }
            else
            {
                Console.WriteLine("Ilyen nevű képviselőjelölt nem szerepel a nyilvántartásban!");
            }
            Console.WriteLine();

            // 4. feladat
            Console.WriteLine("4. feladat");
            int jogosultak = 12345;

            var osszesszavazat1 = (from jelolt in jeloltek
                                   select jelolt.szavazatok).Sum();
            double reszvetel1 = (double)osszesszavazat1 / jogosultak * 100;
            Console.WriteLine($"A választáson {osszesszavazat1} állampolgár, a jogosultak {reszvetel1:F2}%-a vett részt.");

            var osszesszavazat2 = jeloltek.Select(x => x.szavazatok).Sum();
            double reszvetel2 = (double)osszesszavazat2 / jogosultak * 100;
            Console.WriteLine($"A választáson {osszesszavazat2} állampolgár, a jogosultak {reszvetel2:F2}%-a vett részt.");
            Console.WriteLine();

            // 5. feladat
            Console.WriteLine("5. feladat");
            int osszesszavazat = jeloltek.Sum(x => x.szavazatok);

            var partokszavazat1 = from jelolt in jeloltek
                                  group jelolt by jelolt.part into g
                                  select new { Part = g.Key, Szavazat = g.Sum(x => x.szavazatok) };

            foreach (var item in partokszavazat1)
            {
                string partnev = "";
                switch (item.Part)
                {
                    case "GYEP": partnev = "Gyümölcsevők Pártja"; break;
                    case "HEP": partnev = "Húsevők Pártja"; break;
                    case "TISZ": partnev = "Tejivók Szövetsége"; break;
                    case "ZEP": partnev = "Zöldségevők Pártja"; break;
                    case "-": partnev = "Független jelöltek"; break;
                }
                double arany = (double)item.Szavazat / osszesszavazat * 100;
                Console.WriteLine($"{partnev}= {arany:F2}%");
            }

            var partokszavazat2 = jeloltek.GroupBy(x => x.part)
                                         .Select(g => new { Part = g.Key, Szavazat = g.Sum(x => x.szavazatok) });

            foreach (var item in partokszavazat2)
            {
                string partnev = "";
                switch (item.Part)
                {
                    case "GYEP": partnev = "Gyümölcsevők Pártja"; break;
                    case "HEP": partnev = "Húsevők Pártja"; break;
                    case "TISZ": partnev = "Tejivók Szövetsége"; break;
                    case "ZEP": partnev = "Zöldségevők Pártja"; break;
                    case "-": partnev = "Független jelöltek"; break;
                }
                double arany = (double)item.Szavazat / osszesszavazat * 100;
                Console.WriteLine($"{partnev}= {arany:F2}%");
            }
            Console.WriteLine();

            // 6. feladat
            Console.WriteLine("6. feladat");
            int maxszavazat = jeloltek.Max(x => x.szavazatok);

            var legtobbet1 = from jelolt in jeloltek
                             where jelolt.szavazatok == maxszavazat
                             select jelolt;

            foreach (var jelolt in legtobbet1)
            {
                string partnev = jelolt.part == "-" ? "független" : jelolt.part;
                Console.WriteLine($"{jelolt.nev} {partnev}");
            }

            var legtobbet2 = jeloltek.Where(x => x.szavazatok == maxszavazat);

            foreach (var jelolt in legtobbet2)
            {
                string partnev = jelolt.part == "-" ? "független" : jelolt.part;
                Console.WriteLine($"{jelolt.nev} {partnev}");
            }
            Console.WriteLine();

            // 7. feladat

            var gyoztesek1 = from jelolt in jeloltek
                             group jelolt by jelolt.valasztokerulet into g
                             let maxSzavazat = g.Max(x => x.szavazatok)
                             select g.Where(x => x.szavazatok == maxSzavazat).First();

            using (StreamWriter sw = new StreamWriter("kepviselok.txt"))
            {
                foreach (var gyoztes in gyoztesek1.OrderBy(x => x.valasztokerulet))
                {
                    string partnev = gyoztes.part == "-" ? "független" : gyoztes.part;
                    sw.WriteLine($"{gyoztes.valasztokerulet} {gyoztes.nev} {partnev}");
                }
            }

            var gyoztesek2 = jeloltek.GroupBy(x => x.valasztokerulet)
                                    .Select(g => g.OrderByDescending(x => x.szavazatok).First());

            using (StreamWriter sw = new StreamWriter("kepviselok2.txt"))
            {
                foreach (var gyoztes in gyoztesek2.OrderBy(x => x.valasztokerulet))
                {
                    string partnev = gyoztes.part == "-" ? "független" : gyoztes.part;
                    sw.WriteLine($"{gyoztes.valasztokerulet} {gyoztes.nev} {partnev}");
                }
            }


            Console.ReadKey();
        }
    }
}