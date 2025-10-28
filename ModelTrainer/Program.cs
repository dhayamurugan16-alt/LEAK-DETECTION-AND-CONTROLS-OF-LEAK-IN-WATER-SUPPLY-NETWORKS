using System;
using Microsoft.ML;
using Microsoft.ML.Data;

public class SensorData
{
    [LoadColumn(1)] public float Pressure { get; set; }
    [LoadColumn(2)] public float Temperature { get; set; }
    [LoadColumn(3)] public float FlowRate { get; set; }
    [LoadColumn(4)] public string LeakStatus { get; set; }
}

public class SensorPrediction
{
    [ColumnName("PredictedLabel")] public string PredictedLeakStatus { get; set; }
}

class Program
{
    static void Main(string[] args)
    {
        var mlContext = new MLContext();

        // Load data
        string dataPath = "pressure_data.csv";
        IDataView dataView = mlContext.Data.LoadFromTextFile<SensorData>(
            path: dataPath, hasHeader: true, separatorChar: ',');

        // Define pipeline
        var pipeline = mlContext.Transforms.Conversion.MapValueToKey("Label", nameof(SensorData.LeakStatus))
            .Append(mlContext.Transforms.Concatenate("Features", nameof(SensorData.Pressure), nameof(SensorData.Temperature), nameof(SensorData.FlowRate)))
            .Append(mlContext.MulticlassClassification.Trainers.SdcaMaximumEntropy())
            .Append(mlContext.Transforms.Conversion.MapKeyToValue("PredictedLabel"));

        // Train model
        var model = pipeline.Fit(dataView);

        // Evaluate
        var predictions = model.Transform(dataView);
        var metrics = mlContext.MulticlassClassification.Evaluate(predictions);

        Console.WriteLine($"Accuracy: {metrics.MicroAccuracy:P2}");

        // Save model
        mlContext.Model.Save(model, dataView.Schema, "LeakModel.zip");
        Console.WriteLine("Model saved as LeakModel.zip");
    }
}
