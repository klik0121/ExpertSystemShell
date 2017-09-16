using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ExpertSystemShell.KnowledgeBases.ProductionModel;
using ParkingConsole;

namespace ParkingDemo
{
    public partial class DemoForm : Form
    {
        protected ParkingTask task;
        protected Graphics g;
        protected ParkingZoneState drawingState;

        public DemoForm()
        {
            InitializeComponent();
            g = picture.CreateGraphics();
            InitializePakingTask(true);
            drawingState = new ParkingZoneState();
            foreach (var item in Enum.GetValues(typeof (DefuzzificationMethod)))
                methodComboBox.Items.Add(item);
        }

        private void GetAngle(object sender, EventArgs e)
        {
            g.ResetTransform();
            g = picture.CreateGraphics();
            g.SmoothingMode = SmoothingMode.HighQuality;
            drawingState.X = (double)Xnum.Value;
            drawingState.Fi = (double) FInum.Value;
            drawingState.Y = (double) YNum.Value;
            DefuzzificationMethod method = DefuzzificationMethod.GravityCentre;
            if (methodComboBox.SelectedIndex >= 0)
                method = (DefuzzificationMethod)Enum.Parse(typeof (DefuzzificationMethod),
                    methodComboBox.SelectedItem.ToString());
            Stopwatch sw = new Stopwatch();
            sw.Start();
            double teta = task.GetAngle(drawingState.X, drawingState.Fi, method);
            Tetanum.Value = double.IsNaN(teta)? 0: (decimal)teta;
            drawingState.Teta = (double)Tetanum.Value;
            sw.Stop();
            label7.Text = sw.ElapsedMilliseconds.ToString();
            g.Clear(Color.White);
            drawingState.Draw(g);
        }

        private void InitializePakingTask(bool linear)
        {
            task = new ParkingTask(linear);
            task.AddRule(0, 0, 6, 0.80);
            task.AddRule(0, 1, 6, 0.80);
            task.AddRule(0, 3, 0, 0.80);
            task.AddRule(0, 4, 0, 0.80);
            task.AddRule(0, 5, 4, 0.80);
            task.AddRule(0, 6, 4, 0.80);
            task.AddRule(1, 0, 6, 0.80);
            task.AddRule(1, 1, 2, 0.80);
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
            task.AddRule(4, 2, 6, 0.80);
            task.AddRule(4, 3, 6, 0.80);
            task.AddRule(4, 5, 0, 0.80);
            task.AddRule(4, 6, 0, 0.80);
        }

        private void MethodChanged(object sender, EventArgs e)
        {
            if (funcTypeComboBox.SelectedIndex == 0)
                InitializePakingTask(true);
            else InitializePakingTask(false);
        }
    }
}
