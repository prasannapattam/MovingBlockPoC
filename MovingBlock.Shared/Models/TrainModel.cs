namespace MovingBlock.Shared.Models
{
    public class TrainModel
    {
        public int Id { get; set; }
        public int TrainNumber { get;set; }
        public string? TrainName { get; set; }
        public double Speed { get; set; }
        public DateTime CurrentTime { get;set; }

    }
}
