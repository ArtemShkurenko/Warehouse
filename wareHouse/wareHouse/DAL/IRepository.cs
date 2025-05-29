using System;
using wareHouse.Model;


namespace wareHouse.Service
{
    public interface IRepository<TEntity>
        where TEntity : IRecord
    {
        public TEntity GetById(int Id);
        public IEnumerable<TEntity> GetAll();
        public void Create(TEntity entity);
        public void Update(TEntity newEntity);
        public void Delete(int Id);

    }
}
