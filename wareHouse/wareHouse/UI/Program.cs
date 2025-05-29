using System;
using wareHouse.DAL;
using wareHouse.Model;
using wareHouse.Service;
using wareHouse.UI;

Console.WriteLine("WAREHOUSE SERVICE");
Console.WriteLine("\n\nDescription of the main commands: ");
Console.WriteLine("Create feature: add warehouse  OR   add cargo");
Console.WriteLine("Get all features: get-all cargo  OR   get-all warehouse");
Console.WriteLine("Load cargo in warehouse: load-cargo warehouse");
Console.WriteLine("Change cargo in warehouse: import-report");


var cargoService = new CargoService(new InMemoryRepository<Cargo>());
var warehouseService = new WarehouseService(new InMemoryRepository<WareHouse>());
var csvService = new CSVService(warehouseService, cargoService);


while (true)
{
    var command = Console.ReadLine();
    var commandParts = command.Split(" ");
    try
    {

        switch (commandParts[0])
        {
            case Commands.CREATE_COMMAND:
                ExecudeAdd(commandParts);
                break;
            case Commands.GETALL_COMMAND:
                ExecudeGetAll(commandParts);
                break;
            case Commands.LOADCARGO_COMMAND:
                ExecudeLoadCargo(commandParts);
                break;
            case Commands.IMPORT_REPORT_COMMAND:
                ExecudeImportReportFilename(commandParts);
                break;
            default:
                Console.WriteLine("Unknown command....");
                break;
        }

    }
    catch (IndexOutOfRangeException)
    {
        Console.WriteLine("Repeat your input,please.....");
    }
    void ExecudeAdd(String[] commandParts)
    {
        switch (commandParts[1])
        {
            case "cargo":
                {
                    Console.WriteLine("\nInput code:");
                    string code = Console.ReadLine();
                    try
                    {
                        Console.WriteLine("Input type of unit: kg/l/m3");
                        string input = Console.ReadLine();
                        if (Enum.TryParse(input, true, out WeightVolumeUnit unitType))
                        {
                            WeightVolumeUnit unit = unitType;
                        }
                        else
                        {
                            throw new ArgumentException("Invalid input. Please enter either 'kg', 'l' or 'm3'.");
                        }
                    }
                    catch (ArgumentException ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    Console.WriteLine("Input amount:");
                    var count = int.Parse(Console.ReadLine());
                    var cargoToAdd = new Cargo()
                    {
                        Code = code,
                        Count = count,
                    };
                    cargoService.Create(cargoToAdd);

                    break;
                }
            case "warehouse":
                {
                    Console.WriteLine("\nInput name:");
                    string code = Console.ReadLine();
                    var warehouseToAdd = new WareHouse()
                    {
                        Code = code,
                    };
                    warehouseService.Create(warehouseToAdd);
                    break;
                }
            default:
                {
                    Console.WriteLine("Incorrect input, please input: add cargo or add warehouse");
                    break;
                }
        }
    }
    void ExecudeGetAll(string[] commandParts)
    {
        try
        {
            switch (commandParts[1])
            {

                case "cargo":
                    {
                        var allCargos= cargoService.GetAll();
                        foreach (Cargo cargo in allCargos)
                        {
                            Console.WriteLine($"\n{cargo.ToString()}");
                        }
                        break;
                    }
                case "warehouse":
                    {
                        var allWarehouse = warehouseService.GetAll();
                        foreach (WareHouse warehouse in allWarehouse)
                        {
                            Console.WriteLine($"\n{warehouse.ToString()}");
                            foreach (var cargo in warehouse.Cargos)
                            {
                                Console.WriteLine($"{cargo.ToString()}");
                            }
                        }
                        break;
                    }
                default:
                    {
                        Console.WriteLine("Incorrect input, please input: get-all cargo  OR   get-all warehouse");
                        break;
                    }
            }
        }
        catch (Exception)
        {
            Console.WriteLine("Repeat your input,please.....");
        }
    }
    void ExecudeLoadCargo(string[] commandParts)
    {
        switch (commandParts[1])
        {
            case "warehouse":
                {

                    Console.WriteLine("Input code cargo");
                    var code = Console.ReadLine();
                    Console.WriteLine("\nInput code warehouse for loading:");
                    int wareHouseId = int.Parse(Console.ReadLine());
                    var cargo = new Cargo();
                    Console.WriteLine("Input amount of cargo");
                    cargo.Count = double.Parse(Console.ReadLine());
                    warehouseService.LoadCargo(cargo, wareHouseId);
                    break;
                }
            default:
                {
                    Console.WriteLine("Incorrect input, please input: load-cargo vehicle  OR   load-cargo warehouse");
                    break;
                }
        }  
    }
    void ExecudeImportReportFilename(String[] commandParts)
    {
        try
        {
            Console.WriteLine("Input file for import");
            string fileName = Console.ReadLine();
            csvService.LoadFromCSV(fileName);
        }

        catch (Exception)
        {
            Console.WriteLine("Repeat your input,please.....");
        }
    }
}
