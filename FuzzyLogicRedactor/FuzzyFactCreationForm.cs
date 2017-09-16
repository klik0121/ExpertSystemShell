using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ExpertSystemShell.KnowledgeBases.ProductionModel;
using System.Windows.Forms.DataVisualization.Charting;
using System.Windows.Forms.DataVisualization.Charting.Data;
using Microsoft.VisualBasic;
using MathNet.Numerics.Interpolation;

namespace FuzzyLogicRedactor
{
    public partial class FuzzyFactCreationForm : Form
    {
        protected FuzzyVariable currentFact;
        protected BindingList<string> labels;
        protected List<Func<double, double>> functions;
        protected List<List<DataPoint>> points;
        protected int currMin = 0;
        protected int currMax = 35;
        protected Random rnd = new Random();

        public FuzzyVariable CurrentFact
        {
            get { return currentFact; }
        }   

        public FuzzyFactCreationForm()
        {
            InitializeComponent();
            InitializeNewFact();
            this.listBoxLabels.DataSource = labels;
            this.chart.ChartAreas[0].AxisY.Maximum = 2;
            this.chart.ChartAreas[0].AxisY.Minimum = 0;
            this.listBoxLabels.SelectionMode = SelectionMode.MultiExtended;
            this.chart.ChartAreas[0].AxisY.Interval = 1;
            this.rbLinear.Checked = true;
            this.numMax.Minimum = decimal.MinValue;
            this.numMax.Minimum = decimal.MinValue;
            this.numMax.Maximum= decimal.MaxValue;
            this.numMax.Maximum = decimal.MaxValue;
        }

