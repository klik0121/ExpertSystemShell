using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MyParallel;
using System.Diagnostics;
using System.Threading;
using System.Windows.Forms.DataVisualization.Charting;


namespace ParallelSumTest
{
    public partial class ParallelSumTestForm : Form
    {
        protected PictureBox pb;
        protected List<KeyValuePair<string, Action<int>>> methods;
        protected List<Color> colors;
        protected BackgroundWorker bw = new BackgroundWorker();
        protected Stopwatch sw = new Stopwatch();
        protected List<List<DataPoint>> results;
        protected double[] testArray;
        protected AutoResetEvent doneEvent = new AutoResetEvent(false);
        protected int[] args;

        public ParallelSumTestForm()
        {
            InitializeComponent();

            pb = new PictureBox();
            pb.Size = new Size(64, 64);
            pb.Left = mainChart.Width / 2 - 32;
            pb.Top = mainChart.Height / 2 - 32;
            pb.Image = Properties.Resources.Loading;
            mainChart.SizeChanged += ResizeProgressBar;

            bw.DoWork += RunTests;
            bw.RunWorkerCompleted += UpdateData;

            methods = new List<KeyValuePair<string, Action<int>>>();
            methods.Add(new KeyValuePair<string, Action<int>>("Последовательный алгоритм", (a) => { testArray.Sum(); }));
            methods.Add(new KeyValuePair<string, Action<int>>("Пирамидальный алгоритм", (a) => { testArray.PyramidSum(a); }));
            methods.Add(new KeyValuePair<string, Action<int>>("Сегментный алгоритм", (a) => { testArray.SegmentSum(a); }));
            methods.Add(new KeyValuePair<string, Action<int>>("Шаговый алгоритм", (a) => { testArray.SegmentStepSum(a); }));

            colors = new List<Color>();
            colors.Add(Color.Red);
            colors.Add(Color.Blue);
            colors.Add(Color.Green);
            colors.Add(Color.DarkViolet);

            mainChart.ChartAreas[0].AxisX.Title = "Размерность массива";
            mainChart.ChartAreas[0].AxisY.Title = "Время (мс)";
            mainChart.ChartAreas[0].AxisX.IsStartedFromZero = false;
            mainChart.ChartAreas[0].AxisX.IntervalAutoMode = IntervalAutoMode.VariableCount;
        }

        void UpdateData(object sender, RunWorkerCompletedEventArgs e)
        {
            for (int i = 0; i < colors.Count; i++)
            {
                Series series = new Series();
                foreach (DataPoint p in results[i])
                    series.Points.Add(p);
                series.Name = methods[i].Key;
                series.Color = colors[i];
                series.ChartType = SeriesChartType.Line;
                series.BorderWidth = 3;
                mainChart.Series.Add(series);
            }
            mainChart.ChartAreas[0].AxisX.IsLogarithmic = true;
            mainChart.Controls.Remove(pb);
        }

        /// <summary>
        /// Resizes the progress bar.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        void ResizeProgressBar(object sender, EventArgs e)
        {
            pb.Left = mainChart.Width / 2 - 32;
            pb.Top = mainChart.Height / 2 - 32;
        }
        /// <summary>
        /// Creates the random array.
        /// </summary>
        /// <param name="size">The size.</param>
        void CreateRandomArray(int size)
        {
            testArray = new double[size];
            Random rnd = new Random();
            for (int i = 0; i < size; i++)
                testArray[i] = rnd.Next(20);
        }

        private void buttonTest_Click(object sender, EventArgs e)
        {
            doneEvent = new AutoResetEvent(false);
            
            args = new int[4];
            args[1] = int.Parse(textBoxLimit.Text);
            args[2] = int.Parse(textBoxLenght.Text);
            args[3] = int.Parse(textBoxStep.Text);
            mainChart.ChartAreas[0].AxisX.IsLogarithmic = false;
            mainChart.Series.Clear();
            mainChart.Controls.Add(pb);
            bw.RunWorkerAsync();          

        }
        void RunTests(object sender, DoWorkEventArgs e)
        {
            try
            {
                    results = new List<List<DataPoint>>();
                    for (int i = 0; i < methods.Count; i++ )
                    {
                        results.Add(new List<DataPoint>());
                    }
                    for (int j = 1000; j <= 100000000; j *= 2)
                    {
                        CreateRandomArray(j);
                        for (int i = 0; i < methods.Count; i++)
                        {
                            sw.Reset();
                            sw.Start();
                            methods[i].Value.Invoke(args[i]);
                            sw.Stop();
                            results[i].Add(new DataPoint(j, sw.ElapsedMilliseconds + 1));
                        }
                    }
            }
            finally
            {
                doneEvent.Set();
            }
        }

    }
}
