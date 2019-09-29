using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Models;

namespace API.Services
{
    public class PpgRepository : IPpgRepository
    {
        private ApiDBContext _ppgContext;

        public PpgRepository(ApiDBContext context)
        {
            _ppgContext = context;
        }

        public bool CreatePpg(Ppg ppg)
        {
            _ppgContext.Add(ppg);
            return save();
        }

        public bool DeletePpg(Ppg ppg)
        {
            _ppgContext.Remove(ppg);
            return save();
        }

        public ICollection<Ppg> GetAllPpgs()
        {
            return _ppgContext.ppg.OrderBy(p => p.id).ToList();
        }

        public Ppg GetPpgByID(int id)
        {
            return _ppgContext.ppg.Where(p => p.id == id).FirstOrDefault();
        }

        public ICollection<Ppg> GetPpgByTime(DateTime time)
        {
            return _ppgContext.ppg.Where(p => p.time == time).ToList();
        }

        public bool hasPpgId(int id)
        {
            return _ppgContext.ppg.Where(c => c.id == id) != null;
        }

        public bool save()
        {
            return _ppgContext.SaveChanges() >= 0;
        }

        public bool updaetePpg(Ppg ppg)
        {
            _ppgContext.Update(ppg);
            return save();
        }
    }
}
