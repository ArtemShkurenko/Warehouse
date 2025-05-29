using System;


namespace wareHouse.Model
{
    public class Cargo : IRecord
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public Cargo()
        {
        }
        public WeightVolumeUnit Unit { get; set; }
        public double Count { get; set; }
        public override string ToString()
        {
            return $"Cargo: {Code} amount {Count} {Unit}";
        }
    }
}
