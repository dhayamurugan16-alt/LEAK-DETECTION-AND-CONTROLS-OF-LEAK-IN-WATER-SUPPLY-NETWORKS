using System;
using Microsoft.ML;
using Microsoft.ML.Data;
public class SensorData
{
    public float Pressure { get; set; }
    public float Temperature { get; set; }
    public float FlowRate { get; set; }
     // Needed because the model was trained with this column
    public string? LeakStatus { get; set; }
}

public class SensorPrediction
{
     [ColumnName("PredictedLabel")]
    public string? PredictedLeakStatus { get; set; }
}

class Program
{
    static void Main(string[] args)
    {
        var mlContext = new MLContext();

        // Load trained model
        ITransformer model = mlContext.Model.Load("LeakModel.zip", out var modelInputSchema);
        var predictionEngine = mlContext.Model.CreatePredictionEngine<SensorData, SensorPrediction>(model);

        Random random = new Random();
        Console.WriteLine("Real-time leak detection started... Press Ctrl+C to stop.");

        while (true)
        {
            float pressure = (float)Math.Round(random.NextDouble() * 10, 2);
            float temperature = (float)Math.Round(20 + random.NextDouble() * 10, 2);
            float flowRate = (float)Math.Round(30 + random.NextDouble() * 10, 2);

            var sample = new SensorData
            {
                Pressure = pressure,
                Temperature = temperature,
                FlowRate = flowRate
            };

            var prediction = predictionEngine.Predict(sample);

            Console.WriteLine($"Pressure: {pressure} bar | Leak Status: {prediction.PredictedLeakStatus}");
            System.Threading.Thread.Sleep(1000);
        }
    }
}
