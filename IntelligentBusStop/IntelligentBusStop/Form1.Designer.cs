namespace IntelligentBusStop
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
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
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.OutputBox1 = new System.Windows.Forms.TextBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.функцииToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.помощьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.оПрограммеToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.маршрутыToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.весовыеКоэффициентыToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.статистикаToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(518, 66);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(270, 24);
            this.comboBox1.TabIndex = 0;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox_SelectedIndexChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.OutputBox1);
            this.groupBox1.Location = new System.Drawing.Point(12, 44);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(499, 355);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Основная панель";
            // 
            // OutputBox1
            // 
            this.OutputBox1.BackColor = System.Drawing.SystemColors.InfoText;
            this.OutputBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.OutputBox1.ForeColor = System.Drawing.SystemColors.Info;
            this.OutputBox1.Location = new System.Drawing.Point(7, 22);
            this.OutputBox1.Multiline = true;
            this.OutputBox1.Name = "OutputBox1";
            this.OutputBox1.ReadOnly = true;
            this.OutputBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.OutputBox1.Size = new System.Drawing.Size(486, 327);
            this.OutputBox1.TabIndex = 0;
            this.OutputBox1.Text = "Для начала работы выберите остановку";
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.функцииToolStripMenuItem,
            this.помощьToolStripMenuItem,
            this.оПрограммеToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(800, 28);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // функцииToolStripMenuItem
            // 
            this.функцииToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.маршрутыToolStripMenuItem,
            this.весовыеКоэффициентыToolStripMenuItem,
            this.статистикаToolStripMenuItem});
            this.функцииToolStripMenuItem.Name = "функцииToolStripMenuItem";
            this.функцииToolStripMenuItem.Size = new System.Drawing.Size(82, 24);
            this.функцииToolStripMenuItem.Text = "Функции";
            // 
            // помощьToolStripMenuItem
            // 
            this.помощьToolStripMenuItem.Name = "помощьToolStripMenuItem";
            this.помощьToolStripMenuItem.Size = new System.Drawing.Size(81, 24);
            this.помощьToolStripMenuItem.Text = "Помощь";
            // 
            // оПрограммеToolStripMenuItem
            // 
            this.оПрограммеToolStripMenuItem.Name = "оПрограммеToolStripMenuItem";
            this.оПрограммеToolStripMenuItem.Size = new System.Drawing.Size(116, 24);
            this.оПрограммеToolStripMenuItem.Text = "О программе";
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Location = new System.Drawing.Point(674, 372);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(114, 21);
            this.radioButton1.TabIndex = 3;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "Сбор данных";
            this.radioButton1.UseVisualStyleBackColor = true;
            this.radioButton1.CheckedChanged += new System.EventHandler(this.radioButton_CheckedChanged);
            // 
            // маршрутыToolStripMenuItem
            // 
            this.маршрутыToolStripMenuItem.Name = "маршрутыToolStripMenuItem";
            this.маршрутыToolStripMenuItem.Size = new System.Drawing.Size(252, 26);
            this.маршрутыToolStripMenuItem.Text = "Маршруты";
            this.маршрутыToolStripMenuItem.Click += new System.EventHandler(this.маршрутыToolStripMenuItem_Click);
            // 
            // весовыеКоэффициентыToolStripMenuItem
            // 
            this.весовыеКоэффициентыToolStripMenuItem.Name = "весовыеКоэффициентыToolStripMenuItem";
            this.весовыеКоэффициентыToolStripMenuItem.Size = new System.Drawing.Size(252, 26);
            this.весовыеКоэффициентыToolStripMenuItem.Text = "Весовые коэффициенты";
            this.весовыеКоэффициентыToolStripMenuItem.Click += new System.EventHandler(this.весовыеКоэффициентыToolStripMenuItem_Click);
            // 
            // статистикаToolStripMenuItem
            // 
            this.статистикаToolStripMenuItem.Name = "статистикаToolStripMenuItem";
            this.статистикаToolStripMenuItem.Size = new System.Drawing.Size(252, 26);
            this.статистикаToolStripMenuItem.Text = "Статистика";
            this.статистикаToolStripMenuItem.Click += new System.EventHandler(this.статистикаToolStripMenuItem_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 406);
            this.Controls.Add(this.radioButton1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "Умная остановка v1.0";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox OutputBox1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem функцииToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem маршрутыToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem весовыеКоэффициентыToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem статистикаToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem помощьToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem оПрограммеToolStripMenuItem;
        private System.Windows.Forms.RadioButton radioButton1;
    }
}

