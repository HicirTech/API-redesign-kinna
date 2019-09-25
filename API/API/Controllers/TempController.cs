using API.Models;
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
        [ProducesResponseType(400)]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ICollection<Temp>>))]
        public IActionResult GetAllTemp()
        {
            var temps = _tempRepository.GetAllTemps().ToList();
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            return Ok(temps);
        }

        //api/temp/id
        [HttpGet("{id}")]
        public IActionResult GetTempByID(int id)
        {
            if (!_tempRepository.hasTempId(id))
            {
                return NotFound();
            }
            var temps = _tempRepository.GetTempByID(id);
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            return Ok(temps);
        }
    }
}
