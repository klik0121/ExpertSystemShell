namespace ParallelSumTest
{
    partial class ParallelSumTestForm
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.mainTable = new System.Windows.Forms.TableLayoutPanel();
            this.paramsPanel = new System.Windows.Forms.TableLayoutPanel();
            this.buttonTest = new System.Windows.Forms.Button();
            this.groupBoxPyramid = new System.Windows.Forms.GroupBox();
            this.textBoxLimit = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBoxSegment = new System.Windows.Forms.GroupBox();
            this.textBoxLenght = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBoxStep = new System.Windows.Forms.GroupBox();
            this.textBoxStep = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.mainChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.mainTable.SuspendLayout();
            this.paramsPanel.SuspendLayout();
            this.groupBoxPyramid.SuspendLayout();
            this.groupBoxSegment.SuspendLayout();
            this.groupBoxStep.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mainChart)).BeginInit();
            this.SuspendLayout();
            // 
            // mainTable
            // 
            this.mainTable.ColumnCount = 2;
            this.mainTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.mainTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 70F));
            this.mainTable.Controls.Add(this.paramsPanel, 0, 0);
            this.mainTable.Controls.Add(this.mainChart, 1, 0);
            this.mainTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainTable.Location = new System.Drawing.Point(0, 0);
            this.mainTable.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.mainTable.Name = "mainTable";
            this.mainTable.RowCount = 1;
            this.mainTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.mainTable.Size = new System.Drawing.Size(1041, 541);
            this.mainTable.TabIndex = 0;
            // 
            // paramsPanel
            // 
            this.paramsPanel.ColumnCount = 1;
            this.paramsPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.paramsPanel.Controls.Add(this.buttonTest, 0, 3);
            this.paramsPanel.Controls.Add(this.groupBoxPyramid, 0, 0);
            this.paramsPanel.Controls.Add(this.groupBoxSegment, 0, 1);
            this.paramsPanel.Controls.Add(this.groupBoxStep, 0, 2);
            this.paramsPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.paramsPanel.Location = new System.Drawing.Point(3, 3);
            this.paramsPanel.Name = "paramsPanel";
            this.paramsPanel.RowCount = 4;
            this.paramsPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.paramsPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.paramsPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.paramsPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.paramsPanel.Size = new System.Drawing.Size(306, 535);
            this.paramsPanel.TabIndex = 0;
            // 
            // buttonTest
            // 
            this.buttonTest.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.buttonTest.Location = new System.Drawing.Point(101, 488);
            this.buttonTest.Name = "buttonTest";
            this.buttonTest.Size = new System.Drawing.Size(103, 39);
            this.buttonTest.TabIndex = 0;
            this.buttonTest.Text = "Тест";
            this.buttonTest.UseVisualStyleBackColor = true;
            this.buttonTest.Click += new System.EventHandler(this.buttonTest_Click);
            // 
            // groupBoxPyramid
            // 
            this.groupBoxPyramid.Controls.Add(this.textBoxLimit);
            this.groupBoxPyramid.Controls.Add(this.label3);
            this.groupBoxPyramid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxPyramid.Location = new System.Drawing.Point(3, 3);
            this.groupBoxPyramid.Name = "groupBoxPyramid";
            this.groupBoxPyramid.Size = new System.Drawing.Size(300, 154);
            this.groupBoxPyramid.TabIndex = 1;
            this.groupBoxPyramid.TabStop = false;
            this.groupBoxPyramid.Text = "Пирамидальный алгоритм";
            // 
            // textBoxLimit
            // 
            this.textBoxLimit.Location = new System.Drawing.Point(140, 68);
            this.textBoxLimit.Name = "textBoxLimit";
            this.textBoxLimit.Size = new System.Drawing.Size(159, 26);
            this.textBoxLimit.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 70);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(127, 19);
            this.label3.TabIndex = 0;
            this.label3.Text = "Лимит паралл. = ";
            // 
            // groupBoxSegment
            // 
            this.groupBoxSegment.Controls.Add(this.textBoxLenght);
            this.groupBoxSegment.Controls.Add(this.label2);
            this.groupBoxSegment.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxSegment.Location = new System.Drawing.Point(3, 163);
            this.groupBoxSegment.Name = "groupBoxSegment";
            this.groupBoxSegment.Size = new System.Drawing.Size(300, 154);
            this.groupBoxSegment.TabIndex = 2;
            this.groupBoxSegment.TabStop = false;
            this.groupBoxSegment.Text = "Сегментный алгоритм";
            // 
            // textBoxLenght
            // 
            this.textBoxLenght.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.textBoxLenght.Location = new System.Drawing.Point(149, 61);
            this.textBoxLenght.Name = "textBoxLenght";
            this.textBoxLenght.Size = new System.Drawing.Size(151, 26);
            this.textBoxLenght.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 64);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(147, 19);
            this.label2.TabIndex = 0;
            this.label2.Text = "Кол-во сегментов = ";
            // 
            // groupBoxStep
            // 
            this.groupBoxStep.Controls.Add(this.textBoxStep);
            this.groupBoxStep.Controls.Add(this.label1);
            this.groupBoxStep.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxStep.Location = new System.Drawing.Point(3, 323);
            this.groupBoxStep.Name = "groupBoxStep";
            this.groupBoxStep.Size = new System.Drawing.Size(300, 154);
            this.groupBoxStep.TabIndex = 3;
            this.groupBoxStep.TabStop = false;
            this.groupBoxStep.Text = "Шаговый алгоритм";
            // 
            // textBoxStep
            // 
            this.textBoxStep.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.textBoxStep.Location = new System.Drawing.Point(136, 68);
            this.textBoxStep.Name = "textBoxStep";
            this.textBoxStep.Size = new System.Drawing.Size(163, 26);
            this.textBoxStep.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 71);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(124, 19);
            this.label1.TabIndex = 0;
            this.label1.Text = "Величина шага =";
            // 
            // mainChart
            // 
            chartArea2.Name = "ChartArea1";
            this.mainChart.ChartAreas.Add(chartArea2);
            this.mainChart.Dock = System.Windows.Forms.DockStyle.Fill;
            legend2.Name = "Legend1";
            this.mainChart.Legends.Add(legend2);
            this.mainChart.Location = new System.Drawing.Point(315, 3);
            this.mainChart.Name = "mainChart";
            series2.ChartArea = "ChartArea1";
            series2.Legend = "Legend1";
            series2.Name = "Series1";
            this.mainChart.Series.Add(series2);
            this.mainChart.Size = new System.Drawing.Size(723, 535);
            this.mainChart.TabIndex = 1;
            this.mainChart.Text = "chart1";
            // 
            // ParallelSumTestForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1041, 541);
            this.Controls.Add(this.mainTable);
            this.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "ParallelSumTestForm";
            this.mainTable.ResumeLayout(false);
            this.paramsPanel.ResumeLayout(false);
            this.groupBoxPyramid.ResumeLayout(false);
            this.groupBoxPyramid.PerformLayout();
            this.groupBoxSegment.ResumeLayout(false);
            this.groupBoxSegment.PerformLayout();
            this.groupBoxStep.ResumeLayout(false);
            this.groupBoxStep.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mainChart)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel mainTable;
        private System.Windows.Forms.TableLayoutPanel paramsPanel;
        private System.Windows.Forms.Button buttonTest;
        private System.Windows.Forms.DataVisualization.Charting.Chart mainChart;
        private System.Windows.Forms.GroupBox groupBoxPyramid;
        private System.Windows.Forms.GroupBox groupBoxSegment;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBoxStep;
        private System.Windows.Forms.TextBox textBoxStep;
        private System.Windows.Forms.TextBox textBoxLenght;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxLimit;
        private System.Windows.Forms.Label label3;
    }
}

