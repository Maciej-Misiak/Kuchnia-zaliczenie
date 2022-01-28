using Food.DataAccess.Repository.IRepository;
using Food.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Food.DataAccess.Repository
{
    public class DishRepository : Repository<Dish>, IDishRepository
    {
        private ApplicationDbContext _db;
        public DishRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;              
        }       
        public void Update(Dish obj)
        {
            var objFromDb = _db.Dish.FirstOrDefault(u=>u.Id == obj.Id);
            if (objFromDb != null)
            {
                objFromDb.Name = obj.Name;
                objFromDb.Ingredients = obj.Ingredients;
                objFromDb.Preparation = obj.Preparation;
                objFromDb.Price = obj.Price;
                objFromDb.CategoryId = obj.CategoryId;
                objFromDb.LevelTypeId = obj.LevelTypeId;
                if (obj.ImageUrl != null)
                {
                    objFromDb.ImageUrl = obj.ImageUrl;
                }
            }
        }
    }
}
