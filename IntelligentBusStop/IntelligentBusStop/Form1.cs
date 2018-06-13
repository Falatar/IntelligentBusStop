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
using System.Web;

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

        Collector collection = new Collector("stat.txt", 1.0);

        List<Stop> busStops = new List<Stop>();
        List<Road> mapRoads = new List<Road>();
        List<Route> busRoutes = new List<Route>();
        List<Transport> mapTransports = new List<Transport>();

        public Form1()
        {
            InitializeComponent();
            localPort = 8002;
            remotePort = 8001;
            remoteIPAddress = IPAddress.Parse("127.0.0.1");

            uploadStops(busStops);
            uploadRoads(mapRoads);
            uploadRoutes(busRoutes);
            uploadTransports(mapTransports);

            trainNN(mapRoads);

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
            List<int> numbers = new List<int>();
            List<string> names = new List<string>();
            List<List<string>> ways = new List<List<string>>();
            for (int i = 0; i < busRoutes.Count; i++)
            {
                numbers.Add(busRoutes[i].routeNumber);
                names.Add(busRoutes[i].transportType);
                List<string> temp = new List<string>();
                for (int j = 0; j < busRoutes[i].path.Count; j++)
                {
                    int stopID = 0;
                    for (int k = 0; k < busStops.Count; k++)
                    {
                        if (busStops[k].ID == busRoutes[i].path[j]) { stopID = k; break; }
                    }
                    temp.Add(busStops[stopID].name);
                }
                ways.Add(temp);
            }
            Form2 Routes = new Form2(numbers, names, ways);
            Routes.ShowDialog();
        }

        private void весовыеКоэффициентыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<int> lines = new List<int>();
            List<int> distances = new List<int>();
            for (int i = 0; i < mapRoads.Count; i++)
            {
                lines.Add(mapRoads[i].firstStop);
                lines.Add(mapRoads[i].secondStop);
                distances.Add(mapRoads[i].length);
            }
            collection.rewriteW(lines, distances);
        }

        private void статистикаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form3 Stats = new Form3(collection.fileName);
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
            using (StreamReader sR = new StreamReader("stops.txt", Encoding.Default))
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
            using (StreamReader sR = new StreamReader("lines.txt", Encoding.Default))
            {
                int score = 0;
                Stop tmp = new Stop();
                List<int> stops, distances = new List<int>();
                string temp = sR.ReadLine();
                stops = space_Parsing(temp);
                temp = sR.ReadLine();
                distances = space_Parsing(temp);
                using (StreamReader sR1 = new StreamReader("weights.txt", Encoding.Default))
                {
                    List<List<double>> roadWeights = new List<List<double>>();
                    while (true)
                    {
                        string deepTemp = sR1.ReadLine();
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
                        oneRoad.length = distances[i / 2];
                        oneRoad.weight = roadWeights[i / 2];
                        Road newRoad = new Road(oneRoad);
                        roads.Add(newRoad);
                    }
                }
            }
        }

        private void uploadRoutes(List<Route> routes)
        {
            using (StreamReader sR = new StreamReader("routes.txt", Encoding.Default))
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
            using (StreamReader sR = new StreamReader("transports.txt", Encoding.Default))
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

            using (StreamReader sR = new StreamReader("stat.txt", Encoding.Default))
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
                            counter++;
                            tmp = "";
                        }
                        if (i == temp.Length - 1)
                        {
                            for (int j = 0; j < roads.Count; j++)
                            {
                                if (lat == roads[j].firstStop && lng == roads[j].secondStop || lng == roads[j].firstStop && lat == roads[j].secondStop)
                                {
                                    inputDistances.Add(roads[j].length);
                                    inputWeights.Add(roads[j].weight[dayStatus(time)]);
                                    break;
                                }
                            }

                        }
                    }

                }
            }
        }

        private int dayStatus(int time)
        {
            int n = 0;
            int result = 0;
            for (int i = 0; i < 48; i++)
            {
                if (time > n && time < n + 1800)
                {
                    result = i;
                    break;
                }
                n += 1800;
            }
            return result;
        }

        private Pair<int, double> calculateDistance(double lat, double lng, int time, List<Road> roads, Route way, List<Stop> stops, Transport bus, Stop busStop, bool from, double error)
        {
            Pair<int, double> result = new Pair<int, double>(0, 0);
            //Формулы прямых
            List<double> As = new List<double>();
            List<double> Bs = new List<double>();
            List<double> Cs = new List<double>();
            //Вычисление параметров
            for (int i = 0; i < roads.Count; i++)
            {
                int stop1 = 0, stop2 = 0;
                for (int j = 0; j < stops.Count; j++)
                {
                    if (stops[j].ID == roads[i].firstStop) stop1 = j;
                    if (stops[j].ID == roads[i].secondStop) stop2 = j;
                }
                As.Add(stops[stop1].longitude - stops[stop2].longitude);
                Bs.Add(stops[stop2].latitude - stops[stop1].latitude);
                Cs.Add(stops[stop2].longitude * stops[stop1].latitude - stops[stop1].longitude * stops[stop2].latitude);
            }
            //Формулы перпендикуляров
            List<double> verticalAs = new List<double>();
            List<double> verticalBs = new List<double>();
            List<double> verticalCs = new List<double>();
            //Составление уравнений перпендикуляров
            for (int i = 0; i < roads.Count; i++)
            {
                verticalAs.Add(As[i]);
                verticalBs.Add(-Bs[i]);
                verticalCs.Add(As[i] * -lng - Bs[i] * -lat);
            }

            int currentRoad = 0;
            if (bus.previousStop != 0 && bus.nextStop != 0)
            {
                for (int i = 0; i < roads.Count; i++)
                {
                    if (bus.previousStop == roads[i].firstStop && bus.nextStop == roads[i].secondStop || bus.previousStop == roads[i].secondStop && bus.nextStop == roads[i].firstStop)
                    {
                        currentRoad = i;
                        break;
                    }
                }
            }
            else
            {
                //Определение подходящих перпендикуляров
                List<int> suitableRoads = new List<int>();
                List<double> suiX = new List<double>();
                List<double> suiY = new List<double>();
                for (int i = 0; i < roads.Count; i++)
                {
                    int stop1 = 0, stop2 = 0;
                    for (int j = 0; j < stops.Count; j++)
                    {
                        if (stops[j].ID == roads[i].firstStop) stop1 = j;
                        if (stops[j].ID == roads[i].secondStop) stop2 = j;
                    }
                    double x = (Cs[i] * verticalBs[i] - verticalCs[i] * Bs[i]) - (As[i] * verticalBs[i] - verticalAs[i] * Bs[i]);
                    double y = (As[i] * verticalCs[i] - verticalAs[i] * Cs[i]) - (As[i] * verticalBs[i] - verticalAs[i] * Bs[i]);

                    if (x >= stops[stop1].latitude - error && x <= stops[stop2].latitude + error && y >= stops[stop1].longitude - error && y <= stops[stop2].longitude + error) { suitableRoads.Add(i); suiX.Add(x); suiY.Add(y); continue; }
                    if (x <= stops[stop1].latitude + error && x >= stops[stop2].latitude - error && y >= stops[stop1].longitude - error && y <= stops[stop2].longitude + error) { suitableRoads.Add(i); suiX.Add(x); suiY.Add(y); continue; }
                    if (x >= stops[stop1].latitude - error && x <= stops[stop2].latitude + error && y <= stops[stop1].longitude + error && y >= stops[stop2].longitude - error) { suitableRoads.Add(i); suiX.Add(x); suiY.Add(y); continue; }
                    if (x <= stops[stop1].latitude + error && x >= stops[stop2].latitude - error && y <= stops[stop1].longitude + error && y >= stops[stop2].longitude - error) { suitableRoads.Add(i); suiX.Add(x); suiY.Add(y); continue; }
                }
                //Выявление окончательных решений
                double min = Math.Sqrt(Math.Pow(lat - suiX[0], 2) + Math.Pow(lng - suiY[0], 2));
                for (int i = 1; i < suitableRoads.Count; i++)
                {
                    double temp = Math.Sqrt(Math.Pow(lat - suiX[i], 2) + Math.Pow(lng - suiY[i], 2));
                    if (temp < min) { min = temp; currentRoad = suitableRoads[i]; }
                }
                if (Math.Sqrt(Math.Pow(lat - stops[roads[currentRoad].secondStop].latitude, 2) + Math.Pow(lng - stops[roads[currentRoad].secondStop].longitude, 2)) <= error)
                {
                    for (int i = 0; i < way.path.Count; i++)
                    {
                        if (roads[currentRoad].firstStop == way.path[i] && roads[currentRoad].secondStop == way.path[i + 1] || roads[currentRoad].firstStop == way.path[i + 1] && roads[currentRoad].secondStop == way.path[i])
                        {
                            if (i == way.path.Count - 2)
                            {
                                for (int j = 0; j < roads.Count; j++)
                                {
                                    if (roads[j].firstStop == way.path[i + 1] && roads[j].secondStop == way.path[0] || roads[j].secondStop == way.path[i] && roads[j].firstStop == way.path[0])
                                    {
                                        currentRoad = j;
                                        break;
                                    }
                                }
                                break;
                            }
                            else
                            {
                                for (int j = 0; j < roads.Count; j++)
                                {
                                    if (roads[j].firstStop == way.path[i + 1] && roads[j].secondStop == way.path[i + 2] || roads[j].secondStop == way.path[i + 1] && roads[j].firstStop == way.path[i + 2])
                                    {
                                        currentRoad = j;
                                        break;
                                    }
                                }
                                break;
                            }
                        }
                    }
                }
            }
            //Обход графа и сохранение весов
            int position = -1;
            for (int i = 0; i < way.path.Count; i++)
            {
                if (way.path[i] == busStop.ID && from == true) { position = i; break; }
                if (way.path[i] == busStop.ID) position = i;
            }
            double wayStops = 0;
            List<int> distances = new List<int>();
            List<double> weights = new List<double>();
            double currentX = (Cs[currentRoad] * verticalBs[currentRoad] - verticalCs[currentRoad] * Bs[currentRoad]) - (As[currentRoad] * verticalBs[currentRoad] - verticalAs[currentRoad] * Bs[currentRoad]);
            double currentY = (As[currentRoad] * verticalCs[currentRoad] - verticalAs[currentRoad] * Cs[currentRoad]) - (As[currentRoad] * verticalBs[currentRoad] - verticalAs[currentRoad] * Bs[currentRoad]);
            for (int i = position; i >= 0; i--)
            {
                int nStop = 0;
                for (int j = 0; j < stops.Count; j++)
                {
                    if (stops[j].ID == way.path[i])
                    {
                        nStop = j;
                        break;
                    }
                }
                wayStops += stops[nStop].weight;
                int back = i - 1; ;
                if (i == 0) back = way.path.Count - 2;
                if (way.path[i] == roads[currentRoad].firstStop || way.path[i] == roads[currentRoad].secondStop)
                {
                    double tmp = Math.Pow(currentX - stops[nStop].latitude, 2) + Math.Pow(currentY - stops[nStop].longitude, 2);
                    distances.Add((int)Math.Sqrt(tmp));
                    weights.Add(roads[currentRoad].weight[dayStatus(time)]);
                    break;
                }
                for (int j = 0; j < roads.Count; j++)
                {
                    if (roads[j].firstStop == way.path[i] && roads[j].secondStop == way.path[back] || roads[j].secondStop == way.path[i] && roads[j].firstStop == way.path[back])
                    {
                        distances.Add(roads[j].length);
                        weights.Add(roads[j].weight[dayStatus(time)]);
                        break;
                    }
                }
                if (i == 0) i = way.path.Count - 2;
            }
            //Вычисление результата
            for (int i = 0; i < distances.Count; i++)
            {
                result.setX(result.first + distances[i]);
            }
            for (int i = 0; i < distances.Count; i++)
            {
                result.setY(result.second + distances[i] / result.first * weights[i]);
            }

            return result;
        }
    }
}
