export interface TrainModel {
  id: number,
  trainNumber: number,
  trainName: string,
  speed: number,
  trainLength: number
  frontTravelled: number,  // meters
  rearTravelled: number  // meters
}
