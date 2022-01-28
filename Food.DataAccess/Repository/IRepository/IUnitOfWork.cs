using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Food.DataAccess.Repository.IRepository
{
    public interface IUnitOfWork
    {
        ICategoryRepository Category { get; }
        ILevelTypeRepository LevelType { get; }
        IDishRepository Dish { get; }
        void Save();
    }
}
