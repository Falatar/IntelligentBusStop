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
        private static int kekl;

        private static double lat;
        private static double lng;
        private static int ID;
        private static int time;
        private static int day;
        private static bool isHoliday;
        private static int temperature;
        private static double weather;


        public Form1()
        {
            InitializeComponent();
            localPort = 8002;
            remotePort = 8001;
            remoteIPAddress = IPAddress.Parse("127.0.0.1");

            List<Stop> busStops = new List<Stop>();
            List<Road> mapRoads = new List<Road>();
            List<Route> busRoutes = new List<Route>();
            List<Transport> mapTransports = new List<Transport>();

            uploadStops(busStops);
            uploadRoads(mapRoads);
            uploadRoutes(busRoutes);
            uploadTransports(mapTransports);

            comboBox1.Items.Add("Центр молодёжного творчества");
            OutputBox1.Clear();
            Thread tRec = new Thread(new ThreadStart(Receiver));
            tRec.Start();
            //OutputBox1.Text = "2 | Троллейбус | <1 мин. | На конечную" + Environment.NewLine + "2 | Троллейбус | 11 мин. | С конечной" + Environment.NewLine + "3 | Троллейбус | 8 мин. | На конечную" + Environment.NewLine + "3 | Троллейбус | 3 мин. | С конечной" + Environment.NewLine + "4 | Троллейбус | 7 мин." + Environment.NewLine + "8 | Троллейбус | 6 мин. | На конечную" + Environment.NewLine + "8 | Троллейбус | 4 мин. | С конечной";
            DoWorkAsyncInfiniteLoop();
        }

        public int kreep(string s)
        {
            return s.Length;
        }

        private async Task DoWorkAsyncInfiniteLoop()
        {
            while (true)
            {
                //OutputBox1.Text += kekl + Environment.NewLine;
                //await Task.Delay(200);
                if (lat != 0)
                {
                    OutputBox1.Text += /*kek + "\n";*/ lat.ToString() + " | " + lng.ToString() + " | " + ID.ToString() + " | " + time.ToString() + " | " + day.ToString() + " | " + isHoliday.ToString() + " | " + temperature.ToString() + " | " + weather.ToString() + Environment.NewLine;
                }
                await Task.Delay(200);
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
                    int counter = 0;
                    string temp = "";
                    if (kek.Length > 1)
                    {
                        for (int i = 0; i < kek.Length; i++)
                        {
                            if (kek[i] != ' ') temp += kek[i];
                            if (kek[i] == ' ' || i == kek.Length - 1)
                            {
                                switch (counter)
                                {
                                    case 0:
                                        lat = double.Parse(temp);
                                        temp = "";
                                        break;

                                    case 1:
                                        lng = double.Parse(temp);
                                        temp = "";
                                        break;

                                    case 2:
                                        ID = int.Parse(temp);
                                        temp = "";
                                        break;

                                    case 3:
                                        time = int.Parse(temp);
                                        temp = "";
                                        break;

                                    case 4:
                                        day = int.Parse(temp);
                                        temp = "";
                                        break;

                                    case 5:
                                        if (temp == "0") isHoliday = false;
                                        else isHoliday = true;
                                        temp = "";
                                        break;

                                    case 6:
                                        temperature = int.Parse(temp);
                                        temp = "";
                                        break;

                                    case 7:
                                        weather = double.Parse(temp);
                                        temp = "";
                                        break;
                                }
                                counter++;
                            }
                        }
                    }

                    //await Task.Delay(200);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Возникло исключение: " /*+ ex.ToString()*/ + " | " + "\n  " /*+ ex.Message*/);
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
                    if (lines[j] == statistics[i][0] && lines[j + 1] == statistics[i][1] || lines[j + 1] == statistics[i][0] && lines[j] == statistics[i][1])
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

        private void оПрограммеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //DoWorkAsyncInfiniteLoop();
            OutputBox1.Text += double.Parse("25.435452523") + "|" + double.Parse("25,435452523");
        }

        private void uploadStops(List<Stop> stops)
        {
            using (StreamReader sR = new StreamReader("stops.txt"))
            {
                int score = 0;
                Stop tmp = new Stop();
                while (true)
                {
                    string temp = sR.ReadLine();
                    if (temp == null) break;
                    switch (score)
                    {
                        case 0:
                            tmp.ID = int.Parse(temp);
                            break;

                        case 1:
                            tmp.weight = int.Parse(temp);
                            break;

                        case 2:
                            tmp.name = temp;
                            break;

                        case 3:
                            tmp.connectedRoutes = space_Parsing(temp);
                            break;

                        case 4:
                            break;

                        case 5:
                            break;

                        case 6:
                            tmp.latitude = double.Parse(temp, CultureInfo.InvariantCulture);
                            break;

                        case 7:
                            tmp.longitude = double.Parse(temp, CultureInfo.InvariantCulture);
                            break;
                    }
                    score += 1;
                    if (score == 8)
                    {
                        score = 0;
                        Stop newPoint = new Stop(tmp);
                        stops.Add(newPoint);
                    }
                }
            }
        }

        private void uploadRoads(List<Road> roads)
        {
            using (StreamReader sR = new StreamReader("lines.txt"))
            {
                int score = 0;
                Stop tmp = new Stop();
                List<int> stops, distances = new List<int>();
                string temp = sR.ReadLine();
                stops = space_Parsing(temp);
                temp = sR.ReadLine();
                distances = space_Parsing(temp);
                using (StreamReader sR1 = new StreamReader("weights.txt"))
                {
                    List<List<double>> roadWeights = new List<List<double>>();
                    while (true)
                    {
                        string deepTemp = sR.ReadLine();
                        if (deepTemp == null) break;
                        List<double> dayWeights = new List<double>();
                        dayWeights = spaceDouble_Parsing(deepTemp);
                        roadWeights.Add(dayWeights);
                    }
                        for (int i = 0; i < stops.Count; i += 2)
                    {
                        Road oneRoad = new Road();
                        oneRoad.firstStop = stops[i];
                        oneRoad.secondStop = stops[i + 1];
                        oneRoad.secondStop = distances[i / 2];
                        oneRoad.weight = roadWeights[i / 2];
                        roads.Add(oneRoad);
                    }
                }
            }
        }

        private void uploadRoutes(List<Route> routes)
        {
            using (StreamReader sR = new StreamReader("routes.txt"))
            {
                int score = 0;
                Route tmp = new Route();
                while (true)
                {
                    string temp = sR.ReadLine();
                    if (temp == null) break;
                    switch (score)
                    {
                        case 0:
                            tmp.ID = int.Parse(temp);
                            break;

                        case 1:
                            tmp.routeNumber = int.Parse(temp);
                            break;
                        case 2:
                            tmp.transportType = temp;
                            break;
                        case 3:
                            tmp.path = space_Parsing(temp);
                            break;
                    }
                    score += 1;
                    if (score == 4)
                    {
                        score = 0;
                        Route newRoute = new Route(tmp);
                        routes.Add(newRoute);
                    }
                }
            }
        }

        private void uploadTransports(List<Transport> transports)
        {
            using (StreamReader sR = new StreamReader("transports.txt"))
            {
                int score = 0;
                Transport tmp = new Transport();
                while (true)
                {
                    string temp = sR.ReadLine();
                    if (temp == null) break;
                    switch (score)
                    {
                        case 0:
                            tmp.routeId = int.Parse(temp);
                            break;

                        case 1:
                            tmp.trackerId = int.Parse(temp);
                            break;

                        case 2:
                            break;

                        case 3:
                            tmp.previousStop = int.Parse(temp); ;
                            break;

                        case 4:
                            tmp.nextStop = int.Parse(temp);
                            break;

                        case 5:
                            break;
                    }
                    score += 1;
                    if (score == 6)
                    {
                        score = 0;
                        Transport newTransport = new Transport(tmp);
                        transports.Add(newTransport);
                    }
                }
            }
        }

        private void trainNN(List<Road> roads)
        {
            List<int> inputDistances = new List<int>();
            List<double> inputWeights = new List<double>();
            List<int> inputTemperatures = new List<int>();
            List<double> inputWeathers = new List<double>();
            List<int> inputDays = new List<int>();
            List<bool> inputHolidays = new List<bool>();
            List<string> inputTransports = new List<string>();
            List<int> outputTimes = new List<int>();

            using (StreamReader sR = new StreamReader("transports.txt"))
            {
                while (true)
                {
                    string temp = sR.ReadLine();
                    if (temp == null) break;
                    string tmp = "";
                    int counter = 0;
                    int lat = 0, lng = 0, time = 0;
                    for (int i = 0; i < temp.Length; i++)
                    {
                        if (temp[i] != ' ') tmp += temp[i];
                        if (temp[i] == ' ' || i == temp.Length - 1)
                        {
                            switch (counter)
                            {
                                case 0:
                                    lat = int.Parse(tmp);
                                    break;
                                case 1:
                                    lng = int.Parse(tmp);
                                    break;
                                case 2:
                                    time = int.Parse(tmp);
                                    break;
                                case 3:
                                    outputTimes.Add(int.Parse(tmp));
                                    break;
                                case 4:
                                    inputDays.Add(int.Parse(tmp));
                                    break;
                                case 5:
                                    inputHolidays.Add(bool.Parse(tmp));
                                    break;
                                case 6:
                                    inputTemperatures.Add(int.Parse(tmp));
                                    break;
                                case 7:
                                    inputWeathers.Add(double.Parse(tmp));
                                    break;
                            }
                            tmp = "";
                        }
                        if (i == temp.Length - 1)
                        {
                            for (int j = 0; j < roads.Count; j++)
                            {
                                if (lat == roads[i].firstStop && lng == roads[i].secondStop || lng == roads[i].firstStop && lat == roads[i].secondStop)
                                {
                                    inputDistances.Add(roads[i].length);
                                    inputWeights.Add(roads[i].weight[time / 3600]);
                                    break;
                                }
                            }

                        }
                    }
                }
            }
        }
    }
}
