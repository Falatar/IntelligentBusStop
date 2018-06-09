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
using System.Net.Sockets;
using System.Net;
using System.Threading;

namespace IntelligentBusStop
{
    public partial class Form1 : Form
    {
        private static IPAddress remoteIPAddress;
        private static int remotePort;
        private static int localPort;
        private static string kek;
        public Form1()
        {
            InitializeComponent();
            localPort = 5002;
            remotePort = 5001;
            remoteIPAddress = IPAddress.Parse("127.0.0.1");

            comboBox1.Items.Add("Центр молодёжного творчества");
            OutputBox1.Clear();
            Thread tRec = new Thread(new ThreadStart(Receiver));
            tRec.Start();
            //OutputBox1.Text = "2 | Троллейбус | <1 мин. | На конечную" + Environment.NewLine + "2 | Троллейбус | 11 мин. | С конечной" + Environment.NewLine + "3 | Троллейбус | 8 мин. | На конечную" + Environment.NewLine + "3 | Троллейбус | 3 мин. | С конечной" + Environment.NewLine + "4 | Троллейбус | 7 мин." + Environment.NewLine + "8 | Троллейбус | 6 мин. | На конечную" + Environment.NewLine + "8 | Троллейбус | 4 мин. | С конечной";
            DoWorkAsyncInfiniteLoop();
        }

        private async Task DoWorkAsyncInfiniteLoop()
        {
            while (true)
            {
                //label1.Text = kek;
                //await Task.Delay(200);
                double lat;
                double lng;
                int ID;
                int time;
                int day;
                bool isHoliday;
                int temperature;
                double weather;

                int counter = 0;
                for (int i = 0; i < kek.Length; i++)
                {
                    string temp = "";
                    if (kek[i] != ' ') temp += kek[i];
                    if (kek[i] == ' ' || i == kek.Length - 1)
                    {
                        switch (counter)
                        {
                            case 0:
                                lat = double.Parse(temp);
                                break;
                            case 1:
                                lng = double.Parse(temp);
                                break;
                            case 2:
                                ID = int.Parse(temp);
                                break;
                            case 3:
                                time = int.Parse(temp);
                                break;
                            case 4:
                                day = int.Parse(temp);
                                break;
                            case 5:
                                if (temp == "0") isHoliday = false;
                                else isHoliday = true;
                                break;
                            case 6:
                                temperature = int.Parse(temp);
                                break;
                            case 7:
                                weather = double.Parse(temp);
                                break;
                        }
                        counter++;
                    }
                    OutputBox1.Text += kek + Environment.NewLine;
                }
            }
        }

        public static void Receiver()
        {
            // Создаем UdpClient для чтения входящих данных
            UdpClient receivingUdpClient = new UdpClient(localPort);

            IPEndPoint RemoteIpEndPoint = null;

            try
            {
                while (true)
                {
                    // Ожидание дейтаграммы
                    byte[] receiveBytes = receivingUdpClient.Receive(
                       ref RemoteIpEndPoint);

                    // Преобразуем и отображаем данные
                    string returnData = Encoding.UTF8.GetString(receiveBytes);
                    kek = returnData;
                    //await Task.Delay(200);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Возникло исключение: " + ex.ToString() + "\n  " + ex.Message);
            }
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
