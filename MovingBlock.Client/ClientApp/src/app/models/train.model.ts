export interface TrainModel {
  id: number,
  trainNumber: number,
  trainName: string,
  speed: number, // kmph
  trainLength: number
  frontTravelled: number,  // meters
  rearTravelled: number  // meters
}
