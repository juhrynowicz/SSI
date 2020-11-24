using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace SSI3
{
    class Program
    {
        static void Main(string[] args)
        {
            var probki = wczytaj_probki(@"C:\Users\easyo\source\repos\SSI\SSI3\bin\spirala.txt");
            var atrybuty = wczytaj_atrybuty(@"C:\Users\easyo\source\repos\SSI\SSI3\bin\spirala-type.txt");
            var srodki = wygeneruj_srodki(4, probki);
            algorytm_k_srednich(probki, srodki, 100, atrybuty);
            Console.ReadLine();
        }

        private static List<string> wczytaj_atrybuty(string fileName)
        {
            var file = File.ReadAllLines(fileName);
            var atrybuty = new List<string>();
            foreach (var wierszPliku in file)
            {
                if(string.IsNullOrWhiteSpace(wierszPliku))
                    continue;

                var wiersz = Regex.Split(wierszPliku.Trim(), @"\s+", RegexOptions.IgnorePatternWhitespace);
                foreach (var atrybut in wiersz)
                {
                    atrybuty.Add(atrybut);
                }
            }

            return atrybuty;
        }

        static void algorytm_k_srednich(List<(double, double)> probki, 
                                        List<int> srodki,
                                        int iteracje, 
                                        List<string> atrybuty)
        {
            for (int iteracja = 0; iteracja < iteracje; iteracja++)
            {
                var wynik = new List<List<(double, double, int)>>();
                var atrybutyZWartosciami = new List<(string, double, double)>();
                var probkiZNajblizszymSrodkiem = new List<(double, double, int)>();
                foreach (var probka in probki)
                {
                    double d = Math.Sqrt((Math.Pow((probka.Item1 - probki[srodki[0]].Item1), 2) + Math.Pow((probka.Item2 - probki[srodki[0]].Item2), 2))); ;
                    int us = srodki[0];
                    foreach (var srodek in srodki)
                    {
                        var odleglosc = Math.Sqrt((Math.Pow((probka.Item1 - probki[srodek].Item1), 2) + Math.Pow((probka.Item2 - probki[srodek].Item2),2)));
                        if (odleglosc < d)
                        {
                            d = odleglosc;
                            us = srodek;
                        }
                    }

                    probkiZNajblizszymSrodkiem.Add((probka.Item1, probka.Item2, us));
                }

                for (int j = 0; j < srodki.Count; j++)
                {
                    var Xgr = new List<(double, double, int)>();
                    foreach (var probka in probkiZNajblizszymSrodkiem)
                    {
                        if (srodki[j] == probka.Item3)
                            Xgr.Add(probka);
                    }

                    if(Xgr.Count == 0)
                        continue;

                    wynik.Add(Xgr);
                }

                for (int i = 0; i < atrybuty.Count; i++)
                {
                    var atrybutZWartoscia = (atrybuty[i], wynik[i].Select(p => p.Item1).Average(),
                        wynik[i].Select(p => p.Item2).Average());
                    atrybutyZWartosciami.Add(atrybutZWartoscia);
                }

                Wyswietl_dane(probki, wynik, srodki, atrybutyZWartosciami);

            }

        }

        private static void Wyswietl_dane(List<(double, double)> probki, 
            List<List<(double, double, int)>> wynik, 
            List<int> srodki, 
            List<(string, double, double)> atrybutyZWartosciami)
        {
            foreach (var Xgr in wynik)
            {
                Console.WriteLine($"Środek: ({probki[Xgr.First().Item3].Item1},{probki[Xgr.First().Item3].Item2})");
                Console.Write("Probki: ");
                foreach (var probkiWGrupie in Xgr)
                {
                    Console.Write($"({probkiWGrupie.Item1},{probkiWGrupie.Item2}) ");
                }

                Console.WriteLine();
            }

            foreach (var atrybut in atrybutyZWartosciami)
            {
                Console.WriteLine($"Atrybut {atrybut.Item1} = ({atrybut.Item2},{atrybut.Item3})");
            }
        }

        static List<(double, double)> wczytaj_probki(string fileName)
        {
            var file = File.ReadAllLines(fileName);
            var probki = new List<(double, double)>();
            foreach (var wierszPliku in file)
            {
                if (string.IsNullOrWhiteSpace(wierszPliku))
                    continue;

                var wiersz = Regex.Split(wierszPliku.Trim(), @"\s+", RegexOptions.IgnorePatternWhitespace);
                var x = double.Parse(wiersz[0].Replace('.', ','));
                var y = double.Parse(wiersz[1].Replace('.', ','));

                probki.Add((x, y));
            }

            return probki;
        }

        static List<int> wygeneruj_srodki(int k, List<(double, double)> probki)
        {
            var srodki = new List<int>();
            var rand = new Random();
            for (int i = 0; i < k; i++)
            {
                int indeks;
                do
                {
                    indeks = rand.Next(0, probki.Count - 1);
                } while (srodki.Contains(indeks));

                srodki.Add(indeks);
            }

            return srodki;
        }
    }
}
