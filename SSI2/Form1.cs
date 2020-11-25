using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace SSI2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();


        }

        void wykres_lamane_rysuj(List<double> wartosci_x, List<double> wartosci_y)
        {
            wykresy.Series.Add("lamane");
            var seria_akt = wykresy.Series.Last();
            for(int i = 0; i < wartosci_x.Count; i++)
            {
                seria_akt.Points.AddXY(wartosci_x[i], wartosci_y[i]);
            }
            seria_akt.ChartType = SeriesChartType.Line;
            seria_akt.Color = Color.Red;
            seria_akt.BorderWidth = 3;
            seria_akt.BorderDashStyle = ChartDashStyle.Solid;
        }

        void wykres_punkty_rysuj(List<double> wartosci_x, List<double> wartosci_y)
        {
            wykresy.Series.Add("punkty");
            var seria_akt = wykresy.Series.Last();
            seria_akt.ChartType = SeriesChartType.Point;
            for(int i = 0; i < wartosci_x.Count; i++)
            {
                seria_akt.Points.AddXY(wartosci_x[i], wartosci_y[i]);
            }
            seria_akt.Color = Color.Blue;
            seria_akt.MarkerSize = 13;
            seria_akt.MarkerStyle = MarkerStyle.Diamond;
        }

        void wykres_sinus_rysuj(List<double> wartosci_x, List<double> wartosci_y)
        {
            wykresy.Series.Add("sinus");
            var seria_akt = wykresy.Series.Last();
            for(int i = 0; i < wartosci_x.Count; i++)
            {
                seria_akt.Points.AddXY(wartosci_x[i], wartosci_y[i]);
            }
            seria_akt.ChartType = SeriesChartType.Spline;
            seria_akt.Color = Color.Yellow;
            seria_akt.BorderWidth = 2;
            seria_akt.BorderDashStyle = ChartDashStyle.Solid;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            wykres_czysc();

            var chartArea = new ChartArea("Wykresy");
            wykresy.ChartAreas.Add(chartArea);

            wykresy.Legends.Add(new Legend());

            var wartosci_x = new List<double>();
            var wartosci_y = new List<double>();

            //rysowanie koła
            for(int i = 0; i <= 16; i++)
            {
                double x = 2 * Math.Cos(i * 2 * Math.PI / 16);
                double y = 2 * Math.Sin(i * 2 * Math.PI / 16);
                wartosci_x.Add(x);
                wartosci_y.Add(y);
            }

            wykres_lamane_rysuj(wartosci_x, wartosci_y);


            wartosci_x = new List<double>();
            wartosci_y = new List<double>();

            wartosci_x.Add(-1);
            wartosci_y.Add(1);
            wartosci_x.Add(0);
            wartosci_y.Add(0);
            wartosci_x.Add(1);
            wartosci_y.Add(1);
            wykres_punkty_rysuj(wartosci_x, wartosci_y);


            wartosci_x = new List<double>();
            wartosci_y = new List<double>();

            for(double x = -1; x < 1; x += Math.PI / 720.0)
            {
                wartosci_x.Add(x);
                wartosci_y.Add(Math.Sin(x * 1.5 - Math.PI * 1 / 2));
            }

            wykres_sinus_rysuj(wartosci_x, wartosci_y);
        }

        private void wykres_czysc()
        {
            wykresy.Series.Clear();
            wykresy.ChartAreas.Clear();
            wykresy.Legends.Clear();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            wykres_czysc();

            List<(ChartArea, int, int)> chartAreas = new List<(ChartArea, int, int)>()
            {
                (new ChartArea { Name = "P1"}, 2, 3),
                (new ChartArea { Name = "P2"}, 1, 3),
                (new ChartArea { Name = "P3"}, 0, 3),
                (new ChartArea { Name = "P4"}, 1, 2)
            };

            List<List<string>> classes = new List<List<string>>
                {
                    new List<string>(),
                    new List<string>(),
                    new List<string>()
                };
            foreach(string line in File.ReadLines(@"C:\test\iris.txt"))
            {
                var values = line.Split();
                classes[int.Parse(values.Last()) - 1].Add(line);
            }

            foreach(var chartArea in chartAreas)
            {
                wykresy.ChartAreas.Add(chartArea.Item1);


                Legend legend = new Legend
                {
                    Title = $"Oś X - atrybut {chartArea.Item2 + 1}, oś Y - atrybut {chartArea.Item3 + 1}",
                    Name = "Legend" + chartArea.Item1.Name,
                    DockedToChartArea = chartArea.Item1.Name,
                    IsDockedInsideChartArea = false,
                    Docking = Docking.Top,
                    Alignment = StringAlignment.Center
                };

                wykresy.Legends.Add(legend);

                List<Series> series = new List<Series>
                {
                    new Series{ Color = Color.Red },
                    new Series{ Color = Color.Green },
                    new Series{ Color = Color.Blue }
                };

                for(int i = 0; i < 3; i++)
                {
                    series[i].ChartArea = chartArea.Item1.Name;
                    series[i].Name = $"{chartArea.Item1.Name} - Klasa {i + 1}";
                    series[i].Legend = legend.Name;
                    foreach(var line in classes[i])
                    {
                        var values = line.Replace(".", ",").Split();
                        var x = double.Parse(values[chartArea.Item2]);
                        var y = double.Parse(values[chartArea.Item3]);
                        series[i].Points.AddXY(x, y);
                    }
                }
                foreach(var serie in series)
                {
                    serie.ChartType = SeriesChartType.Point;
                    serie.MarkerSize = 8;
                    serie.MarkerStyle = MarkerStyle.Circle;
                    wykresy.Series.Add(serie);
                }
            }
        }
    }
}
