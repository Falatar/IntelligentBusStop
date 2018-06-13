using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IntelligentBusStop
{
    public partial class Form3 : Form
    {
        public Form3(string fileName)
        {
            InitializeComponent();
            using (StreamReader sR = new StreamReader(fileName))
            {
                while (true)
                {
                    string temp = sR.ReadLine();
                    if (temp == null) break;
                    for (int i = 0; i < temp.Length; i++)
                    {
                        textBox1.Text += temp[i];
                        if (temp[i] == ' ') textBox1.Text += "| ";
                    }
                    textBox1.Text += Environment.NewLine;
                }
            }
        }
    }
}
