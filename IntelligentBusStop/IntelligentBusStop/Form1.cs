using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IntelligentBusStop
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            comboBox1.Items.Add("МОПРа");
            OutputBox1.Text = "1 | Троллейбус | 6 мин." + Environment.NewLine + "3 | Троллейбус | <1 мин." + Environment.NewLine + "4 | Троллейбус | 3 мин." + Environment.NewLine + "8 | Троллейбус | 7 мин.";
        }

        private void маршрутыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 Routes = new Form2();
            Routes.ShowDialog();
        }

        private void весовыеКоэффициентыToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void статистикаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form3 Stats = new Form3();
            Stats.ShowDialog();
        }

        private void radioButton_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
