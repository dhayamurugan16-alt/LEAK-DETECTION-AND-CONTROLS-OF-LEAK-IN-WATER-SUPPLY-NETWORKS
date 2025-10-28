using System;
using System.IO;
using System.Threading;

class Program
{
    static void Main(string[] args)
    {
        string filePath = "pressure_data.csv";

        // Write header if file does not exist
        if (!File.Exists(filePath))
        {
            File.WriteAllText(filePath, "Timestamp,Pressure,Temperature,FlowRate,LeakStatus\n");
        }

        Random random = new Random();

        Console.WriteLine("Collecting sensor data... Press Ctrl+C to stop.");

        while (true)
        {
            string timestamp = DateTime.Now.ToString("s"); // ISO timestamp
            float pressure = (float)Math.Round(random.NextDouble() * 10, 2); // 0–10 bar
            float temperature = (float)Math.Round(20 + random.NextDouble() * 10, 2); // 20–30 °C
            float flowRate = (float)Math.Round(30 + random.NextDouble() * 10, 2); // 30–40 L/min
            string leakStatus = pressure > 7 ? "Leak" : "NoLeak";

            string row = $"{timestamp},{pressure},{temperature},{flowRate},{leakStatus}\n";
            File.AppendAllText(filePath, row);

            Console.WriteLine(row.Trim());
            Thread.Sleep(1000); // 1 second interval
        }
    }
}
