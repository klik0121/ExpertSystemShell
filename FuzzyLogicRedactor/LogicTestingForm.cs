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
using System.Windows.Forms.DataVisualization.Charting.Data;
using System.Windows.Forms.DataVisualization.Charting;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace FuzzyLogicRedactor
{
    public partial class LogicTestingForm : Form
    {
        protected BindingList<FuzzyVariable> facts1;
        protected BindingList<FuzzyVariable> facts2;
        protected FuzzyVariable first;
        protected FuzzyVariable second;
        protected BindingList<OperationType> types;
        protected BindingList<string> operations;
        protected FuzzyFactCreationForm form;

        public LogicTestingForm()
        {
            InitializeComponent();
            form = new FuzzyFactCreationForm();
            facts1 = new BindingList<FuzzyVariable>();
            facts2 = new BindingList<FuzzyVariable>();

            //facts.RaiseListChangedEvents = true;

            firstName.DataSource = facts1;
            secondName.DataSource = facts2;
            firstName.DisplayMember = secondName.DisplayMember = "Name";
            firstName.ValueMember = secondName.ValueMember = "Name";

            firstValue.DisplayMember = secondValue.DisplayMember = "Key";
            firstValue.ValueMember = secondValue.ValueMember = "Key";
            
            types = new BindingList<OperationType>();
            foreach (var value in Enum.GetValues(typeof(OperationType)))
                types.Add((OperationType)value);
            operationType.DataSource = types;
            operations = new BindingList<string>();
            operations.Add("И");
            operations.Add("НЕ");
            operations.Add("ИЛИ");
            operation.DataSource = operations;

            chart.ChartAreas[0].AxisY.Minimum = -1;
            chart.ChartAreas[0].AxisY.Maximum = 2;
            chart.ChartAreas[0].AxisY.Interval = 1;
        }

        private void CreateVariable(object sender, EventArgs e)
        {
            form = new FuzzyFactCreationForm();
            form.ShowDialog();
            this.facts1.Add((FuzzyVariable)form.CurrentFact.Clone());
            this.facts2.Add((FuzzyVariable)form.CurrentFact.Clone());
            this.firstName.Update();
        }

        private void DisplayOperation(object sender, EventArgs e)
        {
            chart.Series.Clear();
            OperationType type = (OperationType)operationType.SelectedItem;
            string op = (string)operation.SelectedItem;
            FuzzyVariable fact1 = (FuzzyVariable)firstName.SelectedItem;
            fact1.Value = (string)firstValue.SelectedItem;
            FuzzyVariable fact2 = (FuzzyVariable)secondName.SelectedItem;
            fact2.Value = (string)secondValue.SelectedItem;
            chart.ChartAreas[0].AxisX.Minimum = Math.Min(fact1.Min, fact2.Min);
            chart.ChartAreas[0].AxisX.Maximum = Math.Max(fact1.Max, fact2.Max);
            Func<double, double> fact1Function = (x) => { fact1.NumericValue = x; return fact1.GetConfidence(); };
            Func<double, double> fact2Function = (x) => { fact2.NumericValue = x; return fact2.GetConfidence(); };
            DisplayFunction(fact1.Value, fact1Function, Color.Red, true);
            DisplayFunction(fact2.Value, fact2Function, Color.Blue, true);
            switch(op)
            {
                case("И"):
                    string name = string.Format("{0} ИЛ {1}", fact1.Value, fact2.Value);
                    Func<double, double> and = (x) => 
                    {
                        fact1.NumericValue = x; fact2.NumericValue = x;
                        return fact1.Intersection(fact2, type);
                    };
                    DisplayFunction(name, and, Color.Green, false);
                    break;
                case("ИЛИ"):
                    name = string.Format("{0} ИЛИ {1}", fact1.Value, fact2.Value);
                    Func<double, double> or = (x) => 
                    {
                        fact1.NumericValue = x; fact2.NumericValue = x;
                        return fact1.Union(fact2, type);
                    };
                    DisplayFunction(name, or, Color.Green, false);
                    break;
                case("НЕ"):
                    name = string.Format("НЕ {0}", fact1.Value);
                    Func<double, double> func = (x) => { fact1.NumericValue = x; return fact1.Complement(); };
                    DisplayFunction(name, func, Color.Red, false);
                    name = string.Format("НЕ {0}", fact2.Value);
                    func = (x) => { fact2.NumericValue = x; return fact2.Complement(); };
                    DisplayFunction(name, func, Color.Blue, false);
                    break;
            }
        }

        private void DisplayFunction(string name, Func<double, double> function, Color color, 
            bool dotted)
        {
            Series series = new Series();
            series.Name = name;
            series.ChartType = SeriesChartType.Line;
            series.Color = color;
            double from = chart.ChartAreas[0].AxisX.Minimum;
            double to = chart.ChartAreas[0].AxisX.Maximum;
            double step = (to - from) / 100;
            double currX = from;
            while(currX <= to)
            {
                series.Points.AddXY(currX, function(currX));
                currX += step;
            }
            if(dotted)
            {
                series.BorderDashStyle = ChartDashStyle.Dash;
            }
            series.BorderWidth = 2;
            chart.Series.Add(series);
        }

        private void FirstValueChanged(object sender, EventArgs e)
        {
            if(first != null)
            {
                first.Value = (string)firstValue.SelectedItem;
            }
        }

        private void FirstNameChanged(object sender, EventArgs e)
        {
            this.first = (FuzzyVariable)firstName.SelectedItem;
            if(first != null)
            {
                firstValue.DataSource = new BindingList<string>(first.Labels);
            }
        }

        private void SecondNameChanged(object sender, EventArgs e)
        {
            this.second = (FuzzyVariable)secondName.SelectedItem;
            if(second != null)
            {
                secondValue.DataSource = ((FuzzyVariable)secondName.SelectedItem).Labels;
            }
        }

        private void SecondValueChanged(object sender, EventArgs e)
        {
            if (second != null)
            {
                second.Value = (string)secondValue.SelectedItem;
            }
        }

        private void LogicTestingFormClosing(object sender, FormClosingEventArgs e)
        {

        }
    }
}
