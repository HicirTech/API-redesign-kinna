using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Services
{
    public interface ITempRepository
    {
        ICollection<Temp> GetAllTemps();
        Temp GetTempByID(int id);

        ICollection<Temp> GetTempByTime(DateTime time);
    }
}
