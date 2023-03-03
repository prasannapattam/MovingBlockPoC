using MovingBlock.Functions;
using MovingBlock.Functions.Data;
using MovingBlock.Shared.Models;
using MovingBlock.Shared.Utilities;

Console.WriteLine("Started");

// creating SectionTwin
SectionModel sectionTwin = DigitalTwinFunctions.CreateSectionTwin();

TrainModel trainModel = new TrainModel()
{
    TrainNumber = 123,
    TrainName = "PPK",
    TrainLength = 600,
    Speed = 100 // sectionTwin.Speed
};
DigitalTwinFunctions.CreateTrainTwin(trainModel);

List<TrainModel> trainTwins = DigitalTwinFunctions.GetTrains();
TrainModel trainTwin = trainTwins[0];
LocationSensorModel frontSensor = new LocationSensorModel(trainTwin.FrontSensor!);
LocationSensorModel rearSensor = new LocationSensorModel(trainTwin.RearSensor!);

int timeDelay = 1; // secs
int timeDelayms = timeDelay * 1000;

while (trainTwins.Count > 0)
{
    Console.WriteLine($"{trainTwin.FrontTravelled:F2} - {trainTwin.RearTravelled:F2}");

    Random random = new Random();
    double randomNumber = Math.Round(random.NextDouble() * 6 - 3, 2);
    double speed = trainTwin.Speed + randomNumber;

    double distanceTravelled = speed * (5.0 / 18.0) * timeDelay;
    frontSensor.CurrentLocation = DistanceCalculator.GetPoint2(frontSensor.CurrentLocation, distanceTravelled);
    rearSensor.CurrentLocation = DistanceCalculator.GetPoint2(rearSensor.CurrentLocation, distanceTravelled);
    frontSensor.TimeElapsed = timeDelay;
    rearSensor.TimeElapsed = timeDelay;
    DeviceTwinFunctions.ProcessLocationSensor(frontSensor);
    DeviceTwinFunctions.ProcessLocationSensor(rearSensor);
    await Task.Delay(timeDelayms);
}

