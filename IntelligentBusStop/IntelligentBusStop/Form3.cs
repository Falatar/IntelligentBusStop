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
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
            textBox1.Text += "7 | " + "8 | " + "Троллейбус | " + "213 | " + "3768" + Environment.NewLine;
            textBox1.Text += "16 | " + "17 | " + "Троллейбус | " + "149 | " + "3810" + Environment.NewLine;
            textBox1.Text += "14 | " + "24 | " + "Троллейбус | " + "213 | " + "3812";
        }
    }
}
