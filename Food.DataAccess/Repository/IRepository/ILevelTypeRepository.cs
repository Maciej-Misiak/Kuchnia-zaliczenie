using Food.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Food.DataAccess.Repository.IRepository
{
    public interface ILevelTypeRepository : IRepository<LevelType>
    { 
        void Update(LevelType obj);      
    }
}
