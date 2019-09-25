using API.Models;
using API.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API
{
    public static class DBSeeding
    {
        public static void SeedTempDataContext(this ApiDBContext context)
        {
            var temps = new List<Temp>()
            {
                new Temp()
                {
                temp=1.1f,
                time = new DateTime(1990,1,1)
                },
                new Temp()
                {
                temp=2.2f,
                time = new DateTime(1990,2,2)
                }
            };
            context.temp.AddRange(temps);
            context.SaveChanges();
        }
        public static void SeedPPGDataContext(this ApiDBContext context)
        {
            var ppgs = new List<Ppg>()
            {
                new Ppg()
                {
                    heartRate=100,
                    time = new DateTime(1990,1,1),
                    greenCount1=1,
                    greenCount2=1,
                    xAccel=1,
                    yAccel=1,
                    zAccel=1,
                    hrConfidence=1
                },
                new Ppg()
                {
                    heartRate=101,
                    time = new DateTime(1990,2,2),
                    greenCount1=2,
                    greenCount2=2,
                    xAccel=2,
                    yAccel=2,
                    zAccel=2,
                    hrConfidence=2
                }
            };
            context.ppg.AddRange(ppgs);
            context.SaveChanges();
        }

    }
}
