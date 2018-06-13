using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace IntelligentBusStop
{
    class Collector
    {
        public string fileName { get; }
        public double version { get; }
        public bool isActive { get; set; }

        public Collector(string file, double currentVersion)
        {
            fileName = file;
            version = currentVersion;
            isActive = false;
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

        public void rewriteW(List<int> lines, List<int> distances)
        {
            List<List<int>> statistics = new List<List<int>>();
            using (StreamReader sR = new StreamReader(fileName))
            {
                while (true)
                {
                    string tmp = "";
                    int counter = 0;
                    List<int> stat = new List<int>();
                    string temp = sR.ReadLine();
                    if (temp == null) break;
                    for (int i = 0; i < temp.Length; i++)
                    {
                        if (temp[i] != ' ') tmp += temp[i];
                        if (temp[i] == ' ' || i == temp.Length - 1)
                        {
                            switch (counter)
                            {
                                case 0:
                                    stat.Add(int.Parse(tmp));
                                    break;
                                case 1:
                                    stat.Add(int.Parse(tmp));
                                    break;
                                case 2:
                                    stat.Add(int.Parse(tmp));
                                    break;
                                case 3:
                                    stat.Add(int.Parse(tmp));
                                    break;
                                default:
                                    break;
                            }
                            counter++;
                            tmp = "";
                        }
                    }
                    statistics.Add(stat);
                }
            }
            List<List<double>> dayWeights = new List<List<double>>();
            List<List<int>> dayCounts = new List<List<int>>();
            for (int i = 0; i < lines.Count; i += 2)
            {
                List<double> weights = new List<double>();
                List<int> counts = new List<int>();
                for (int j = 0; j < 48; j++)
                {
                    weights.Add(0);
                    counts.Add(0);
                }
                dayWeights.Add(weights);
                dayCounts.Add(counts);
            }
            for (int i = 0; i < statistics.Count; i++)
            {
                for (int j = 0; j < lines.Count; j += 2)
                {
                    if (lines[j] == statistics[i][0] && lines[j + 1] == statistics[i][1] || lines[j + 1] == statistics[i][0] && lines[j] == statistics[i][1])
                    {
                        dayWeights[j / 2][dayStatus(statistics[i][3])] += ((double)distances[j / 2] / (double)statistics[i][2]) / (double)(40 * 10 / 36);
                        dayCounts[j / 2][dayStatus(statistics[i][3])]++;
                        break;
                    }
                }
            }
            for (int i = 0; i < dayWeights.Count; i++)
            {
                for (int j = 0; j < dayWeights[i].Count; j++)
                {
                    if (dayCounts[i][j] != 0) dayWeights[i][j] /= dayCounts[i][j];
                    else dayWeights[i][j] = 0.5;
                }
            }
            using (StreamWriter sw = new StreamWriter("weights.txt", false, System.Text.Encoding.Default))
            {
                for (int i = 0; i < dayWeights.Count; i++)
                {
                    string ToFile = "";
                    for (int j = 0; j < dayWeights[i].Count; j++)
                    {
                        ToFile += dayWeights[i][j].ToString();
                        if (j != dayWeights[i].Count - 1) ToFile += " ";
                    }
                    sw.WriteLine(ToFile);
                }
            }
        }
    }
}
