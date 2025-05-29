using System;
using wareHouse.Model;


namespace wareHouse.Service
{
    public class CargoService
    {

        private readonly IRepository<Cargo> _cargoRepository;
        public CargoService(IRepository<Cargo> cargoRepository)
        {
            _cargoRepository = cargoRepository;
        }
        public void Create(Cargo cargo)
        {
            _cargoRepository.Create(cargo);
        }
        public Cargo GetById(int cargoId)
        {
            return _cargoRepository.GetById(cargoId);
        }
        public IEnumerable<Cargo> GetAll()
        {
            return _cargoRepository.GetAll();
        }
        public void Delete(int cargoId)
        {
            _cargoRepository.Delete(cargoId);
        }
        public void Update(Cargo cargo)
        {
            _cargoRepository.Update(cargo);
        }
    }
}
