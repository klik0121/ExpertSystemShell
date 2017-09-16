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
using ParkingConsole;
using ExpertSystemShell.KnowledgeBases.ProductionModel;

namespace ParkingForms
{
    public partial class ParkingForm : Form
    {
        protected ParkingTask task;
        public ParkingForm()
        {
            InitializeComponent();
            Series s = new Series();
            s.Name = "Траектория";
            s.Color = Color.Red;
            s.ChartType = SeriesChartType.Line;
            chart.Series[0] = (s);
            task = new ParkingTask();
            task.AddRule(0, 0, 6, 0.80);
            task.AddRule(0, 1, 6, 0.80);
            task.AddRule(0, 3, 0, 0.80);
            task.AddRule(0, 4, 0, 0.80);
            task.AddRule(0, 5, 4, 0.80);
            task.AddRule(0, 6, 4, 0.80);
            task.AddRule(1, 0, 6, 0.80);
            task.AddRule(1, 1, 3, 0.80);
            task.AddRule(1, 3, 0, 0.80);
            task.AddRule(1, 4, 4, 0.80);
            task.AddRule(1, 5, 6, 0.80);
            task.AddRule(1, 6, 6, 0.80);
            task.AddRule(2, 2, 1, 0.80);
            task.AddRule(2, 3, 3, 0.80);
            task.AddRule(2, 4, 5, 0.80);
            task.AddRule(3, 0, 0, 0.80);
            task.AddRule(3, 1, 0, 0.80);
            task.AddRule(3, 2, 2, 0.80);
            task.AddRule(3, 3, 6, 0.80);
            task.AddRule(3, 5, 4, 0.80);
            task.AddRule(4, 0, 2, 0.80);
            task.AddRule(4, 1, 2, 0.80);
            task.AddRule(4, 2, 5, 0.80);
            task.AddRule(4, 3, 5, 0.80);
            task.AddRule(4, 5, 0, 0.80);
            task.AddRule(4, 6, 0, 0.80);
            chart.ChartAreas[0].AxisX.Maximum = 150;
            chart.ChartAreas[0].AxisX.Minimum = -150;
            chart.ChartAreas[0].AxisY.Maximum = 300;
            chart.ChartAreas[0].AxisY.Minimum = 0;
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            double x = double.Parse(textBoxX.Text);
            double fi = double.Parse(textBoxFi.Text)/Math.PI;
            double y = double.Parse(textBoxY.Text);
            chart.Series[0].Points.Clear();
            double teta = 0;            
            while ((x <= 150 && x >= -150) && (y >= 0 && y <= 300))
            {
                chart.Series[0].Points.AddXY(x, y);
                chart.Refresh();
                teta = task.GetAngle(x, fi * Math.PI)/Math.PI;
                x = x + Math.Sin(fi + teta) - Math.Sin(fi) * Math.Cos(teta);
                y = y - Math.Sin(fi + teta) - Math.Sin(fi) * Math.Sin(teta);
                fi = fi - Math.Asin((2 * Math.Sin(teta)) / 20);
            }
        }
    }
}
