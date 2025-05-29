using System;
using wareHouse.Model;
using wareHouse.DAL;


namespace wareHouse.Service
{
    public class WarehouseService
    {
        private readonly IRepository<WareHouse> _warehouseRepository;
        public WarehouseService(IRepository<WareHouse> warehouseRepository)
        {
            _warehouseRepository = warehouseRepository;
        }
        public void Create(WareHouse warehouse)
        {
            _warehouseRepository.Create(warehouse);
        }
        public WareHouse GetById(int warehouseId)
        {
            return _warehouseRepository.GetById(warehouseId);
        }
        public IEnumerable<WareHouse> GetAll()
        {
            return _warehouseRepository.GetAll();
        }
        public void Delete(int warehouseId)
        {
            _warehouseRepository.Delete(warehouseId);
        }
        public void Update(WareHouse warehouse)
        {
            _warehouseRepository.Update(warehouse);
        }
        public void LoadCargo(Cargo cargo, int warehouseId)
        {
            var warehouse = GetById(warehouseId);
            if (warehouse != null)
            {
                warehouse.Cargos.Add(cargo);
            }
        }
        public void addCargo(int cargoId, int warehouseId, double addAmount)
        {
            var warehouse = _warehouseRepository.GetById(warehouseId);
            if (warehouse == null)
            {
                throw new ArgumentException($"Warehouse {warehouseId} does not exist.");
            }
            var cargoToChange = warehouse.Cargos.FirstOrDefault(c => c.Id == cargoId);
            if (cargoToChange == null)
            {
                throw new ArgumentException($"Cargo {cargoId} does not exist in warehouse {warehouseId}.");
            }
            cargoToChange.Count += addAmount;
        }
        public void unLoadCargo(string cargoCode, int warehouseId, double unLoadAmount)
        {
            var warehouse = _warehouseRepository.GetById(warehouseId);
            if (warehouse == null)
            {
                throw new ArgumentException($"Warehouse {warehouseId} does not exist.");
            }
            var cargoToChange = warehouse.Cargos.FirstOrDefault(c => c.Code == cargoCode);
            if (cargoToChange == null)
            {
                throw new ArgumentException($"Cargo {cargoCode} does not exist in Warehouse {warehouseId}.");
            }
            if (unLoadAmount <= cargoToChange.Count)
            {
                cargoToChange.Count -= unLoadAmount;
            }
            else
            {
                throw new ArgumentException("Excess of existing stock.");
            }
        }
    }
}
