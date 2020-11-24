using System;
using System.Collections.Generic;
using System.IO;

namespace SSI1
{
    class Program
    {
        static void Main(string[] args)
        {
            var probki = new List<List<string>>();
            var czy_atr_symb = new List<bool>();
            var nazwy_atr = new List<string>();
            wczytaj_baze_probek_z_tekstem(@"C:\test\probki.txt", @"C:\test\atrybuty.txt"
                ,out probki, out czy_atr_symb, out nazwy_atr);
            Console.ReadLine();
        }

        static void wczytaj_baze_probek_z_tekstem(
            string nazwa_pliku_z_wartosciami, string nazwa_pliku_z_opisem_atr,
            out List<List<string>> probki, out List<bool> czy_atr_symb, out List<string> nazwy_atr)
        {
            probki = new List<List<string>>();
            czy_atr_symb = new List<bool>();
            nazwy_atr = new List<string>();


            //próbki
            var plik_z_wartosciami = File.ReadAllLines(nazwa_pliku_z_wartosciami);

            foreach (var line in plik_z_wartosciami)
            {
                var wiersz_probki = new List<string>();
                foreach (var cell in line.Split(' ' , '\t'))
                {
                    if(string.IsNullOrWhiteSpace(cell))
                        continue;
                    wiersz_probki.Add(cell);
                }

                probki.Add(wiersz_probki);
            }

            //atrybuty
            var plik_z_atrybutami = File.ReadAllLines(nazwa_pliku_z_opisem_atr);

            foreach (var line in plik_z_atrybutami)
            {
                foreach (var cell in line.Split(' ', '\t'))
                {
                    if(string.IsNullOrWhiteSpace(cell))
                        continue;
                    if (cell.Length == 1 && cell == "s")
                    {
                        czy_atr_symb.Add(true);
                    }
                    else if (cell.Length == 1 && cell != "s")
                    {
                        czy_atr_symb.Add(false);
                    }
                    else
                    {
                        nazwy_atr.Add(cell);
                    }
                }
            }
        }
    }
}
