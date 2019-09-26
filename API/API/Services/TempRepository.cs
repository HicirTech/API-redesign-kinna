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

        public bool CreateTemp(Temp temp)
        {
            _tempContext.Add(temp);
            return save();
        }

        public bool DeleteTemp(Temp temp)
        {
            _tempContext.Remove(temp);
            return save();
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

        public bool save()
        {
            return _tempContext.SaveChanges() >= 0;//if save happend?
        }

        public bool updaeteTemp(Temp temp)
        {
            _tempContext.Update(temp);
            return save();
        }
    }
}
