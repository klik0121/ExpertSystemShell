namespace FuzzyLogicRedactor
{
    partial class LogicTestingForm
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
            this.factTable = new System.Windows.Forms.TableLayoutPanel();
            this.groupBoxFisrt = new System.Windows.Forms.GroupBox();
            this.fisrtCreate = new System.Windows.Forms.Button();
            this.firstValue = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.firstName = new System.Windows.Forms.ComboBox();
            this.groupBoxSecond = new System.Windows.Forms.GroupBox();
            this.secondCreate = new System.Windows.Forms.Button();
            this.secondValue = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.secondName = new System.Windows.Forms.ComboBox();
            this.operationBox = new System.Windows.Forms.GroupBox();
            this.operationType = new System.Windows.Forms.ComboBox();
            this.ok = new System.Windows.Forms.Button();
            this.chart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.operation = new System.Windows.Forms.ComboBox();
            this.mainTable.SuspendLayout();
            this.factTable.SuspendLayout();
            this.groupBoxFisrt.SuspendLayout();
            this.groupBoxSecond.SuspendLayout();
            this.operationBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart)).BeginInit();
            this.SuspendLayout();
            // 
            // mainTable
            // 
            this.mainTable.ColumnCount = 1;
            this.mainTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.mainTable.Controls.Add(this.factTable, 0, 0);
            this.mainTable.Controls.Add(this.operationBox, 0, 1);
            this.mainTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainTable.Location = new System.Drawing.Point(0, 0);
            this.mainTable.Name = "mainTable";
            this.mainTable.RowCount = 2;
            this.mainTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 26.37681F));
            this.mainTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 73.62319F));
            this.mainTable.Size = new System.Drawing.Size(637, 345);
            this.mainTable.TabIndex = 0;
            // 
            // factTable
            // 
            this.factTable.ColumnCount = 2;
            this.factTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.factTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.factTable.Controls.Add(this.groupBoxFisrt, 0, 0);
            this.factTable.Controls.Add(this.groupBoxSecond, 1, 0);
            this.factTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.factTable.Location = new System.Drawing.Point(3, 3);
            this.factTable.Name = "factTable";
            this.factTable.RowCount = 1;
            this.factTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.factTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 84F));
            this.factTable.Size = new System.Drawing.Size(631, 84);
            this.factTable.TabIndex = 0;
            // 
            // groupBoxFisrt
            // 
            this.groupBoxFisrt.Controls.Add(this.fisrtCreate);
            this.groupBoxFisrt.Controls.Add(this.firstValue);
            this.groupBoxFisrt.Controls.Add(this.label2);
            this.groupBoxFisrt.Controls.Add(this.label1);
            this.groupBoxFisrt.Controls.Add(this.firstName);
            this.groupBoxFisrt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxFisrt.Location = new System.Drawing.Point(3, 3);
            this.groupBoxFisrt.Name = "groupBoxFisrt";
            this.groupBoxFisrt.Size = new System.Drawing.Size(309, 78);
            this.groupBoxFisrt.TabIndex = 0;
            this.groupBoxFisrt.TabStop = false;
            this.groupBoxFisrt.Text = "Первая переменная";
            // 
            // fisrtCreate
            // 
            this.fisrtCreate.Location = new System.Drawing.Point(232, 17);
            this.fisrtCreate.Name = "fisrtCreate";
            this.fisrtCreate.Size = new System.Drawing.Size(75, 23);
            this.fisrtCreate.TabIndex = 6;
            this.fisrtCreate.Text = "Создать";
            this.fisrtCreate.UseVisualStyleBackColor = true;
            this.fisrtCreate.Click += new System.EventHandler(this.CreateVariable);
            // 
            // firstValue
            // 
            this.firstValue.FormattingEnabled = true;
            this.firstValue.Location = new System.Drawing.Point(107, 43);
            this.firstValue.Name = "firstValue";
            this.firstValue.Size = new System.Drawing.Size(121, 21);
            this.firstValue.TabIndex = 3;
            this.firstValue.SelectedValueChanged += new System.EventHandler(this.FirstValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 46);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Значение";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Переменная";
            // 
            // firstName
            // 
            this.firstName.FormattingEnabled = true;
            this.firstName.Location = new System.Drawing.Point(107, 19);
            this.firstName.Name = "firstName";
            this.firstName.Size = new System.Drawing.Size(121, 21);
            this.firstName.TabIndex = 0;
            this.firstName.SelectedValueChanged += new System.EventHandler(this.FirstNameChanged);
            // 
            // groupBoxSecond
            // 
            this.groupBoxSecond.Controls.Add(this.secondCreate);
            this.groupBoxSecond.Controls.Add(this.secondValue);
            this.groupBoxSecond.Controls.Add(this.label5);
            this.groupBoxSecond.Controls.Add(this.label6);
            this.groupBoxSecond.Controls.Add(this.secondName);
            this.groupBoxSecond.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxSecond.Location = new System.Drawing.Point(318, 3);
            this.groupBoxSecond.Name = "groupBoxSecond";
            this.groupBoxSecond.Size = new System.Drawing.Size(310, 78);
            this.groupBoxSecond.TabIndex = 1;
            this.groupBoxSecond.TabStop = false;
            this.groupBoxSecond.Text = "Вторая переменная";
            // 
            // secondCreate
            // 
            this.secondCreate.Location = new System.Drawing.Point(232, 17);
            this.secondCreate.Name = "secondCreate";
            this.secondCreate.Size = new System.Drawing.Size(75, 23);
            this.secondCreate.TabIndex = 13;
            this.secondCreate.Text = "Создать";
            this.secondCreate.UseVisualStyleBackColor = true;
            this.secondCreate.Click += new System.EventHandler(this.CreateVariable);
            // 
            // secondValue
            // 
            this.secondValue.FormattingEnabled = true;
            this.secondValue.Location = new System.Drawing.Point(107, 43);
            this.secondValue.Name = "secondValue";
            this.secondValue.Size = new System.Drawing.Size(121, 21);
            this.secondValue.TabIndex = 10;
            this.secondValue.SelectedValueChanged += new System.EventHandler(this.SecondValueChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 46);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(55, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "Значение";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 22);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(71, 13);
            this.label6.TabIndex = 8;
            this.label6.Text = "Переменная";
            // 
            // secondName
            // 
            this.secondName.FormattingEnabled = true;
            this.secondName.Location = new System.Drawing.Point(107, 19);
            this.secondName.Name = "secondName";
            this.secondName.Size = new System.Drawing.Size(121, 21);
            this.secondName.TabIndex = 7;
            this.secondName.SelectedValueChanged += new System.EventHandler(this.SecondNameChanged);
            // 
            // operationBox
            // 
            this.operationBox.Controls.Add(this.operationType);
            this.operationBox.Controls.Add(this.ok);
            this.operationBox.Controls.Add(this.chart);
            this.operationBox.Controls.Add(this.operation);
            this.operationBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.operationBox.Location = new System.Drawing.Point(3, 93);
            this.operationBox.Name = "operationBox";
            this.operationBox.Size = new System.Drawing.Size(631, 249);
            this.operationBox.TabIndex = 1;
            this.operationBox.TabStop = false;
            this.operationBox.Text = "Операция";
            this.operationBox.UseCompatibleTextRendering = true;
            // 
            // operationType
            // 
            this.operationType.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.operationType.FormattingEnabled = true;
            this.operationType.Location = new System.Drawing.Point(258, 18);
            this.operationType.Name = "operationType";
            this.operationType.Size = new System.Drawing.Size(121, 21);
            this.operationType.TabIndex = 3;
            // 
            // ok
            // 
            this.ok.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.ok.Location = new System.Drawing.Point(385, 17);
            this.ok.Name = "ok";
            this.ok.Size = new System.Drawing.Size(75, 23);
            this.ok.TabIndex = 2;
            this.ok.Text = "OK";
            this.ok.UseVisualStyleBackColor = true;
            this.ok.Click += new System.EventHandler(this.DisplayOperation);
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
            this.chart.Location = new System.Drawing.Point(9, 45);
            this.chart.Name = "chart";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.chart.Series.Add(series1);
            this.chart.Size = new System.Drawing.Size(620, 195);
            this.chart.TabIndex = 1;
            this.chart.Text = "chart1";
            // 
            // operation
            // 
            this.operation.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.operation.FormattingEnabled = true;
            this.operation.Location = new System.Drawing.Point(131, 17);
            this.operation.Name = "operation";
            this.operation.Size = new System.Drawing.Size(121, 21);
            this.operation.TabIndex = 0;
            // 
            // LogicTestingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(637, 345);
            this.Controls.Add(this.mainTable);
            this.Name = "LogicTestingForm";
            this.Text = "LogicTestingForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.LogicTestingFormClosing);
            this.mainTable.ResumeLayout(false);
            this.factTable.ResumeLayout(false);
            this.groupBoxFisrt.ResumeLayout(false);
            this.groupBoxFisrt.PerformLayout();
            this.groupBoxSecond.ResumeLayout(false);
            this.groupBoxSecond.PerformLayout();
            this.operationBox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chart)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel mainTable;
        private System.Windows.Forms.TableLayoutPanel factTable;
        private System.Windows.Forms.GroupBox groupBoxFisrt;
        private System.Windows.Forms.Button fisrtCreate;
        private System.Windows.Forms.ComboBox firstValue;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox firstName;
        private System.Windows.Forms.GroupBox groupBoxSecond;
        private System.Windows.Forms.Button secondCreate;
        private System.Windows.Forms.ComboBox secondValue;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox secondName;
        private System.Windows.Forms.GroupBox operationBox;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart;
        private System.Windows.Forms.ComboBox operation;
        private System.Windows.Forms.Button ok;
        private System.Windows.Forms.ComboBox operationType;
    }
}