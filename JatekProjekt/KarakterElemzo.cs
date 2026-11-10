using JatekProjekt;
using System;
using System.Collections.Generic;
using System.Text;

namespace JatekProjekt
{

    public class KarakterElemzo
    {
        private List<Karakter> karakterek;

        // Konstruktor: példányosítja az adatbázis-kezelőt és betölti az adatokat a listába
        public KarakterElemzo()
        {
            var dbKezelo = new AdatbazisKezelo();
            this.karakterek = dbKezelo.AdatokBetoltese();
        }

        public List<string> MagasSzintuKarakterek()
        {
            return karakterek
                .Where(k => k.Szint >= 50)
                .Select(k => k.Nev)
                .ToList();
        }

        // Gyűjtsd ki az összes "Mágus" osztályú karakter nevét!
        public List<string> MagusOsztalyuNevek()
        {
            return karakterek
                .Where(k => k.Osztaly == "Mágus")
                .Select(k => k.Nev)
                .ToList();
        }


        // Add vissza az összes karakter nevét egy egyszerű listában!
        public List<string> OsszesKarakterNevListaba()
        {
            return karakterek
                .Select(k => k.Nev)
                .ToList();
        }

        // Szűrd ki azokat a karaktereket, amelyek valamelyik "EU-"-val kezdődő szerveren játszanak, és add vissza a nevüket listában!
        public List<string> EuSzerveresKarakterek()
        {
            return karakterek
                .Where(k => k.Szerver.StartsWith("EU-"))
                .Select(k => k.Nev)
                .ToList();
        }

        // Válaszd ki az összes egyedi (különböző) szervernevet, ahol a karakterek játszanak (Distinct segítségével)!
        public List<string> EgyediSzerverNevek()
        {
            return karakterek
                .Select(k => k.Szerver)
                .Distinct()
                .ToList();
        }


        // ==========================================
        // 2. EGYSZERŰ AGGREGÁCIÓK ÉS ELDÖNTÉsek
        // ==========================================

        // Számold meg Sum segítségével, hogy mennyi a karakterek összes aranya a szerveren!
        public int OsszesAranyMennyiseg()
        {
            return karakterek
                .Select(k => k.Arany)
                .Sum();

        }

        // Számítsd ki Average segítségével a karakterek átlagos szintjét double értékként!
        public double AtlagSzint()
        {
            return karakterek
                .Select(k => k.Szint)
                .Average();
        }

        // Állapítsd meg Any segítségével, hogy van-e olyan "Tolvaj" osztályú karakter, akinek a szintje eléri vagy meghaladja a 40-et!
        public bool VaneMagasSzintuTolvaj()
        {
            return karakterek
                .Any(k => k.Osztaly == "Tolvaj" && k.Szint >= 40);
        }

        // Ellenőrizd Any segítségével, hogy létezik-e olyan karakter, akinek 10 000 aranynál több van a zsebében!
        public bool VanETizEzerAranynalTobb()
        {
            return karakterek
                .Any(k => k.Arany > 10000);
        }

        // Keresd meg FirstOrDefault segítségével az első olyan karaktert, akinek az aranya meghaladja az 5000-et!
        public Karakter LegelsoOvenKarakter()
        {
            return karakterek
                .FirstOrDefault(k => k.Arany > 5000);
        }


        // ==========================================
        // 3. RENDEZÉsek ÉS KERESÉsek
        // ==========================================

        // Add vissza az összes karaktert szint szerint csökkenő, azon belül pedig név szerint növekvő sorrendbe rendezve!
        public List<Karakter> KarakterekSzintSzerintRendezve()
        {
            return karakterek
                .OrderByDescending(k => k.Szint)
                .ThenBy(k => k.Nev)
                .ToList();

        }

        // Keresd meg OrderByDescending és FirstOrDefault segítségével annak a karakternek a nevét, akinek a legtöbb aranya van!
        public string LeggazdagabbKarakterNeve()
        {
            return karakterek
                .OrderByDescending(k => k.Arany)
                .FirstOrDefault()?.Nev;
        }

        // Keresd meg OrderBy és FirstOrDefault segítségével a legszegényebb (legkevesebb aranyú) karakter nevét!
        public string LegszegenyebbKarakterNeve()
        {
            return karakterek
                .OrderBy(k => k.Arany)
                .FirstOrDefault()?.Nev;
        }

        // Keresd meg Max segítségével a legmagasabb szintű karakter szintjét!
        public int LegmagasabbSzint()
        {
            return karakterek
                .Max(k => k.Szint);
        }

        // Rendezd a karaktereket a nevük hossza szerint növekvő sorrendbe!
        public List<Karakter> KarakterekNevHosszaSzerint()
        {
            return karakterek
                .OrderBy(k => k.Nev.Length)
                .ToList();
        }


        // ==========================================
        // 4. CSOPORTOSÍTÁS ÉS ÖSSzetettebb LINQ MŰVELETek
        // ==========================================

        // Csoportosítsd a karaktereket az Osztaly tulajdonságuk alapján, és alakítsd át ToDictionary-vel úgy, hogy a kulcs az osztály neve, az érték pedig a karakterek darabszáma legyen!
        public Dictionary<string, int> OsztalyonkentKarakterSzam()
        {
            return karakterek
                .GroupBy(k => k.Osztaly)
                .ToDictionary(csoport => csoport.Key, csoport => csoport.Count());
        }

        // Csoportosítsd a karaktereket a Szerver tulajdonságuk alapján, és számold meg, hány karakter játszik az egyes szervereken!
        public Dictionary<string, int> SzerverenKarakterekSzama()
        {
            return karakterek
                .GroupBy(k => k.Szerver)
                .ToDictionary(csoport => csoport.Key, csoport => csoport.Count());
        }

        // Gyűjtsd ki az összes "Harcos" osztályú karakter azonosítóját (Id) egy listába!
        public List<int> HarcosokAzonositoi()
        {
            return karakterek
                .Where(k => k.Osztaly == "Harcos")
                .Select(k => k.Id)
                .ToList();
        }

        // Állapítsd meg, hogy van-e olyan 50-es szintnél magasabb Mágus, akinek 3000-nél kevesebb aranya van!
        public bool VanETolvajSpecKondicioval()
        {
            return karakterek
                .Any(k => k.Osztaly == "Mágus"
                && k.Szint > 50
                && k.Arany < 3000);
        }

        // Keresd meg és add vissza a legmagasabb szintű karakter objektumát!
        public Karakter LegmagasabbSzintuKarakterObjektum()
        {
            return karakterek
                .OrderByDescending(k => k.Szint)
                .FirstOrDefault();
        }

    }
}