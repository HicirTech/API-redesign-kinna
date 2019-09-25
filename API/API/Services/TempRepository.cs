using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Models;

namespace API.Services
{
    public class TempRepository : ITempRepository
    {
        private ApiDBContext _tempContext;
        public TempRepository(ApiDBContext tempContext)
        {
            _tempContext = tempContext;
        }
        public ICollection<Temp> GetAllTemps()
        {
            return _tempContext.temp.OrderBy(c => c.time).ToList<Temp>();
        }

        public Temp GetTempByID(int id)
        {
            return _tempContext.temp.Where(c => c.id == id).FirstOrDefault();
        }

        public ICollection<Temp> GetTempByTime(DateTime time)
        {
            return _tempContext.temp.Where(c => c.time == time).ToList();
        }

        public bool hasTempId(int id)
        {
            return  _tempContext.temp.Where(c => c.id == id) != null;
        }
    }
}
