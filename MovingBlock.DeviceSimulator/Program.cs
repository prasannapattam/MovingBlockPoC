using MovingBlock.Functions;
using MovingBlock.Functions.Data;
using MovingBlock.Shared.Models;
using MovingBlock.Shared.Utilities;

Console.WriteLine("Started");

// creating SectionTwin
SectionModel sectionTwin = DigitalTwinFunctions.CreateSectionTwin();

TrainModel trainModel = new TrainModel()
{
    TrainNumber = 1,
    TrainName = "PPK",
    TrainLength = 600,
    Speed = 500 // sectionTwin.Speed
};
DigitalTwinFunctions.CreateTrainTwin(trainModel);

List<TrainModel> trainTwins = DigitalTwinFunctions.GetTrains();
LocationSensorModel frontSensor = new LocationSensorModel(trainTwins[0].FrontSensor!);
LocationSensorModel rearSensor = new LocationSensorModel(trainTwins[0].RearSensor!);

int timeDelay = 1; // secs
int timeDelayms = timeDelay * 1000;
double distanceTravelled = trainModel.Speed * (5.0 / 18.0) * timeDelay;

while (trainTwins.Count > 0)
{
    Console.WriteLine($"{trainTwins[0].FrontPathTravelled} - {trainTwins[0].RearPathTravelled}");
    frontSensor.CurrentLocation = DistanceCalculator.GetPoint2(frontSensor.CurrentLocation, distanceTravelled);
    rearSensor.CurrentLocation = DistanceCalculator.GetPoint2(rearSensor.CurrentLocation, distanceTravelled);
    DeviceTwinFunctions.ProcessLocationSensor(frontSensor);
    DeviceTwinFunctions.ProcessLocationSensor(rearSensor);
    await Task.Delay(1000);
    
    //break;
}

