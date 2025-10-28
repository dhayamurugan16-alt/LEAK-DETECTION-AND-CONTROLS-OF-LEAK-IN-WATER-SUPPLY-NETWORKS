# LEAK-DETECTION-AND-CONTROLS-OF-LEAK-IN-WATER-SUPPLY-NETWORKS
LEAK DETECTION  AND  CONTROLS  OF  LEAK  IN WATER  SUPPLY   NETWORKS
Execution Steps for Leak Detection System using ML.NET
Setup and Execution of Data Collector
This document explains how to execute the Leak Detection System consisting of Data Collector, Model Trainer, and Leak Detector using C# and ML.NET in Visual Studio Code.
1. Install Requirements
Before running any program, make sure the following are installed:
- .NET SDK (version 6 or later)
- Visual Studio Code
- C# extension for VS Code

Verify .NET installation using the command:
    dotnet --version
2. Create the Project Folder
Create a folder named LeakDetectionSystem with three subfolders:

LeakDetectionSystem/
├── DataCollector/
├── ModelTrainer/
└── LeakDetection/
3. Setup the Data Collector
1. Open VS Code and open the DataCollector folder.
2. Create a new console app:
    dotnet new console
3. Replace Program.cs with the Data Collector code provided.
4. Run the app using:
    dotnet run
It will generate pressure_data.csv and print live sensor data.
Sample Output:
2025-10-28T21:05:10,6.23,27.12,35.21,NoLeak
2025-10-28T21:05:11,8.45,25.87,33.42,Leak

 Training and Leak Detection
4. Setup the Model Trainer
1. Open the ModelTrainer folder and create a new console app:
    dotnet new console
2. Add the ML.NET library:
    dotnet add package Microsoft.ML
3. Copy pressure_data.csv from DataCollector folder.
4. Replace Program.cs with the Model Trainer code.
5. Run the trainer using:
    dotnet run
This will generate LeakModel.zip.
5. Setup the Leak Detection
1. Open LeakDetector folder and create a new console app:
    dotnet new console
2. Add ML.NET package:
    dotnet add package Microsoft.ML
3. Copy LeakModel.zip from ModelTrainer folder.
4. Replace Program.cs with Leak Detection code.
5. Run using:
    dotnet run
Sample Output:
Real-time leak detection started... Press Ctrl+C to stop.
Pressure: 3.45 bar | Leak Status: NoLeak
Pressure: 8.22 bar | Leak Status: Leak


6. Summary of Commands
Step	Command	Description
1	dotnet new console	Create new project
2	dotnet add package Microsoft.ML	Add ML.NET library
3	dotnet run	Run the C# program
4	Ctrl + C	Stop the running program
7. Final Flow
1. Run DataCollector → Generates pressure_data.csv
2. Run ModelTrainer → Produces LeakModel.zip
3. Run LeakDetector → Predicts Leak/NoLeak in real time

