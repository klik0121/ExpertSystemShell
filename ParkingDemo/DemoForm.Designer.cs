namespace ParkingDemo
{
    partial class DemoForm
    {
        /// <summary>
        /// Требуется переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.picture = new System.Windows.Forms.PictureBox();
            this.controls = new System.Windows.Forms.GroupBox();
            this.YNum = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.funcTypeComboBox = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.methodComboBox = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.Tetanum = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.buttonGetAngle = new System.Windows.Forms.Button();
            this.FInum = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.Xnum = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picture)).BeginInit();
            this.controls.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.YNum)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Tetanum)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FInum)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Xnum)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 75F));
            this.tableLayoutPanel1.Controls.Add(this.picture, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.controls, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(585, 289);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // picture
            // 
            this.picture.Dock = System.Windows.Forms.DockStyle.Fill;
            this.picture.Location = new System.Drawing.Point(149, 3);
            this.picture.Name = "picture";
            this.picture.Size = new System.Drawing.Size(433, 283);
            this.picture.TabIndex = 0;
            this.picture.TabStop = false;
            // 
            // controls
            // 
            this.controls.Controls.Add(this.label7);
            this.controls.Controls.Add(this.YNum);
            this.controls.Controls.Add(this.label6);
            this.controls.Controls.Add(this.funcTypeComboBox);
            this.controls.Controls.Add(this.label5);
            this.controls.Controls.Add(this.methodComboBox);
            this.controls.Controls.Add(this.label4);
            this.controls.Controls.Add(this.Tetanum);
            this.controls.Controls.Add(this.label3);
            this.controls.Controls.Add(this.buttonGetAngle);
            this.controls.Controls.Add(this.FInum);
            this.controls.Controls.Add(this.label2);
            this.controls.Controls.Add(this.Xnum);
            this.controls.Controls.Add(this.label1);
            this.controls.Dock = System.Windows.Forms.DockStyle.Fill;
            this.controls.Location = new System.Drawing.Point(3, 3);
            this.controls.Name = "controls";
            this.controls.Size = new System.Drawing.Size(140, 283);
            this.controls.TabIndex = 1;
            this.controls.TabStop = false;
            // 
            // YNum
            // 
            this.YNum.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.YNum.Location = new System.Drawing.Point(42, 44);
            this.YNum.Maximum = new decimal(new int[] {
            300,
            0,
            0,
            0});
            this.YNum.Name = "YNum";
            this.YNum.Size = new System.Drawing.Size(98, 20);
            this.YNum.TabIndex = 12;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(10, 46);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(23, 13);
            this.label6.TabIndex = 11;
            this.label6.Text = "Y =";
            // 
            // funcTypeComboBox
            // 
            this.funcTypeComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.funcTypeComboBox.FormattingEnabled = true;
            this.funcTypeComboBox.Items.AddRange(new object[] {
            "Линейный",
            "Полином"});
            this.funcTypeComboBox.Location = new System.Drawing.Point(6, 178);
            this.funcTypeComboBox.Name = "funcTypeComboBox";
            this.funcTypeComboBox.Size = new System.Drawing.Size(134, 21);
            this.funcTypeComboBox.TabIndex = 10;
            this.funcTypeComboBox.SelectedIndexChanged += new System.EventHandler(this.MethodChanged);
            // 
            // label5
            // 
            this.label5.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(39, 162);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(72, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "Вид функции";
            // 
            // methodComboBox
            // 
            this.methodComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.methodComboBox.FormattingEnabled = true;
            this.methodComboBox.Location = new System.Drawing.Point(6, 125);
            this.methodComboBox.Name = "methodComboBox";
            this.methodComboBox.Size = new System.Drawing.Size(134, 21);
            this.methodComboBox.TabIndex = 8;
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(49, 109);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(39, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Метод";
            // 
            // Tetanum
            // 
            this.Tetanum.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Tetanum.DecimalPlaces = 3;
            this.Tetanum.Location = new System.Drawing.Point(35, 251);
            this.Tetanum.Maximum = new decimal(new int[] {
            45,
            0,
            0,
            0});
            this.Tetanum.Minimum = new decimal(new int[] {
            45,
            0,
            0,
            -2147483648});
            this.Tetanum.Name = "Tetanum";
            this.Tetanum.Size = new System.Drawing.Size(99, 20);
            this.Tetanum.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 253);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(22, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "t = ";
            // 
            // buttonGetAngle
            // 
            this.buttonGetAngle.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.buttonGetAngle.Location = new System.Drawing.Point(35, 222);
            this.buttonGetAngle.Name = "buttonGetAngle";
            this.buttonGetAngle.Size = new System.Drawing.Size(75, 23);
            this.buttonGetAngle.TabIndex = 4;
            this.buttonGetAngle.Text = "Рассчитать";
            this.buttonGetAngle.UseVisualStyleBackColor = true;
            this.buttonGetAngle.Click += new System.EventHandler(this.GetAngle);
            // 
            // FInum
            // 
            this.FInum.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.FInum.InterceptArrowKeys = false;
            this.FInum.Location = new System.Drawing.Point(42, 70);
            this.FInum.Maximum = new decimal(new int[] {
            180,
            0,
            0,
            0});
            this.FInum.Minimum = new decimal(new int[] {
            180,
            0,
            0,
            -2147483648});
            this.FInum.Name = "FInum";
            this.FInum.Size = new System.Drawing.Size(98, 20);
            this.FInum.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 72);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(24, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Fi =";
            // 
            // Xnum
            // 
            this.Xnum.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Xnum.Location = new System.Drawing.Point(42, 18);
            this.Xnum.Maximum = new decimal(new int[] {
            150,
            0,
            0,
            0});
            this.Xnum.Minimum = new decimal(new int[] {
            150,
            0,
            0,
            -2147483648});
            this.Xnum.Name = "Xnum";
            this.Xnum.Size = new System.Drawing.Size(98, 20);
            this.Xnum.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(26, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "X = ";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(10, 206);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(35, 13);
            this.label7.TabIndex = 13;
            this.label7.Text = "label7";
            // 
            // DemoForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(585, 289);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "DemoForm";
            this.Text = "Form1";
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picture)).EndInit();
            this.controls.ResumeLayout(false);
            this.controls.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.YNum)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Tetanum)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.FInum)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Xnum)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.PictureBox picture;
        private System.Windows.Forms.GroupBox controls;
        private System.Windows.Forms.Button buttonGetAngle;
        private System.Windows.Forms.NumericUpDown FInum;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown Xnum;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown Tetanum;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox methodComboBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox funcTypeComboBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown YNum;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
    }
}

