using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.Diagnostics;
using MyParallel;
using System.IO;

namespace TestSorting
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            chartLenght.Series.Clear();
            chartLimit.Series.Clear();
            Series seqLenght = new Series();
            seqLenght.Color = Color.Red;
            seqLenght.Name = "Посл.";
            seqLenght.ChartType = SeriesChartType.Line;
            Series parLenght = new Series();
            parLenght.Color = Color.Blue;
            parLenght.Name = "Пар.";
            parLenght.ChartType = SeriesChartType.Line;
            Series seqLimit = new Series();
            seqLimit.Color = Color.Red;
            seqLimit.Name = "Оптимум";
            seqLimit.ChartType = SeriesChartType.Line;
            Series parLimit = new Series();
            parLimit.Name = "Пар.";
            parLimit.ChartType = SeriesChartType.Line;
            parLimit.Color = Color.Blue;
            chartLenght.Series.Add(seqLenght);
            chartLenght.Series.Add(parLenght);
            chartLimit.Series.Add(seqLimit);
            //chartLimit.Series.Add(parLimit);
            foreach (var series in chartLenght.Series)
                series.BorderWidth = 2;
            foreach (var series in chartLimit.Series)
                series.BorderWidth = 2;
            chartLenght.ChartAreas[0].AxisX.Title = "Размерность массива";
            chartLenght.ChartAreas[0].AxisY.Title = "Время (мс)";
            chartLimit.ChartAreas[0].AxisX.Title = "Размерность массива";
            chartLimit.ChartAreas[0].AxisY.Title = "Лимит параллельности";
        }

        public void Clear()
        {
            chartLenght.ChartAreas[0].AxisX.IsLogarithmic = false;
            chartLimit.ChartAreas[0].AxisX.IsLogarithmic = false;
            foreach(var series in chartLenght.Series)
            {
                series.Points.Clear();
            }
            foreach (var series in chartLimit.Series)
            {
                series.Points.Clear();
            }
        }

        private void buttonTest_Click(object sender, EventArgs e)
        {
            Clear();
            TestLenght();
            TestLimit();
        }

        public void TestLenght()
        {
            Stopwatch sw = new Stopwatch();
            for(int i = 10; i < 1000000; i*= 2)
            {
                int[] arr = GenerateArray(i);
                int[] copy = new int[arr.Length];
                arr.CopyTo(copy, 0);
                sw.Reset();
                sw.Start();
                ParallelSorting.MergeSort(arr);
                sw.Stop();
                chartLenght.Series[0].Points.AddXY(i, sw.ElapsedMilliseconds);
                sw.Reset();
                sw.Start();
                ParallelSorting.PMergeSort(arr, 200000);
                sw.Stop();
                chartLenght.Series[1].Points.AddXY(i, sw.ElapsedMilliseconds);
                chartLenght.Update();
            }
            chartLenght.ChartAreas[0].AxisX.IsLogarithmic = true;
            chartLenght.Update();
        }

        public void TestLimit()
        {
            Stopwatch sw = new Stopwatch();
            long min;
            int limitMin;
            using (StreamWriter stream = File.CreateText("out.scv"))
            {
                for (int i = 100; i < 3000000; i *= 2)
                {
                    //limitMin = int.MaxValue;
                    //IOneDimensionalOptimization opt = new CubicApproximation();
                    //opt.Accuracy = 20;
                    //int[] arr = GenerateArray(i);
                    //Func<double, double> func = (a) =>
                    //{
                    //    int[] copy = new int[arr.Length];
                    //    arr.CopyTo(copy, 0);
                    //    int limit = (int)a;
                    //    sw.Restart();
                    //    ParallelSorting.PMergeSort(copy, limit);
                    //    sw.Stop();
                    //    return sw.ElapsedTicks;
                    //};
                    //limitMin = (int)opt.GetMin(func, i / 20);
                    //chartLimit.Series[0].Points.AddXY(i, limitMin);
                    //stream.WriteLine(string.Format("{0}, {1}", i, limitMin));
                    //chartLimit.Update();
                }
                chartLimit.ChartAreas[0].AxisX.IsLogarithmic = true;
                chartLimit.Update();
            }
        }


        public int[] GenerateArray(int lenght)
        {
            int[] result = new int[lenght];
            Random rnd = new Random();
            for (int i = 0; i < lenght; i++)
                result[i] = rnd.Next(100000);
            return result;
        }
    }
}
