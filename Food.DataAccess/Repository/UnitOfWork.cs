using Food.DataAccess.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Food.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private ApplicationDbContext _db;
        public UnitOfWork(ApplicationDbContext db) 
        {
            _db = db;
            Category = new CategoryRepository(_db);
            LevelType = new LevelTypeRepository(_db);
            Dish = new DishRepository(_db);
        }
        public ICategoryRepository Category { get; private set; }
        public ILevelTypeRepository LevelType { get; private set; }
        public IDishRepository Dish { get; private set; }
        public void Save()
        {
            _db.SaveChanges();
        }

    }
}
