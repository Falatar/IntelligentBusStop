using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
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
            comboBox1.Items.Add("Центр молодёжного творчества");
            OutputBox1.Text = "2 | Троллейбус | <1 мин. | На конечную" + Environment.NewLine + "2 | Троллейбус | 11 мин. | С конечной" + Environment.NewLine + "3 | Троллейбус | 8 мин. | На конечную" + Environment.NewLine + "3 | Троллейбус | 3 мин. | С конечной" + Environment.NewLine + "4 | Троллейбус | 7 мин." + Environment.NewLine + "8 | Троллейбус | 6 мин. | На конечную" + Environment.NewLine + "8 | Троллейбус | 4 мин. | С конечной";
        }

        private void маршрутыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 Routes = new Form2();
            Routes.ShowDialog();
        }

        private void весовыеКоэффициентыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<int> lines = new List<int>();
            List<int> distances = new List<int>();
            using (StreamReader sR = new StreamReader("lines.txt"))
            {
                string temp = sR.ReadLine();
                lines = space_Parsing(temp);
                string temp2 = sR.ReadLine();
                distances = space_Parsing(temp2);
            }
            List<List<int>> statistics = new List<List<int>>();
            using (StreamReader sR = new StreamReader("stat.txt"))
            {
                while (true)
                {
                    string temp = sR.ReadLine();
                    if (temp == null) break;
                    List<int> stat = new List<int>();
                    stat = space_Parsing(temp);
                    statistics.Add(stat);
                }
            }
            List<double> weights = new List<double>();
            List<int> count = new List<int>();
            for (int i = 0; i < lines.Count / 2; i++)
            {
                weights.Add(0);
                count.Add(0);
            }
            for (int i = 0; i < statistics.Count; i++)
            {
                for (int j = 0; j < lines.Count; j += 2)
                {
                    if (lines[j] == statistics[i][0] && lines[j+1] == statistics[i][1] || lines[j + 1] == statistics[i][0] && lines[j] == statistics[i][1])
                    {
                        weights[j / 2] += ((double)distances[j / 2] / (double)statistics[i][2]) / (double)(60 * 10 / 36);
                        count[j / 2]++;
                        break;
                    }
                }
            }
            for (int i = 0; i < weights.Count; i++)
            {
                weights[i] = weights[i] / count[i];
            }
            using (StreamWriter sw = new StreamWriter("weights.txt", false, System.Text.Encoding.Default))
            {
                for (int i = 0; i < weights.Count; i++)
                {
                    string ToFile = "";
                    ToFile += weights[i].ToString();
                    if (i != weights.Count - 1) ToFile += " ";
                    sw.Write(ToFile);
                }
            }
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

        private List<int> space_Parsing(string space_str)
        {
            List<int> temp = new List<int>();
            List<int> result = new List<int>();
            for (int i = 0; i < space_str.Length; i++)
            {
                if (space_str[i] != ' ') temp.Add(int.Parse(space_str[i].ToString()));
                if (space_str[i] == ' ' || i == space_str.Length - 1)
                {
                    int summ = 0;
                    for (int j = temp.Count - 1, k = 1; j >= 0; j--, k *= 10)
                    {
                        summ += temp[j] * k;
                    }
                    result.Add(summ);
                    temp.RemoveRange(0, temp.Count);
                }
            }
            return result;
        }

        private List<double> spaceDouble_Parsing(string space_str)
        {
            string temp = "";
            List<double> result = new List<double>();
            for (int i = 0; i < space_str.Length; i++)
            {
                if (space_str[i] != ' ') temp += space_str[i];
                if (space_str[i] == ' ' || i == space_str.Length - 1)
                {
                    result.Add(double.Parse(temp, CultureInfo.InvariantCulture));
                    temp = "";
                }
            }
            return result;
        }
    }
}
