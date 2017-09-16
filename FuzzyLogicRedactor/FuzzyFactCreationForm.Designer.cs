namespace FuzzyLogicRedactor
{
    partial class FuzzyFactCreationForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.mainTable = new System.Windows.Forms.TableLayoutPanel();
            this.returnFactBtn = new System.Windows.Forms.Button();
            this.subTable = new System.Windows.Forms.TableLayoutPanel();
            this.groupBoxMedium = new System.Windows.Forms.GroupBox();
            this.numMax = new System.Windows.Forms.NumericUpDown();
            this.numMin = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.table = new System.Windows.Forms.TableLayoutPanel();
            this.groupBoxLabels = new System.Windows.Forms.GroupBox();
            this.buttonAdd = new System.Windows.Forms.Button();
            this.textBoxNewLabel = new System.Windows.Forms.TextBox();
            this.listBoxLabels = new System.Windows.Forms.ListBox();
            this.groupBoxFunction = new System.Windows.Forms.GroupBox();
            this.numY = new System.Windows.Forms.NumericUpDown();
            this.numX = new System.Windows.Forms.NumericUpDown();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnSetPoint = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.rbPolynom = new System.Windows.Forms.RadioButton();
            this.rbLinear = new System.Windows.Forms.RadioButton();
            this.chart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.mainTable.SuspendLayout();
            this.subTable.SuspendLayout();
            this.groupBoxMedium.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numMax)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMin)).BeginInit();
            this.table.SuspendLayout();
            this.groupBoxLabels.SuspendLayout();
            this.groupBoxFunction.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart)).BeginInit();
            this.SuspendLayout();
            // 
            // mainTable
            // 
            this.mainTable.ColumnCount = 1;
            this.mainTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.mainTable.Controls.Add(this.returnFactBtn, 0, 1);
            this.mainTable.Controls.Add(this.subTable, 0, 0);
            this.mainTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainTable.Location = new System.Drawing.Point(0, 0);
            this.mainTable.Name = "mainTable";
            this.mainTable.RowCount = 2;
            this.mainTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 90F));
            this.mainTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.mainTable.Size = new System.Drawing.Size(609, 300);
            this.mainTable.TabIndex = 0;
            // 
            // returnFactBtn
            // 
            this.returnFactBtn.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.returnFactBtn.Location = new System.Drawing.Point(267, 273);
            this.returnFactBtn.Name = "returnFactBtn";
            this.returnFactBtn.Size = new System.Drawing.Size(75, 23);
            this.returnFactBtn.TabIndex = 0;
            this.returnFactBtn.Text = "ОК";
            this.returnFactBtn.UseVisualStyleBackColor = true;
            this.returnFactBtn.Click += new System.EventHandler(this.CreateFact);
            // 
            // subTable
            // 
            this.subTable.ColumnCount = 1;
            this.subTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.subTable.Controls.Add(this.groupBoxMedium, 0, 0);
            this.subTable.Controls.Add(this.table, 0, 1);
            this.subTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.subTable.Location = new System.Drawing.Point(3, 3);
            this.subTable.Name = "subTable";
            this.subTable.RowCount = 2;
            this.subTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 75F));
            this.subTable.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.subTable.Size = new System.Drawing.Size(603, 264);
            this.subTable.TabIndex = 1;
            // 
            // groupBoxMedium
            // 
            this.groupBoxMedium.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxMedium.Controls.Add(this.numMax);
            this.groupBoxMedium.Controls.Add(this.numMin);
            this.groupBoxMedium.Controls.Add(this.label2);
            this.groupBoxMedium.Controls.Add(this.label1);
            this.groupBoxMedium.Location = new System.Drawing.Point(3, 3);
            this.groupBoxMedium.Name = "groupBoxMedium";
            this.groupBoxMedium.Size = new System.Drawing.Size(597, 69);
            this.groupBoxMedium.TabIndex = 0;
            this.groupBoxMedium.TabStop = false;
            this.groupBoxMedium.Text = "Универсальное множество";
            // 
            // numMax
            // 
            this.numMax.Location = new System.Drawing.Point(39, 42);
            this.numMax.Maximum = new decimal(new int[] {
            10000000,
            0,
            0,
            0});
            this.numMax.Minimum = new decimal(new int[] {
            10000000,
            0,
            0,
            -2147483648});
            this.numMax.Name = "numMax";
            this.numMax.Size = new System.Drawing.Size(93, 20);
            this.numMax.TabIndex = 3;
            this.numMax.Value = new decimal(new int[] {
            10000000,
            0,
            0,
            -2147483648});
            this.numMax.ValueChanged += new System.EventHandler(this.MaxValueChanged);
            // 
            // numMin
            // 
            this.numMin.Location = new System.Drawing.Point(39, 17);
            this.numMin.Maximum = new decimal(new int[] {
            10000000,
            0,
            0,
            0});
            this.numMin.Minimum = new decimal(new int[] {
            1000000,
            0,
            0,
            -2147483648});
            this.numMin.Name = "numMin";
            this.numMin.Size = new System.Drawing.Size(93, 20);
            this.numMin.TabIndex = 2;
            this.numMin.ValueChanged += new System.EventHandler(this.MinValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 44);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(28, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "До: ";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(26, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "От: ";
            // 
            // table
            // 
            this.table.ColumnCount = 2;
            this.table.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.table.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 60F));
            this.table.Controls.Add(this.groupBoxLabels, 0, 0);
            this.table.Controls.Add(this.groupBoxFunction, 1, 0);
            this.table.Dock = System.Windows.Forms.DockStyle.Fill;
            this.table.Location = new System.Drawing.Point(3, 78);
            this.table.Name = "table";
            this.table.RowCount = 1;
            this.table.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.table.Size = new System.Drawing.Size(597, 183);
            this.table.TabIndex = 1;
            // 
            // groupBoxLabels
            // 
            this.groupBoxLabels.Controls.Add(this.buttonAdd);
            this.groupBoxLabels.Controls.Add(this.textBoxNewLabel);
            this.groupBoxLabels.Controls.Add(this.listBoxLabels);
            this.groupBoxLabels.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxLabels.Location = new System.Drawing.Point(3, 3);
            this.groupBoxLabels.Name = "groupBoxLabels";
            this.groupBoxLabels.Size = new System.Drawing.Size(232, 177);
            this.groupBoxLabels.TabIndex = 0;
            this.groupBoxLabels.TabStop = false;
            this.groupBoxLabels.Text = "Значения";
            // 
            // buttonAdd
            // 
            this.buttonAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonAdd.Location = new System.Drawing.Point(114, 151);
            this.buttonAdd.Name = "buttonAdd";
            this.buttonAdd.Size = new System.Drawing.Size(75, 23);
            this.buttonAdd.TabIndex = 2;
            this.buttonAdd.Text = "Добавить";
            this.buttonAdd.UseVisualStyleBackColor = true;
            this.buttonAdd.Click += new System.EventHandler(this.AddLabel);
            // 
            // textBoxNewLabel
            // 
            this.textBoxNewLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.textBoxNewLabel.Location = new System.Drawing.Point(7, 151);
            this.textBoxNewLabel.Name = "textBoxNewLabel";
            this.textBoxNewLabel.Size = new System.Drawing.Size(100, 20);
            this.textBoxNewLabel.TabIndex = 1;
            // 
            // listBoxLabels
            // 
            this.listBoxLabels.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listBoxLabels.FormattingEnabled = true;
            this.listBoxLabels.Location = new System.Drawing.Point(3, 16);
            this.listBoxLabels.Name = "listBoxLabels";
            this.listBoxLabels.Size = new System.Drawing.Size(223, 121);
            this.listBoxLabels.TabIndex = 0;
            this.listBoxLabels.SelectedIndexChanged += new System.EventHandler(this.SelectedIndexChanged);
            // 
            // groupBoxFunction
            // 
            this.groupBoxFunction.Controls.Add(this.numY);
            this.groupBoxFunction.Controls.Add(this.numX);
            this.groupBoxFunction.Controls.Add(this.btnClear);
            this.groupBoxFunction.Controls.Add(this.btnSetPoint);
            this.groupBoxFunction.Controls.Add(this.label4);
            this.groupBoxFunction.Controls.Add(this.label3);
            this.groupBoxFunction.Controls.Add(this.rbPolynom);
            this.groupBoxFunction.Controls.Add(this.rbLinear);
            this.groupBoxFunction.Controls.Add(this.chart);
            this.groupBoxFunction.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxFunction.Location = new System.Drawing.Point(241, 3);
            this.groupBoxFunction.Name = "groupBoxFunction";
            this.groupBoxFunction.Size = new System.Drawing.Size(353, 177);
            this.groupBoxFunction.TabIndex = 1;
            this.groupBoxFunction.TabStop = false;
            this.groupBoxFunction.Text = "Функция принадлежности";
            // 
            // numY
            // 
            this.numY.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.numY.DecimalPlaces = 2;
            this.numY.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.numY.Location = new System.Drawing.Point(109, 155);
            this.numY.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numY.Name = "numY";
            this.numY.Size = new System.Drawing.Size(43, 20);
            this.numY.TabIndex = 9;
            // 
            // numX
            // 
            this.numX.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.numX.Location = new System.Drawing.Point(33, 155);
            this.numX.Maximum = new decimal(new int[] {
            35,
            0,
            0,
            0});
            this.numX.Name = "numX";
            this.numX.Size = new System.Drawing.Size(39, 20);
            this.numX.TabIndex = 4;
            // 
            // btnClear
            // 
            this.btnClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnClear.Location = new System.Drawing.Point(239, 152);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(75, 23);
            this.btnClear.TabIndex = 8;
            this.btnClear.Text = "Очистить";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.ToDefault);
            // 
            // btnSetPoint
            // 
            this.btnSetPoint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSetPoint.Location = new System.Drawing.Point(158, 152);
            this.btnSetPoint.Name = "btnSetPoint";
            this.btnSetPoint.Size = new System.Drawing.Size(75, 23);
            this.btnSetPoint.TabIndex = 7;
            this.btnSetPoint.Text = "Установить";
            this.btnSetPoint.UseVisualStyleBackColor = true;
            this.btnSetPoint.Click += new System.EventHandler(this.SetPoint);
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(83, 159);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(20, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "Y: ";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 160);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(20, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "X: ";
            // 
            // rbPolynom
            // 
            this.rbPolynom.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.rbPolynom.AutoSize = true;
            this.rbPolynom.Location = new System.Drawing.Point(259, 42);
            this.rbPolynom.Name = "rbPolynom";
            this.rbPolynom.Size = new System.Drawing.Size(71, 17);
            this.rbPolynom.TabIndex = 2;
            this.rbPolynom.TabStop = true;
            this.rbPolynom.Text = "Полином";
            this.rbPolynom.UseVisualStyleBackColor = true;
            this.rbPolynom.CheckedChanged += new System.EventHandler(this.CheckedChanged);
            // 
            // rbLinear
            // 
            this.rbLinear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.rbLinear.AutoSize = true;
            this.rbLinear.Location = new System.Drawing.Point(259, 19);
            this.rbLinear.Name = "rbLinear";
            this.rbLinear.Size = new System.Drawing.Size(75, 17);
            this.rbLinear.TabIndex = 1;
            this.rbLinear.TabStop = true;
            this.rbLinear.Text = "Линейная";
            this.rbLinear.UseVisualStyleBackColor = true;
            // 
            // chart
            // 
            this.chart.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            chartArea1.Name = "ChartArea1";
            this.chart.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.chart.Legends.Add(legend1);
            this.chart.Location = new System.Drawing.Point(6, 19);
            this.chart.Name = "chart";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.chart.Series.Add(series1);
            this.chart.Size = new System.Drawing.Size(247, 130);
            this.chart.TabIndex = 0;
            this.chart.Text = "chart1";
            // 
            // FuzzyFactCreationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(609, 300);
            this.Controls.Add(this.mainTable);
            this.Name = "FuzzyFactCreationForm";
            this.Text = "Form1";
            this.mainTable.ResumeLayout(false);
            this.subTable.ResumeLayout(false);
            this.groupBoxMedium.ResumeLayout(false);
            this.groupBoxMedium.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numMax)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMin)).EndInit();
            this.table.ResumeLayout(false);
            this.groupBoxLabels.ResumeLayout(false);
            this.groupBoxLabels.PerformLayout();
            this.groupBoxFunction.ResumeLayout(false);
            this.groupBoxFunction.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numX)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel mainTable;
        private System.Windows.Forms.Button returnFactBtn;
        private System.Windows.Forms.TableLayoutPanel subTable;
        private System.Windows.Forms.GroupBox groupBoxMedium;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TableLayoutPanel table;
        private System.Windows.Forms.GroupBox groupBoxLabels;
        private System.Windows.Forms.ListBox listBoxLabels;
        private System.Windows.Forms.Button buttonAdd;
        private System.Windows.Forms.TextBox textBoxNewLabel;
        private System.Windows.Forms.GroupBox groupBoxFunction;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart;
        private System.Windows.Forms.Button btnSetPoint;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.RadioButton rbPolynom;
        private System.Windows.Forms.RadioButton rbLinear;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.NumericUpDown numMax;
        private System.Windows.Forms.NumericUpDown numMin;
        private System.Windows.Forms.NumericUpDown numY;
        private System.Windows.Forms.NumericUpDown numX;

    }
}

