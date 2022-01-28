using Food.DataAccess.Repository.IRepository;
using Food.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Food.DataAccess.Repository
{
    public class LevelTypeRepository : Repository<LevelType>, ILevelTypeRepository
    {
        private ApplicationDbContext _db;
        public LevelTypeRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;              
        }

        public void Add(Dish dish)
        {
            throw new NotImplementedException();
        }

        public void Update(LevelType obj)
        {
            _db.LevelType.Update(obj);
        }
    }
}
