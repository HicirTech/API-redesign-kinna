using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Services
{
    public interface IPpgRepository
    {
        ICollection<Ppg> GetAllPpgs();
        Ppg GetPpgByID(int id);
        ICollection<Ppg> GetPpgByTime(DateTime time);
        bool hasPpgId(int id);
    }
}
