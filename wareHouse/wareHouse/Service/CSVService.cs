using System;
using System.Collections.Generic;
using System.Linq;
using wareHouse.Model;
using wareHouse.DAL;
using wareHouse.Service;
using System.Globalization;
using System.IO;

namespace wareHouse.Service
{
    public class CSVService

    {
        private readonly WarehouseService _warehouseService;
        private readonly CargoService _cargoService;

        public CSVService(WarehouseService warehouseService, CargoService cargoService)
        {
            _warehouseService = warehouseService;
            _cargoService = cargoService;
        }
        public void LoadFromCSV(string filePath)
        {
            if (!File.Exists(filePath))
                throw new FileNotFoundException("CSV file not found", filePath);

            var lines = File.ReadAllLines(filePath);
            for (int i = 1; i < lines.Length; i++)
            {
                var parts = lines[i].Split(';');
                if (parts.Length < 6)
                    continue;

                if (!int.TryParse(parts[0].Trim(), out int warehouseId))
                    continue;

                string warehouseCode = parts[1].Trim();

                if (!int.TryParse(parts[2].Trim(), out int cargoId))
                    continue;

                string cargoCode = parts[3].Trim();

                if (!Enum.TryParse(parts[4].Trim(), true, out WeightVolumeUnit unit))
                    continue;

                if (!double.TryParse(parts[5].Trim(), NumberStyles.Float, CultureInfo.InvariantCulture, out double count))
                    continue;

                var warehouse = _warehouseService.GetById(warehouseId);
                if (warehouse == null || warehouse.Code != warehouseCode)
                    continue;

                var existingCargoInWarehouse = warehouse.Cargos.FirstOrDefault(c => c.Id == cargoId);

                if (existingCargoInWarehouse == null)
                {
                    var existingCargo = _cargoService.GetAll().FirstOrDefault(c => c.Id == cargoId);
                    if (existingCargo != null)
                    {
                        var cargoToAdd = new Cargo
                        {
                            Id = existingCargo.Id,
                            Code = existingCargo.Code,
                            Unit = existingCargo.Unit,
                            Count = count
                        };
                        _warehouseService.LoadCargo(cargoToAdd, warehouseId);
                    }
                    else
                    {
                        Console.WriteLine($"Cargo with ID '{cargoId}' didn`t exist.");
                    }
                }
            }
        }
    }
}