        public void InitializeNewFact()
        {
            labels = new BindingList<string>();
            functions = new List<Func<double, double>>();
            points = new List<List<DataPoint>>();
            this.currMax = 35;
            this.currMin = 0;
        }
        private void CreateFact(object sender, EventArgs e)
        {
            string name = Interaction.InputBox("Введите имя переменной",
                "Имя переменной", "Имя переменной");            
            currentFact = new FuzzyVariable(name, "", currMin, currMax);
            for(int i = 0; i < labels.Count; i++)
            {
                currentFact.AddSet(labels[i], functions[i]);
            }
            this.Hide();
        }
        private void AddLabel(object sender, EventArgs e)
        {
            string label = this.textBoxNewLabel.Text;
            if(labels.Contains(label))
            {
                ShowMessage(string.Format("Список уже содержит значение {0}.", label));
            }
            else
            {
                labels.Add(label);
                //this.listBoxLabels.Update();
                functions.Add((x) => { return 1; });
                points.Add(new List<DataPoint>());
                listBoxLabels.ClearSelected();
                listBoxLabels.SelectedIndex = labels.Count - 1;
            }
            
        }
        public void ShowMessage(string message)
        {
            MessageBox.Show(message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        protected void DisplayFunction(int index)
        {
            Color randomColor = RandomColor.Get();
            if (chart.Series.Count((s) => { return s.Color == randomColor; }) > 0)
                DisplayFunction(index);
            else
            {
                Series series = new Series();
                series.Name = labels[index];
                double step = (currMin + currMax) / 100;
                this.chart.ChartAreas[0].AxisX.Interval = step * 10;
                this.chart.ChartAreas[0].AxisX.Minimum = currMin;
                this.chart.ChartAreas[0].AxisX.Maximum = currMax;
                for(double x = currMin; x <= currMax; x++)
                {
                    series.Points.AddXY(x, functions[index](x));
                }
                series.ChartType = SeriesChartType.Line;
                chart.Series.Add(series);
            }
            this.chart.Update();
        }
        protected void ClearChart()
        {
            chart.Series.Clear();
            chart.Update();
        }
        private void SelectedIndexChanged(object sender, EventArgs e)
        {
            ClearChart();
            foreach(var index in listBoxLabels.SelectedIndices)
            {
                DisplayFunction((int)index);
            }
        }
        private void SetValue(int index, double x, double y)
        {
            List<DataPoint> currPoints = points[index];
            if(currPoints.Count == 0)
            {
                currPoints.Add(new DataPoint(currMin, 0));
                currPoints.Add(new DataPoint(currMax, 0));
            }
            for (int i = 0; i < currPoints.Count; i++)
            {
                var dataPoint = currPoints[i];
                if (x == dataPoint.XValue)
                {
                    dataPoint.XValue = x;
                    dataPoint.YValues[0] = y;
                    break;
                }
                if (x < dataPoint.XValue)
                {
                    currPoints.Insert(i, new DataPoint(x, y));
                    break;
                }
            }
            if(rbLinear.Checked)
            {
                functions[index] = CreateLinear(index);
            }
            else
            {
                functions[index] = CreatePolynom(index);
            }

        }
        protected Func<double, double> CreateLinear(int index)
        {
            if (points[index].Count == 0)
            {
                return (x) => { return 1; };
            }
            DataPoint[] currPoints = points[index].ToArray();
            return (x) => 
            {
                if (x < currMin || x > currMax)
                    return 0;
                for (int i = 0; i < (currPoints.Length - 1); i++)
                {
                    //точка лежит в границах
                    if(x >= currPoints[i].XValue && x <= currPoints[i + 1].XValue)
                    {
                        //построим уравнение прямой и вернём результат
                        double x1 = currPoints[i].XValue;
                        double y1 = currPoints[i].YValues[0];
                        double x2 = currPoints[i + 1].XValue;
                        double y2 = currPoints[i + 1].YValues[0];
                        return y2 + (x - x2) * (y2 - y1) / (x2 - x1);
                    }
                }
                return x;
            };
        }
        protected Func<double, double> CreatePolynom(int index)
        {
            if(points[index].Count == 0)
            {
                points[index].Add(new DataPoint(currMin, 1));
                points[index].Add(new DataPoint(currMax, 1));
            }
            DataPoint[] currPoints = points[index].ToArray();
            double[,] coeffs = new double[currPoints.Length, 2];
            List<double> xv = new List<double>();
            List<double> yv = new List<double>();
            Func<double, double> linear = CreateLinear(index);
            for (int i = 0; i < currPoints.Length; i++)
            {
                xv.Add(currPoints[i].XValue);
                yv.Add(currPoints[i].YValues[0]);
                coeffs[i, 0] = currPoints[i].XValue;
                coeffs[i, 1] = currPoints[i].YValues[0];
            }
            Random rnd = new Random();
            while(xv.Count < 5)
            {
                int i = rnd.Next(xv.Count - 1);
                int j = i + 1;
                double x = (xv[i] + xv[j]) / 2;
                yv.Insert(j, linear(x));
                xv.Insert(j, x);
            }
            var akima = CubicSpline.InterpolateAkima(xv, yv);
            return (x) =>
            {
                double res = akima.Interpolate(x);
                if (res < 0)
                    return 0;
                if (res > 1)
                    return 1;
                return res;
            };
        }
        private void SetPoint(object sender, EventArgs e)
        {
            ClearChart();
            double x = (double)numX.Value;
            double y = (double)numY.Value;
            foreach(var index in this.listBoxLabels.SelectedIndices)
            {
                SetValue((int)index, x, y);
                DisplayFunction((int)index);
            }
        }
        private void ToDefault(object sender, EventArgs e)
        {
            ClearChart();
            foreach (var index in this.listBoxLabels.SelectedIndices)
            {
                points[(int)index].Clear();
                functions[(int)index] = (x) => { return 1; }; 
                DisplayFunction((int)index);
            }
        }
        private void CheckedChanged(object sender, EventArgs e)
        {
            ClearChart();
            foreach(var index in this.listBoxLabels.SelectedIndices)
            {
                int i = (int)index;
                if(this.rbLinear.Checked)
                {
                    functions[i] = CreateLinear(i);
                }
                else
                {
                    functions[i] = CreatePolynom(i);
                }
                DisplayFunction(i);
            }
        }
        private void MinValueChanged(object sender, EventArgs e)
        {
            ClearChart();
            this.currMin = (int)this.numMin.Value;
            this.chart.ChartAreas[0].AxisX.Minimum = currMin;
            this.numX.Minimum = currMin;
            for(int i = 0; i < functions.Count; i++)
            {
                points[i] = points[i].Where((point) => { return point.XValue >= currMin; }).ToList();
                if (points[i][0].XValue != currMin)
                    SetValue(i, currMin, functions[i](currMin));
                DisplayFunction(i);
            }
        }
        private void MaxValueChanged(object sender, EventArgs e)
        {
            ClearChart();
            this.currMax = (int)this.numMax.Value;
            this.chart.ChartAreas[0].AxisX.Maximum = currMax;
            this.numX.Maximum = currMax;
            for (int i = 0; i < functions.Count; i++)
            {
                points[i] = points[i].Where((point) => { return point.XValue <= currMax; }).ToList();
                if (points[i][0].XValue != currMax)
                    SetValue(i, currMax, functions[i](currMax));
                DisplayFunction(i);
            }
        }
    }
}
