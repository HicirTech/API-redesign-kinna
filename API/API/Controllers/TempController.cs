using API.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TempController : Controller
    {

        private ITempRepository _tempRepository;
        public TempController(ITempRepository tempRepository)
        {
            _tempRepository = tempRepository;
        }

        //api/temp
        [HttpGet]
        public IActionResult GetAllTemp()
        {
            var temps = _tempRepository.GetAllTemps().ToList();
            return Ok(temps);
        }
    }
}
