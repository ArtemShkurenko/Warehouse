using System;


namespace wareHouse.Model
{
    public class WareHouse : IRecord
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public List<Cargo> Cargos { get; set; }
        public WareHouse()
        {
            Cargos = new List<Cargo>();
        }
        public override string ToString()
        {
            return $"Warehouse: {Code}";
        }
    }
}
