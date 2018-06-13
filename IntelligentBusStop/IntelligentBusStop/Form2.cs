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
    public partial class Form2 : Form
    {
        List<List<string>> routes;
        public Form2(List<int> numbers, List<string> names, List<List<string>> ways)
        {
            InitializeComponent();
            for (int i = 0; i < numbers.Count; i++)
            {
                comboBox1.Items.Add(numbers[i].ToString() + " " + names[i]);
            }
            routes = ways;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox1.Text = "";
            for (int i = 0; i < routes[comboBox1.SelectedIndex].Count; i++)
            {
                textBox1.Text += routes[comboBox1.SelectedIndex][i];
                if (i != routes[comboBox1.SelectedIndex].Count - 1) textBox1.Text += Environment.NewLine;
            }
        }
    }
}
